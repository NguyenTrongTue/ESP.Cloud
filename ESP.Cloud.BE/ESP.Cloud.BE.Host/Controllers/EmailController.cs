using ESP.Cloud.BE.Email;
using ESP.Cloud.BE.Email.Interface;
using Microsoft.AspNetCore.Mvc;

namespace ESP.Cloud.BE.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        #region Fields
        private readonly IEmail _emailService;
        #endregion
        public EmailController(IEmail emailService)
        {
            _emailService = emailService;
        }

        [HttpPost("send-email")]
        public async Task<IActionResult> SendEmail(EmailDto request)
        {
            _emailService.SendMail(request);

            return Ok();
        }
    }
}
