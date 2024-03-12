using ESP.Cloud.BE.Application.Dto;
using ESP.Cloud.BE.Application.Interface;
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
        [HttpGet("get_model_by_garage_id")]
        public async Task<IActionResult> GetModelByGarageId(Guid garageId, string make, int year)
        {
            try
            {
                var results = await _bookingService.GetModelByGarageId(garageId, make, year);

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
        public async Task<IActionResult> GetYearByGarageId(Guid garageId, string make)
        {
            try
            {
                var results = await _bookingService.GetYearByGarageId(garageId, make);

                return Ok(results);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
