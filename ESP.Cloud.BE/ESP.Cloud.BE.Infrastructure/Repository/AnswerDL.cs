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

        public async Task<List<object>> GetAnswerRecently(string make, string model, int year)
        {
            var filterBy = "1 = 1";
            var paramDicnary = new Dictionary<string, object>();

            if (!String.IsNullOrEmpty(make))
            {
                filterBy += " AND make ilike @make";
                paramDicnary.Add("@make", make);
            }
            if (!String.IsNullOrEmpty(model))
            {
                filterBy += " AND model ilike @model";
                paramDicnary.Add("@model", model);
            }
            if (year > 0)
            {
                filterBy += " AND year = @year";
                paramDicnary.Add("@year", year);
            }

            var param = new DynamicParameters(paramDicnary);
            var sql = "SELECT DISTINCT q.questions_id, q.questions_title, q.make, q.year, q.model, COALESCE(a.answer_total, 0) AS answer_total, q.created_date FROM questions q LEFT JOIN LATERAL (SELECT COUNT(DISTINCT user_id) AS answer_total FROM public.answers a1 WHERE a1.questions_id = q.questions_id) AS a ON TRUE WHERE " + filterBy + " ORDER BY q.created_date DESC";
            var answers = await _uow.Connection.QueryAsync<object>(sql, param);

            return answers.ToList();
        }
    }
}
