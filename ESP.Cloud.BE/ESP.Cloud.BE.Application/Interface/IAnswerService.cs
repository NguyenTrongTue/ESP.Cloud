using ESP.Cloud.BE.Application.Dto;
using ESP.Cloud.BE.Application.Interface.Base;
using ESP.Cloud.BE.Core.Model;

namespace ESP.Cloud.BE.Application.Interface
{
    public interface IAnswerService : IBaseService<AnswerEntity, CreateAnswerDto, UpdateAnswerDto>
    {
        /// <summary>
        /// Hàm lấy thông tin câu trả lời theo id câu hỏi
        /// </summary>
        /// <param name="questionId">ID câu hỏi</param>
        /// <returns></returns>
        /// Created by: nttue 07/04/2024
        Task<Dictionary<string, object>> GetAnswerByQuestionIdAsync(Guid questionId);

        Task<List<object>> GetAnswerRecently(string make, string model, int year);
    }
}
