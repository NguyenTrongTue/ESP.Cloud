using ESP.Cloud.BE.Application.Dto;
using ESP.Cloud.BE.Application.Interface;
using ESP.Cloud.BE.Application.Param;
using ESP.Cloud.BE.Core.Model;
using ESP.Cloud.BE.Host.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace ESP.Cloud.BE.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnswersController : BaseController<AnswerEntity, CreateAnswerDto, UpdateAnswerDto>
    {
        private readonly IAnswerService _answerService;
        public AnswersController(IAnswerService answerService) : base(answerService)
        {
            _answerService = answerService;
        }
        /// <summary>
        /// Hàm lấy thông tin câu hỏi theo id của xe
        /// </summary>
        /// <param name="questionId"></param>
        /// <returns></returns>
        /// Created by: nttue 07/04/2024
        [HttpGet("get_answer_by_questions_id")]
        public async Task<IActionResult> GetAnswerByQuestionId(Guid questionId)
        {
            try
            {
                var results = await _answerService.GetAnswerByQuestionIdAsync(questionId);

                return Ok(results);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost("get_answer_recently")]
        public async Task<IActionResult> GetAnswerRecently(GetAnswerParam param)
        {
            try
            {
                var results = await _answerService.GetAnswerRecently(param.make, param.model, param.year);

                return Ok(results);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
