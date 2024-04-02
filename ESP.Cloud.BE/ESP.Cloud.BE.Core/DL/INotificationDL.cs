using ESP.Cloud.BE.Core.BaseDL.Repository;
using ESP.Cloud.BE.Model;

namespace ESP.Cloud.BE.Core.DL
{
    public interface INotificationDL : IBaseRepository<Notifications>
    {
        Task UpdateUnReadNotificationAsync(Guid id);
    }
}
