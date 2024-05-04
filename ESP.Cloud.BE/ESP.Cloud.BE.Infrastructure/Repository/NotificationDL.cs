using Dapper;
using ESP.Cloud.BE.Core.BaseDL;
using ESP.Cloud.BE.Core.DL;
using ESP.Cloud.BE.Core.Model;
using ESP.Cloud.BE.Infrastructure.Repository.Base;
using ESP.Cloud.BE.Model;
using Newtonsoft.Json;
using System.Data;
using static Dapper.SqlMapper;

namespace ESP.Cloud.BE.Infrastructure.Repository
{
    public class NotificationDL : BaseRepository<Notifications>, INotificationDL
    {
        private readonly IUnitOfWork _unitOfWork;
        public NotificationDL(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task UpdateUnReadNotificationAsync(Guid id)
        {
            var param = new Dictionary<string, object>();
            var sql = $"UPDATE public.user_notifications\r\nSET unread = false\r\nWHERE user_notifications_id=@Id;";

            param.Add("@Id", id);

            await _uow.ExecuteDefault(sql, param);
        }
    }
}
