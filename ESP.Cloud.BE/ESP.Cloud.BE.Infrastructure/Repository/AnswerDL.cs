using Dapper;
using ESP.Cloud.BE.Core.BaseDL;
using ESP.Cloud.BE.Core.DL;
using ESP.Cloud.BE.Core.Model;
using ESP.Cloud.BE.Infrastructure.Repository.Base;
using static Dapper.SqlMapper;

namespace ESP.Cloud.BE.Infrastructure.Repository
{
    public class AnswerDL : BaseRepository<AnswerEntity>, IAnswerDL
    {
        private readonly IUnitOfWork _unitOfWork;
        public AnswerDL(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Dictionary<string, object>> GetAnswerByQuestionIdAsync(Guid questionId)
        {
            var result = new Dictionary<string, object>() {
                { "question", new QuestionsEntity() },
                { "answers", new List<AnswerEntity>() }
            };

            var paramDicnary = new Dictionary<string, object>
                {
                    { $"@questionId", questionId }
                };
            var param = new DynamicParameters(paramDicnary);
            var sql = @"select * from public.questions where questions_id = @questionId;
                select * from public.answers where questions_id = @questionId;";

            using (var multi = await _uow.Connection.QueryMultipleAsync(sql, param))
            {
                var quetion = await multi.ReadAsync<QuestionsEntity>();

                var answers = await multi.ReadAsync<AnswerEntity>();
                result["question"] = quetion;
                result["answers"] = answers;
            }


            return result;
        }

        public async Task<List<object>> GetAnswerRecently()
        {

            var sql = "select q.questions_id, q.questions_title,q.make, q.year, q.model, coalesce(a.answer_total, 0) as answer_total, a.created_date from questions q \r\n left join lateral\r\n(select\tcount(*) over(partition by a1.questions_id) as answer_total, a1.created_date\r\n\tfrom  public.answers a1\t\r\n\twhere a1.questions_id = q.questions_id\r\n) as a on true\r\norder by a.created_date desc;";
            var answers = await _uow.Connection.QueryAsync<object>(sql);

            return answers.ToList();
        }
    }
}
