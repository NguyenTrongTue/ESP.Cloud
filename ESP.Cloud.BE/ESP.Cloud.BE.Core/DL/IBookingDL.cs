using ESP.Cloud.BE.Core.BaseDL.Repository;
using ESP.Cloud.BE.Core.Model;

namespace ESP.Cloud.BE.Core.DL
{
    public interface IBookingDL : IBaseRepository<BookingHistoryEntity>
    {
        Task<List<object>> GetMakeByGarageId(Guid garageId);
        Task<List<object>> GetYearByGarageId(Guid garageId, string make);
        Task<List<object>> GetModelByGarageId(Guid garageId, string make, int year);
    }
}
