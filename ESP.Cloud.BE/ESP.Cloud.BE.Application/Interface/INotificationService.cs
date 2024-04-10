using ESP.Cloud.BE.Model;

namespace ESP.Cloud.BE.Application
{
    public interface INotificationService
    {
        Task UpdateUnReadeAsync(Guid id);


        Task<List<Notifications>> GetNotificationsAsync();
    }
}
