using ESP.Cloud.BE.Application.Interface;
using ESP.Cloud.BE.Application.Param;
using Microsoft.AspNetCore.Mvc;

namespace ESP.Cloud.BE.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GaragesController : ControllerBase
    {
        private readonly IGarageService _garageService;
        public GaragesController(IGarageService garageService)
        {
            _garageService = garageService;
        }
        /// <summary>
        /// Hảm tìm kiếm gara
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [HttpPost("get_garages")]
        public async Task<IActionResult> GetGaragesAsync(SearchGarageParam param)
        {
            try
            {
                var results = await _garageService.GetGaragesAsync(param.Coordinates, param.SortBy, param.ListServiceNames, param.CarType, param.TimeOpen, param.take, param.skip);

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
        [HttpGet("get_garage_services")]
        public async Task<IActionResult> GetGarageServices()
        {
            try
            {
                var results = await _garageService.GetGarageServicesAsync();

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
        [HttpGet("get_cars")]
        public async Task<IActionResult> GetCars()
        {
            try
            {
                var results = await _garageService.GetCarsAsync();

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
        [HttpGet("get_garage_by_id")]
        public async Task<IActionResult> GetGarageById(Guid garage_id)
        {
            try
            {
                var results = await _garageService.GetGarageByIdAsync(garage_id);

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
        [HttpPost("get_garage_reviews_by_id")]
        public async Task<IActionResult> GetGarageReviewsById(GarageReviewsParam param)
        {
            try
            {
                var results = await _garageService.GetGarageReviewsByIdAsync(param);

                return Ok(results);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
