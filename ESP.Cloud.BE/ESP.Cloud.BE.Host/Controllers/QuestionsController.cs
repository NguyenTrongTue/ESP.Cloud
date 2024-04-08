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
    public class QuestionsController : BaseController<QuestionsEntity, CreateQuestionsDto, UpdateQuestionsDto>
    {
        private readonly IQuestionsService _questionsService;
        public QuestionsController(IQuestionsService questionsService) : base(questionsService)
        {
            _questionsService = questionsService;
        }

        /// <summary>
        /// Hàm lấy thông tin câu hỏi theo id của xe
        /// </summary>
        /// <param name="carId"></param>
        /// <returns></returns>
        /// Created by: nttue 07/04/2024
        [HttpGet("get_questions_popular")]
        public async Task<IActionResult> GetQuestionPopular()
        {
            try
            {
                var results = await _questionsService.GetQuestionPopular();

                return Ok(results);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Hàm lấy thông tin câu hỏi theo id của xe
        /// </summary>
        /// <param name="carId"></param>
        /// <returns></returns>
        /// Created by: nttue 07/04/2024
        [HttpGet("get_questions_by_car")]
        public async Task<IActionResult> GetQuestionByCar(Guid carId)
        {
            try
            {
                var results = await _questionsService.GetQuestionByCarAsync(carId);

                return Ok(results);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Hàm tìm kiếm câu hỏi theo tiêu đề câu hỏi
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        /// Created by: nttue 07/04/2024
        [HttpGet("search_questions_by_title")]
        public async Task<IActionResult> SearchQuestionByTitle(string title)
        {
            try
            {
                var results = await _questionsService.SearchQuestionByTitleAsync(title);

                return Ok(results);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Hàm lấy thông tin câu hỏi theo make
        /// </summary>
        /// <param name="make"></param>
        /// <returns></returns>
        /// Created by: nttue 07/04/2024
        [HttpGet("get_questions_by_make")]
        public async Task<IActionResult> GetQuestionByMake(string make)
        {
            try
            {
                var results = await _questionsService.GetQuestionByMakeAsync(make);

                return Ok(results);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Hàm lấy thông tin câu hỏi theo year
        /// </summary>
        /// <param name="make"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        /// Created by: nttue 07/04/2024
        [HttpGet("get_questions_by_year")]
        public async Task<IActionResult> GetQuestionByYear(string make, int year)
        {
            try
            {
                var results = await _questionsService.GetQuestionByYearAsync(make, year);

                return Ok(results);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Hàm lấy thông tin câu hỏi theo model
        /// </summary>
        /// <param name="make"></param>
        /// <param name="year"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        /// Created by: nttue 07/04/2024
        [HttpGet("get_questions_by_model")]
        public async Task<IActionResult> GetQuestionByModel(string make, int year, string model)
        {
            try
            {
                var results = await _questionsService.GetQuestionByModelAsync(make, year, model);

                return Ok(results);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


    }
}
