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
            var sql = "select make as car_name, count(*) as total_question, jsonb_agg(model) as list_popular from questions q group by make order by make asc;";
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
                    { $"@title", title }
                };
            var param = new DynamicParameters(paramDicnary);
            var sql = "select * from questions q where questions_title  ilike '%@title%';";
            var questions = await _uow.Connection.QueryAsync<object>(sql, param);

            return questions.ToList();
        }

        public async Task<List<object>> GetQuestionByMakeAsync(string make)
        {
            var paramDicnary = new Dictionary<string, object>
                {
                    { $"@make", make }
                };
            var param = new DynamicParameters(paramDicnary);
            var sql = "select * from questions q where make  ilike '%@make%';";
            var questions = await _uow.Connection.QueryAsync<object>(sql, param);

            return questions.ToList();
        }

        public async Task<List<object>> GetQuestionByYearAsync(string make, int year)
        {
            var paramDicnary = new Dictionary<string, object>
                {
                    { $"@make", make },
                    { $"@year", year }
                };
            var param = new DynamicParameters(paramDicnary);
            var sql = "select * from questions q where make  ilike '%@make%' and year  = @year;";
            var questions = await _uow.Connection.QueryAsync<object>(sql, param);

            return questions.ToList();
        }

        public async Task<List<object>> GetQuestionByModelAsync(string make, int year, string model)
        {
            var paramDicnary = new Dictionary<string, object>
                {
                    { $"@make", make },
                    { $"@year", year },
                    { $"@model", model }
                };
            var param = new DynamicParameters(paramDicnary);
            var sql = "select * from questions q where make  ilike '%@make%' and year  = @year and model = @model ;";
            var questions = await _uow.Connection.QueryAsync<object>(sql, param);

            return questions.ToList();
        }
    }
}
