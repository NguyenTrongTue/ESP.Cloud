using ESP.Cloud.BE.Core.DL;
using ESP.Cloud.BE.Model;

namespace ESP.Cloud.BE.Application.Service
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationDL _notificationDL;
        public NotificationService(INotificationDL notificationDL)
        {
            _notificationDL = notificationDL;
        }

        public async Task<List<Notifications>> GetNotificationsAsync()
        {
            var result = await _notificationDL.GetAllAsync();

            return result;
        }

        public async Task UpdateUnReadeAsync(Guid id)
        {
            await _notificationDL.UpdateUnReadNotificationAsync(id);
        }
    }
}
