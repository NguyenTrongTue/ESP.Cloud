using ESP.Cloud.BE.Application.Interface;
using ESP.Cloud.BE.Application.Service;
using ESP.Cloud.BE.Core.Model;
using ESP.Cloud.BE.Infrastructure.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ESP.Cloud.BE.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromotionController : BaseReadControler<PromotionEntity>
    {
        private readonly IPromotionService _promotionService;
        public PromotionController(IPromotionService promotionService) : base(promotionService)
        {
            _promotionService = promotionService;
        }

        /// <summary>
        /// Hảm lấy dịch vụ của gara
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [HttpGet("get_filter")]
        public async Task<IActionResult> GetListFilteredPromotions(int? timeFilterType, string? carName, Guid? garageId)
        {
            try
            {
                var results = await _promotionService.GetListFilteredPromotions(timeFilterType, carName, garageId);

                return Ok(results);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
