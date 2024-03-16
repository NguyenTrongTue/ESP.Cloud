using AutoMapper;
using ESP.Cloud.BE.Application.Dto;
using ESP.Cloud.BE.Application.Helper;
using ESP.Cloud.BE.Application.Interface;
using ESP.Cloud.BE.Application.Param;
using ESP.Cloud.BE.Core.DL.UserDL;
using ESP.Cloud.BE.Core.ESPException;
using ESP.Cloud.BE.Core.Model;
using ESP.Cloud.BE.Core.Resource;
using ESP.Cloud.BE.Email;
using ESP.Cloud.BE.Email.Interface;
using Microsoft.Extensions.Configuration;

namespace ESP.Cloud.BE.Application.Service
{
    public class AuthSerivice : IAuthService
    {
        private readonly IUserDL _userDL;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        private readonly IEmail _email;
        public AuthSerivice(IUserDL userDL, IMapper mapper, IConfiguration config, IEmail email)
        {
            _userDL = userDL;
            _mapper = mapper;
            _config = config;
            _email = email;
        }

        public async Task GetNewPasswordAsync(string emailReset)
        {
            var userExists = await _userDL.GetUserByEmailAsync(emailReset);
            if (userExists == null)
            {
                throw new ConflictException(Resource.UserNotExists);
            }
            var password = AuthHelper.GenerateRandomPassword();

            var request = new EmailDto()
            {
                Subject = "ESP: Lấy lại mật khẩu",
                To = emailReset,
                Body = password
            };

            _email.SendMail(request);

            AuthHelper.CreatePassword(password, out byte[] passwordHash, out byte[] passwordSalt);

            await _userDL.UpdatePassWordAsync(passwordHash, passwordSalt, userExists.user_id);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userLoginDto"></param>
        /// <returns></returns>
        /// <exception cref="ConflictException"></exception>
        /// <exception cref="AuthException"></exception>
        public async Task<UserDto> Login(UserLoginParam userLoginDto)
        {
            var userExists = await _userDL.GetUserByEmailAsync(userLoginDto.email);
            if (userExists == null)
            {
                throw new ConflictException(Resource.UserNotExists);
            }
            if (!AuthHelper.VetifyPassword(userLoginDto.password, userExists.password_hash, userExists.password_salt))
            {
                throw new AuthException(Resource.IncorrectPassword);
            }

            var result = _mapper.Map<UserDto>(userExists);

            return result;
        }

        public async Task<UserDto> Register(UserRegisterParam userDto)
        {
            try
            {
                var userExists = await _userDL.GetUserByEmailAsync(userDto.email);
                if (userExists != null)
                {
                    throw new NotFoundException(Resource.UserExists);
                }
                AuthHelper.CreatePassword(userDto.password, out byte[] passwordHash, out byte[] passwordSalt);
                var user = _mapper.Map<UserEntity>(userDto);

                user.password_hash = passwordHash;
                user.password_salt = passwordSalt;

                user.user_id = Guid.NewGuid();

                await _userDL.InsertAsync(user);

                var result = _mapper.Map<UserDto>(user);

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
