using Dapper;
using ESP.Cloud.BE.Core.BaseDL;
using ESP.Cloud.BE.Core.DL;
using ESP.Cloud.BE.Core.Model;
using ESP.Cloud.BE.Infrastructure.Repository.Base;
using static Dapper.SqlMapper;

namespace ESP.Cloud.BE.Infrastructure.Repository
{
    public class CarReviewDL : BaseRepository<CarReviewEntity>, ICarReviewDL
    {
        private readonly IUnitOfWork _unitOfWork;
        public CarReviewDL(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CarReviewData> GetQuestionPopular(Guid userId)
        {
            var result = new CarReviewData();

            var paramDicnary = new Dictionary<string, object>
                {
                    { $"@p_user_id", userId }
                };
            var param = new DynamicParameters(paramDicnary);
            var sql = "SELECT make AS car_name, make, COUNT(*) AS total_question, SUM(rating)::DECIMAL / COUNT(*) AS rating_avg, JSONB_AGG(distinct model) AS list_popular FROM car_review GROUP BY make ORDER BY make ASC;" +
                "select distinct cr.*,  COUNT(ul.car_review_id) over(partition by ul.car_review_id) as total_likes, exists( select 1 from user_likes ul2 where user_id = @p_user_id and ul2.car_review_id = cr.car_review_id) as is_liked from car_review cr left join user_likes ul on ul.car_review_id = cr.car_review_id order by created_date desc limit 10;";
            using (var multi = await _uow.Connection.QueryMultipleAsync(sql, param))
            {
                var data1 = await multi.ReadAsync<object>();

                var data2 = await multi.ReadAsync<object>();
                result.data_1 = data1.ToList();
                result.data_2 = data2.ToList();
            }


            return result;
        }
        public async Task<List<object>> GetQuestionByCarAsync(Guid carId)
        {
            var paramDicnary = new Dictionary<string, object>
                {
                    { $"@carId", carId }
                };
            var param = new DynamicParameters(paramDicnary);
            var sql = "select * from car_review where cars_id = @carId;";
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
            var sql = "SELECT * FROM car_review q WHERE car_review_title ILIKE @title;";
            var questions = await _uow.Connection.QueryAsync<object>(sql, param);

            return questions.ToList();
        }


        public async Task<CarReviewData> GetQuestionByMakeAsync(string make, Guid userId)
        {
            var result = new CarReviewData();

            var paramDicnary = new Dictionary<string, object>
                {
                    { $"@make", $"%{make}%" },
                     { $"@p_user_id", userId },
                     { $"@p_make", make },
                };
            var param = new DynamicParameters(paramDicnary);
            var sql = "select make || ' ' || model as car_name, make, model, COUNT(*) AS total_question, SUM(rating)::DECIMAL / COUNT(*) AS rating_avg, jsonb_agg(distinct year) as list_popular from car_review q where make  ilike  @make group by make, model order by make asc;" +
                "select distinct cr.*,  COUNT(ul.car_review_id) over(partition by ul.car_review_id) as total_likes, exists( select 1 from user_likes ul2 where user_id = @p_user_id and ul2.car_review_id = cr.car_review_id) as is_liked from car_review cr left join user_likes ul on ul.car_review_id = cr.car_review_id where cr.make = @p_make order by created_date desc;";
            using (var multi = await _uow.Connection.QueryMultipleAsync(sql, param))
            {
                var data1 = await multi.ReadAsync<object>();

                var data2 = await multi.ReadAsync<object>();
                result.data_1 = data1.ToList();
                result.data_2 = data2.ToList();
            }

            return result;
        }

        public async Task<CarReviewData> GetQuestionByModelAsync(string make, string model, Guid userId)
        {
            var result = new CarReviewData();

            var paramDicnary = new Dictionary<string, object>
                {
                 { $"@make", $"%{make}%" },
                { $"@model", $"%{model}%" },
                   { $"@p_user_id", userId },
                   { $"@p_make", make },
                   { $"@p_model", model },
                };
            var param = new DynamicParameters(paramDicnary);
            var sql = "select make || ' ' || model || ' ' || year as car_name,make, model, year, COUNT(*) AS total_question, SUM(rating)::DECIMAL / COUNT(*) AS rating_avg from car_review q where make  ilike  @make and model ilike @model group by make, model, year order by make asc;" +
                "select distinct cr.*,  COUNT(ul.car_review_id) over(partition by ul.car_review_id) as total_likes, exists( select 1 from user_likes ul2 where user_id = @p_user_id and ul2.car_review_id = cr.car_review_id) as is_liked from car_review cr left join user_likes ul on ul.car_review_id = cr.car_review_id where cr.make = @p_make and cr.model = @p_model order by created_date;";
            using (var multi = await _uow.Connection.QueryMultipleAsync(sql, param))
            {
                var data1 = await multi.ReadAsync<object>();

                var data2 = await multi.ReadAsync<object>();
                result.data_1 = data1.ToList();
                result.data_2 = data2.ToList();
            }

            return result;
        }

        public async Task<List<RatingOverviewResult>> GetOverviewRatingAsync(string? make = "")
        {
            var paramDicnary = new Dictionary<string, object>
                {
                 { $"@p_make", make }
                };
            var param = new DynamicParameters(paramDicnary);
            var sql = "select * from func_get_overview_rating(@p_make)";
            var result = await _uow.Connection.QueryAsync<RatingOverviewResult>(sql, param);

            return result.ToList();
        }
    }
}
