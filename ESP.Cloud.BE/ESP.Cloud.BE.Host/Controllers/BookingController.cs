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
    public class Booking : BaseController<BookingHistoryEntity, CreateBookingDto, UpdateBookingDto>
    {
        private readonly IBookingService _bookingService;
        public Booking(IBookingService bookingService) : base(bookingService)
        {
            _bookingService = bookingService;
        }

        /// <summary>
        /// Hảm lấy dịch vụ của gara
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [HttpGet("get_garage_services")]
        public async Task<IActionResult> GetGarageServices(Guid carId)
        {
            try
            {
                var results = await _bookingService.GetGarageServicesAsync(carId);

                return Ok(results);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Hảm lấy dịch vụ của gara
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [HttpGet("get_make_by_garage_id")]
        public async Task<IActionResult> GetMakeByGarageId(Guid garageId)
        {
            try
            {
                var results = await _bookingService.GetMakeByGarageId(garageId);

                return Ok(results);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Hảm lấy dịch vụ của gara
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [HttpGet("get_year_by_garage_id")]
        public async Task<IActionResult> GetYearByGarageId(Guid garageId, string make, string model)
        {
            try
            {
                var results = await _bookingService.GetYearByGarageId(garageId, make, model);

                return Ok(results);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Hảm lấy dịch vụ của gara
        /// </summary>
        /// <param name="garageId"></param>
        ///  <param name="make"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [HttpGet("get_model_by_garage_id")]
        public async Task<IActionResult> GetModelByGarageId(Guid garageId, string make)
        {
            try
            {
                var results = await _bookingService.GetModelByGarageId(garageId, make);

                return Ok(results);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Hảm lấy dịch vụ của gara
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [HttpPost("get_estimate_service")]
        public async Task<IActionResult> GetEstimateService(EstimateParam estimateParam)
        {
            try
            {
                var results = await _bookingService.GetEstimateServiceAsync(estimateParam.carId, estimateParam.serviceCodes);

                return Ok(results);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        /// <summary>
        /// Hảm lấy dịch vụ của gara
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [HttpPost("get_garage_by_estimate")]
        public async Task<IActionResult> GetGarageByEstimate(GetGarageByEstimateParam estimateParam)
        {
            try
            {
                var results = await _bookingService.GetGarageByEstimateAsync(estimateParam.latitude, estimateParam.longitude, estimateParam.p_estimate_id);

                return Ok(results);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
