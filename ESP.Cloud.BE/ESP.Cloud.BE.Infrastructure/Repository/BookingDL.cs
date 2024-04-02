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
    public class BookingDL : BaseRepository<BookingHistoryEntity>, IBookingDL
    {
        private readonly IUnitOfWork _unitOfWork;
        public BookingDL(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<NotificationsResult> CheckBooking()
        {
            var result = new NotificationsResult();
            var sql = @"select c.make, c.model, c.""year"", g.address, g.garage_name, bh.*
                FROM booking_history bh
                LEFT JOIN cars c ON c.cars_id = bh.cars_id
                LEFT JOIN garage g ON g.garage_id = bh.garage_id
                WHERE DATE(booking_date) = CURRENT_DATE + INTERVAL '1 day';

                select g.garage_name, count(*) OVER (PARTITION BY g.garage_id) AS discount_total
                FROM public.promo_garages pg
                LEFT JOIN garage g ON g.garage_id = pg.garage_id
                WHERE current_date >= DATE(start_date) AND current_date <= DATE(end_date);";

            using (var multi = await _uow.Connection.QueryMultipleAsync(sql))
            {
                var bookings = await multi.ReadAsync<BookingDetail>();

                var promos = await multi.ReadAsync<PromoNofications>();
                result.bookingDetails = bookings.ToList();
                result.promoNofications = promos.ToList();
            }

            return result;
        }

        public async Task<List<object>> GetMakeByGarageId(Guid garageId)
        {
            if (garageId == Guid.Empty)
            {

                var sql = "select distinct c.make  from garage_services gs \r\nleft join cars c on c.cars_id = gs.cars_id;";
                var makes = await _uow.Connection.QueryAsync<object>(sql);

                return makes.ToList();
            }
            else
            {

                var paramDicnary = new Dictionary<string, object>
                {
                    { $"@garageId", garageId }
                };
                var param = new DynamicParameters(paramDicnary);
                var sql = "select distinct c.make  from garage_services gs \r\nleft join cars c on c.cars_id = gs.cars_id \r\nwhere garage_id = @garageId\r\n;";
                var makes = await _uow.Connection.QueryAsync<object>(sql, param);

                return makes.ToList();
            }
        }

        public async Task<List<object>> GetModelByGarageId(Guid garageId, string make, int year)
        {
            if (garageId == Guid.Empty)
            {
                var paramDicnary = new Dictionary<string, object>
                {
                    { $"@make", make },
                    { $"@year", year }
                };
                var param = new DynamicParameters(paramDicnary);
                var sql = "select distinct c.model,c.cars_id from garage_services gs \r\nleft join cars c on c.cars_id = gs.cars_id where c.make = @make and c.\"year\" = @year \r\n;;";
                var models = await _uow.Connection.QueryAsync<object>(sql, param);

                return models.ToList();
            }
            else
            {


                var paramDicnary = new Dictionary<string, object>
                {
                    { $"@garageId", garageId },
                    { $"@make", make },
                    { $"@year", year }
                };
                var param = new DynamicParameters(paramDicnary);
                var sql = "select distinct c.model,c.cars_id from garage_services gs \r\nleft join cars c on c.cars_id = gs.cars_id \r\nwhere garage_id =  @garageId\r\nand c.make = @make and c.\"year\" = @year \r\n;";
                var models = await _uow.Connection.QueryAsync<object>(sql, param);

                return models.ToList();
            }
        }

        public async Task<List<object>> GetYearByGarageId(Guid garageId, string make)
        {
            if (garageId == Guid.Empty)
            {

                var paramDicnary = new Dictionary<string, object>
                {

                    { $"@make", make }
                };
                var param = new DynamicParameters(paramDicnary);
                var sql = "select distinct c.\"year\"  from garage_services gs \r\nleft join cars c on c.cars_id = gs.cars_id where c.make = @make;";
                var years = await _uow.Connection.QueryAsync<object>(sql, param);

                return years.ToList();
            }
            else
            {
                var paramDicnary = new Dictionary<string, object>
                {
                    { $"@garageId", garageId },
                    { $"@make", make }
                };
                var param = new DynamicParameters(paramDicnary);
                var sql = "select distinct c.\"year\"  from garage_services gs \r\nleft join cars c on c.cars_id = gs.cars_id \r\nwhere garage_id = @garageId\r\nand c.make = @make \r\n;";
                var years = await _uow.Connection.QueryAsync<object>(sql, param);

                return years.ToList();
            }
        }

        public async Task<List<object>> GetGarageServicesAsync(Guid carId)
        {

            var paramDicnary = new Dictionary<string, object>
                {
                    { $"@carId", carId }
                };
            var param = new DynamicParameters(paramDicnary);
            var sql = "select distinct service_code  from garage_services gs where cars_id = @carId;";
            var years = await _uow.Connection.QueryAsync<object>(sql, param);

            return years.ToList();
        }

        public async Task<List<object>> GetEstimateServiceAsync(Guid carId, List<ServiceCode> serviceCodes)
        {
            var paramDicnary = new Dictionary<string, object>
                {
                    { $"@p_car_id", carId },
                    { $"@t_services_code", JsonConvert.SerializeObject(serviceCodes) }
                };
            var param = new DynamicParameters(paramDicnary);

            var sql = " select * from func_estimate_garage_services(@p_car_id, @t_services_code::jsonb);";
            var result = await _uow.Connection.QueryAsync<object>(sql, param, commandType: CommandType.Text);

            return result.ToList();
        }



        public async Task<List<object>> GetGarageByEstimateAsync(double? latitude, double? longitude, Guid p_estimate_id)
        {

            var paramDictionary = new Dictionary<string, object>()
            {
                {"lat" , latitude == null ? 0 : latitude },
                {"lng" , longitude == null ? 0 : longitude },
                {"p_estimate_id", p_estimate_id}
            };
            var param = new DynamicParameters(paramDictionary);


            var sql = "select * from public.func_get_garage_by_estimate(@lat, @lng, @p_estimate_id)";

            var result = await _unitOfWork.Connection.QueryAsync<object>(sql, param, commandType: CommandType.Text);

            return result.ToList();
        }
    }
}
