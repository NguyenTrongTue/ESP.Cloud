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
                { "answers", new List<object>() }
            };

            var paramDicnary = new Dictionary<string, object>
                {
                    { $"@questionId", questionId }
                };
            var param = new DynamicParameters(paramDicnary);
            var sql = @"select * from public.questions where questions_id = @questionId;
               select a.*,a2.list_reply  from answers a left join lateral ( select jsonb_agg(a1.*) as list_reply from answers a1 where a1.reply_to_answer_id = a.answers_id) a2 on true where questions_id = @questionId and a.is_reply = false";

            using (var multi = await _uow.Connection.QueryMultipleAsync(sql, param))
            {
                var quetion = await multi.ReadAsync<QuestionsEntity>();

                var answers = await multi.ReadAsync<object>();
                result["question"] = quetion;
                result["answers"] = answers;
            }


            return result;
        }

        public async Task<List<object>> GetAnswerRecently()
        {

            var sql = "select distinct q.questions_id, q.questions_title,q.make, q.year, q.model, coalesce(a.answer_total, 0) as answer_total, q.created_date \r\nfrom questions q \r\nleft join lateral\r\n(select count(*) over(partition by a1.questions_id) as answer_total, \r\na1.created_date from  public.answers a1\r\nwhere a1.questions_id = q.questions_id) as a on true\r\norder by q.created_date desc;";
            var answers = await _uow.Connection.QueryAsync<object>(sql);

            return answers.ToList();
        }
    }
}
