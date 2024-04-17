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

        public async Task<List<object>> GetYearByGarageId(Guid garageId, string make, string model)
        {
            var result = await _bookingDL.GetYearByGarageId(garageId, make, model);

            return result;
        }

        public async Task<List<object>> GetGarageServicesAsync(Guid carId)
        {
            var result = await _bookingDL.GetGarageServicesAsync(carId);

            return result;
        }

        public async Task<List<object>> GetModelByGarageId(Guid garageId, string make)
        {
            var result = await _bookingDL.GetModelByGarageId(garageId, make);

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

        public async Task<List<object>> GetEstimateServiceAsync(Guid carId, List<ServiceCode> serviceCodes)
        {
            var result = await _bookingDL.GetEstimateServiceAsync(carId, serviceCodes);

            return result;
        }
        public async Task<List<object>> GetGarageByEstimateAsync(double? latitude, double? longitude, Guid p_estimate_id)
        {
            var result = await _bookingDL.GetGarageByEstimateAsync(latitude, longitude, p_estimate_id);

            return result;
        }


    }
}
