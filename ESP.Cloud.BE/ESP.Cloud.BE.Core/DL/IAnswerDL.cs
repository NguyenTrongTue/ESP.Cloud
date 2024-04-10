using ESP.Cloud.BE.Core.BaseDL.Repository;
using ESP.Cloud.BE.Core.Model;

namespace ESP.Cloud.BE.Core.DL
{
    public interface IAnswerDL : IBaseRepository<AnswerEntity>
    {
        /// <summary>
        /// Hàm lấy thông tin câu trả lời theo id câu hỏi
        /// </summary>
        /// <param name="questionId">ID câu hỏi</param>
        /// <returns></returns>
        /// Created by: nttue 07/04/2024
        Task<Dictionary<string, object>> GetAnswerByQuestionIdAsync(Guid questionId);

         Task<List<object>> GetAnswerRecently();

    }
}
