using ESP.Cloud.BE.Application;
using ESP.Cloud.BE.Application.Dto;
using ESP.Cloud.BE.Application.Interface;
using ESP.Cloud.BE.Application.Param;
using ESP.Cloud.BE.Application.Service;
using ESP.Cloud.BE.Core.Model;
using ESP.Cloud.BE.Host.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace ESP.Cloud.BE.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly INotificationService _notificationServices;
        public NotificationsController(INotificationService notificationServices)
        {
            _notificationServices = notificationServices;
        }
        /// <summary>
        /// Hảm lấy thông báo từ server
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [HttpGet]
        public async Task<IActionResult> GetNotification()
        {
            try
            {
                var results = await _notificationServices.GetNotificationsAsync();

                return Ok(results);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPut("update_unread")]
        public async Task<IActionResult> UpdateUnRead(Guid id)
        {
            try
            {
                await _notificationServices.UpdateUnReadeAsync(id);

                return Ok(1);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
