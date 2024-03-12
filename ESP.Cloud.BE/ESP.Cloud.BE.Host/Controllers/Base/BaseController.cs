using ESP.Cloud.BE.Application.Interface.Base;
using Microsoft.AspNetCore.Mvc;

namespace ESP.Cloud.BE.Host.Controllers.Base
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController<TEntityDto, TEntityCreateDto, TEntityUpdateDto> : BaseReadControler<TEntityDto>
    {
        #region Properties
        private readonly IBaseService<TEntityDto, TEntityCreateDto, TEntityUpdateDto> _baseService;
        #endregion 

        #region Constructors
        protected BaseController(IBaseService<TEntityDto, TEntityCreateDto, TEntityUpdateDto> baseService) : base(baseService)
        {
            _baseService = baseService;
        }
        #endregion 

        #region Methods
        /// <summary>
        /// Hàm thêm mới một nhân viên
        /// </summary>
        /// <param name="employee">Thông tin của nhân viên được thêm mới</param>
        /// <returns>Trả về kết quả của bản ghi đã được chèn</returns>
        /// nttue (12/7/2023)
        // POST api/<EmployeesController>
        [HttpPost]
        public async Task<IActionResult> InsertAsync([FromBody] TEntityCreateDto entityCreateDto)
        {

            var entityDto = await _baseService.InsertAsync(entityCreateDto);

            return StatusCode(StatusCodes.Status201Created, entityDto);
        }

        /// <summary>
        /// Hàm sửa một nhân viên
        /// </summary>
        /// <param name="id">id của nhân viên được sửa</param>
        /// <param name="employeeUpdateDto">thông tin sửa của nhân viên</param>
        /// <returns>Trả về nhân viên mới đã được sửa</returns>
        /// nttue (12/07/2023)
        // PUT api/<EmployeesController>/5  
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(Guid id, [FromBody] TEntityUpdateDto employeeUpdateDto)
        {
            var entityDto = await _baseService.UpdateAsync(id, employeeUpdateDto);

            return StatusCode(StatusCodes.Status200OK, entityDto);

        }

        /// <summary>
        /// Xóa nhân viên theo từng id
        /// </summary>
        /// <param name="id">id của 1 nhân viên</param>
        /// <returns>Trả về số lượng bản ghi đã xóa</returns>
        /// nttue (12/07/2023)
        // DELETE api/<EmployeesController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            await _baseService.DeleteAsync(id);
            return StatusCode(StatusCodes.Status200OK);
        }
        #endregion
    }
}
