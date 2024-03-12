using ESP.Cloud.BE.Application.Interface.Base;
using Microsoft.AspNetCore.Mvc;

namespace ESP.Cloud.BE.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseReadControler<TModelEntity> : ControllerBase
    {
        private readonly IBaseReadService<TModelEntity> _baseReadService;
        public BaseReadControler(IBaseReadService<TModelEntity> baseReadService)
        {
            _baseReadService = baseReadService;
        }
        /// <summary>
        /// Lấy ra một bản ghi theo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Trả về bản ghi được lấy</returns>
        /// nttue (12/07/2023)       
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(Guid id)
        {

            var entityDto = await _baseReadService.GetAsync(id);

            return StatusCode(StatusCodes.Status200OK, entityDto);

        }

        /// <summary>
        /// Hàm lấy tất cả các bản ghi
        /// </summary>
        /// <returns>Trả về danh sách bản ghi lấy được</returns>
        /// Created by: nttue (20/08/2023)
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var listEntityDto = await _baseReadService.GetAllAsync();

            return StatusCode(StatusCodes.Status200OK, listEntityDto);

        }
    }
}
