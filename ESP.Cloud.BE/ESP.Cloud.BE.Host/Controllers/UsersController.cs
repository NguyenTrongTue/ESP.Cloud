using ESP.Cloud.BE.Application.Interface;
using ESP.Cloud.BE.Application.Param;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ESP.Cloud.BE.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        #region Fields
        private readonly IAuthService _authService;
        #endregion
        public UsersController(IAuthService authService)
        {
            _authService = authService;
        }
        /// <summary>
        /// API đăng ký.
        /// </summary>
        /// <param name="userDto">Thông tin của người dùng</param>
        /// <returns>Trả về thông tin người dùng đã được đăng ký.</returns>
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterParam userDto)
        {
            var result = await _authService.Register(userDto);

            return Ok(result);
        }

        /// <summary>
        /// Login
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginParam userDto)
        {
            var result = await _authService.Login(userDto);

            return Ok(result);
        }

        [HttpGet("get_new_password")]
        public async Task<IActionResult> GetNewPasswordAsync(string email)
        {
            try
            {
                await _authService.GetNewPasswordAsync(email);
                return Ok(1);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
