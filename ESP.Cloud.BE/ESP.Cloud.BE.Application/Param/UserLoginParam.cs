using System.ComponentModel.DataAnnotations;

namespace ESP.Cloud.BE.Application.Param
{
    public class UserLoginParam
    {
        /// <summary>
        /// Mật khẩu được mã hóa và lưu vào db.
        /// </summary>
        public string password { get; set; } = string.Empty;

        /// <summary>
        /// Email người dùng
        /// </summary>
        [Required, EmailAddress]
        public string email { get; set; } = string.Empty;
    }
}
