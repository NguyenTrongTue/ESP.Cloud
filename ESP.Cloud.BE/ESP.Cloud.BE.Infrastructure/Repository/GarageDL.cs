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

            var sql = $"with temp_result as (\r\n\t\tselect * \r\n\t\tfrom public.garage g\r\n\t\twhere garage_id = @garage_id \r\n) select tr.*,\r\n\t\t coalesce(gr2.total_reviews, 0) as total_reviews,\r\n\t\t coalesce(gr2.avg_rating, 0) as avg_rating\t\r\n\t\t from temp_result tr\r\n\t\t left join lateral(\r\n\t\t \tselect count(gr.*)as total_reviews,\r\n\t\t\tsum(gr.rating) as avg_rating,\r\n\t\t\tgr.garage_id\r\n\t\t\tfrom public.garage_reviews gr\r\n\t\t\twhere gr.garage_id = tr.garage_id\r\n\t\t\tgroup by garage_id\r\n\t\t ) gr2 on true\t\t \t\t\r\n\t\t limit 1;";

            var entity = await _uow.QueryDefault<object>(sql, param);

            return entity;
        }
    }
}
