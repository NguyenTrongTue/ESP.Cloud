using Dapper;
using ESP.Cloud.BE.Core.BaseDL;
using ESP.Cloud.BE.Core.DL.GarageDL;
using ESP.Cloud.BE.Core.Model;
using ESP.Cloud.BE.Infrastructure.Repository.Base;
using System.Data;
using static Dapper.SqlMapper;

namespace ESP.Cloud.BE.Infrastructure.Repository
{
    public class GarageDL : BaseRepository<GarageEntity>, IGarageDL
    {
        private readonly IUnitOfWork _unitOfWork;
        public GarageDL(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        /// <summary>
        /// Hàm lọc gara theo điều kiện của người dùng.
        /// </summary>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <param name="sort_by"></param>
        /// <param name="list_vervice_names"></param>
        /// <param name="list_availability"></param>
        /// <param name="take"></param>
        /// <param name="skip"></param>
        /// <returns></returns>
        public async Task<List<object>> SearchGarageAsync(double? latitude, double? longitude, string sort_by, string list_vervice_names, string car_type, string p_open_time, int take, int skip)
        {

            var paramDictionary = new Dictionary<string, object>()
            {
                {"lat" , latitude == null ? 0 : latitude },
                {"lng" , longitude == null ? 0 : longitude },
                {"sort_by" , string.IsNullOrEmpty(sort_by) ? "distance asc" : sort_by },
                {"list_vervice_names" , list_vervice_names },
                {"car_type" , car_type },
                {"p_open_time" , p_open_time },
                {"take" , take },
                {"skip" , skip }
            };
            var param = new DynamicParameters(paramDictionary);


            var sql = "select * from public.func_get_garage_by_coordinate(@lat, @lng, @sort_by, @car_type, @p_open_time, @list_vervice_names,  @take, @skip)";

            var result = await _unitOfWork.Connection.QueryAsync<object>(sql, param, commandType: CommandType.Text);

            return result.ToList();
        }

        public async Task<List<object>> GetGarageServicesAsync()
        {
            var sql = "select distinct service_name from public.garage_services;";

            var result = await _unitOfWork.Connection.QueryAsync<object>(sql, commandType: CommandType.Text);

            return result.ToList();

        }
        public async Task<List<object>> GetCarsAsync()
        {
            var sql = "select distinct make from cars;";

            var result = await _unitOfWork.Connection.QueryAsync<object>(sql, commandType: CommandType.Text);

            return result.ToList();

        }

        public async Task<object?> GetGarageByIdAsync(Guid garage_id)
        {
            var param = new Dictionary<string, object>
                {
                    { $"@garage_id", garage_id }
                };

            var sql = $"select \r\n       t.avg_rating, \r\n            t.total_rating, t1.total_sevices\r\n,g.* from public.garage g \r\nleft join lateral(\r\n\tselect  distinct garage_id, avg(gr.rating) over (partition by gr.garage_id) as avg_rating, \r\n            count(*) over (partition by gr.garage_id) as total_rating\r\n     from garage_reviews gr \r\n     where gr.garage_id = @garage_id \r\n) t on true\r\nleft join lateral(\r\n\tselect distinct garage_id, count(*) over (partition by garage_id, cars_id) as total_sevices\r\n\tfrom garage_services gs \r\n\twhere gs.garage_id = @garage_id\r\n) t1 on true\r\nwhere g.garage_id = @garage_id;";

            var entity = await _uow.QueryDefault<object>(sql, param);

            return entity;
        }

        public async Task<List<object>> GetGarageReviewsByIdAsync(Guid garageId, int take, int skip)
        {
            var param = new Dictionary<string, object>
                {
                    { $"@garage_id", garageId },
                     { $"@take", take },
                      { $"@skip",  take * skip }
                };

            var sql = $"select t.fullname, gr.* from garage_reviews gr \r\nleft join lateral (\r\n\tselect u.fullname from \"user\" u \r\n\twhere u.user_id = gr.user_id \r\n) t on true where garage_id = @garage_id limit @take offset @skip;";

            var entity = await _unitOfWork.Connection.QueryAsync<object>(sql, param);

            return entity.ToList();
        }
    }
}
