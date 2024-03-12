using AutoMapper;
using ESP.Cloud.BE.Application.Dto;
using ESP.Cloud.BE.Application.Interface;
using ESP.Cloud.BE.Application.Service.Base;
using ESP.Cloud.BE.Core.DL;
using ESP.Cloud.BE.Core.Model;

namespace ESP.Cloud.BE.Application.Service
{
    public class BookingService : BaseService<BookingHistoryEntity, CreateBookingDto, UpdateBookingDto>, IBookingService
    {
        private readonly IBookingDL _bookingDL;
        public BookingService(IBookingDL bookingDL, IMapper mapper) : base(bookingDL, mapper)
        {
            _bookingDL = bookingDL;
        }

        public async Task<List<object>> GetMakeByGarageId(Guid garageId)
        {
            var result = await _bookingDL.GetMakeByGarageId(garageId);

            return result;
        }

        public async Task<List<object>> GetModelByGarageId(Guid garageId, string make, int year)
        {
            var result = await _bookingDL.GetModelByGarageId(garageId, make, year);

            return result;
        }

        public async Task<List<object>> GetYearByGarageId(Guid garageId, string make)
        {
            var result = await _bookingDL.GetYearByGarageId(garageId, make);

            return result;
        }

        public override async Task<BookingHistoryEntity> MapCreateDtoToEntity(CreateBookingDto createBookingDto)
        {
            var booking = _mapper.Map<BookingHistoryEntity>(createBookingDto);
            booking.booking_history_id = Guid.NewGuid();

            return booking;
        }

        public override Task<BookingHistoryEntity> MapUpdateDtoToEntity(Guid id, UpdateBookingDto entityUpdateDto)
        {
            throw new NotImplementedException();
        }
    }
}
