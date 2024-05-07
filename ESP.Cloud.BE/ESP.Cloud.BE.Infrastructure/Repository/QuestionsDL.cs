using Dapper;
using ESP.Cloud.BE.Core.BaseDL;
using ESP.Cloud.BE.Core.DL;
using ESP.Cloud.BE.Core.Model;
using ESP.Cloud.BE.Infrastructure.Repository.Base;
using Newtonsoft.Json;
using System.Data;
using static Dapper.SqlMapper;

namespace ESP.Cloud.BE.Infrastructure.Repository
{
    public class QuestionsDL : BaseRepository<QuestionsEntity>, IQuestionsDL
    {
        private readonly IUnitOfWork _unitOfWork;
        public QuestionsDL(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<object>> GetQuestionPopular()
        {
            var sql = "select make as car_name, make, count(*) as total_question, jsonb_agg(distinct model) as list_popular from questions q group by make order by make asc;";
            var questions = await _uow.Connection.QueryAsync<object>(sql);

            return questions.ToList();
        }
        public async Task<List<object>> GetQuestionByCarAsync(Guid carId)
        {
            var paramDicnary = new Dictionary<string, object>
                {
                    { $"@carId", carId }
                };
            var param = new DynamicParameters(paramDicnary);
            var sql = "select * from questions where cars_id = @carId;";
            var questions = await _uow.Connection.QueryAsync<object>(sql, param);

            return questions.ToList();
        }


        public async Task<List<object>> SearchQuestionByTitleAsync(string title)
        {
            var paramDicnary = new Dictionary<string, object>
                {
                    { "@title", $"%{title}%" }
                };

            var param = new DynamicParameters(paramDicnary);
            var sql = "SELECT * FROM questions q WHERE questions_title ILIKE @title;";
            var questions = await _uow.Connection.QueryAsync<object>(sql, param);

            return questions.ToList();
        }


        public async Task<List<object>> GetQuestionByMakeAsync(string make)
        {
            var paramDicnary = new Dictionary<string, object>
                {
                    { $"@make", $"%{make}%" }
                };
            var param = new DynamicParameters(paramDicnary);
            var sql = "select make || ' ' || model as car_name,make, model, count(*) as total_question, jsonb_agg(distinct year) as list_popular from questions q where make  ilike  @make group by make, model order by make asc;";
            var questions = await _uow.Connection.QueryAsync<object>(sql, param);

            return questions.ToList();
        }

        public async Task<List<object>> GetQuestionByModelAsync(string make, string model)
        {
            var paramDicnary = new Dictionary<string, object>
                {
                 { $"@make", $"%{make}%" },
                    { $"@model", $"%{model}%" }
                };
            var param = new DynamicParameters(paramDicnary);
            var sql = "select make || ' ' || model || ' ' || year as car_name,make, model, year, count(*) as total_question from questions q where make  ilike  @make and model ilike @model group by make, model, year order by make asc;";
            var questions = await _uow.Connection.QueryAsync<object>(sql, param);

            return questions.ToList();
        }


    }
}
