using AutoMapper;
using ESP.Cloud.BE.Application.Dto;
using ESP.Cloud.BE.Application.Helper;
using ESP.Cloud.BE.Application.Interface;
using ESP.Cloud.BE.Application.Param;
using ESP.Cloud.BE.Core.DL.UserDL;
using ESP.Cloud.BE.Core.ESPException;
using ESP.Cloud.BE.Core.Model;
using ESP.Cloud.BE.Core.Resource;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;

namespace ESP.Cloud.BE.Application.Service
{
    public class AuthSerivice : IAuthService
    {
        private readonly IUserDL _userDL;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        public AuthSerivice(IUserDL userDL, IMapper mapper, IConfiguration config)
        {
            _userDL = userDL;
            _mapper = mapper;
            _config = config;
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

            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_config.GetSection("EmailUsername").Value));
            email.To.Add(MailboxAddress.Parse(request.To));
            email.Subject = request.Subject;
            email.Body = new TextPart(TextFormat.Html) { Text = request.Body };

            using var smtp = new MailKit.Net.Smtp.SmtpClient();
            smtp.Connect(_config.GetSection("EmailHost").Value, 465, true);
            smtp.Authenticate(_config.GetSection("EmailUsername").Value, _config.GetSection("EmailPassword").Value);
            smtp.Send(email);
            smtp.Disconnect(true);

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
