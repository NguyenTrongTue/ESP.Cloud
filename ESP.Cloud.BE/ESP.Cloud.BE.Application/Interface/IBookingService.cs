using ESP.Cloud.BE.Application.Dto;
using ESP.Cloud.BE.Application.Interface.Base;
using ESP.Cloud.BE.Core.Model;

namespace ESP.Cloud.BE.Application.Interface
{
    public interface IBookingService : IBaseService<BookingHistoryEntity, CreateBookingDto, UpdateBookingDto>
    {
        Task<List<object>> GetMakeByGarageId(Guid garageId);
        Task<List<object>> GetYearByGarageId(Guid garageId, string make);
        Task<List<object>> GetModelByGarageId(Guid garageId, string make, int year);
    }
}
