using Dapper;
using ESP.Cloud.BE.Core.BaseDL;
using ESP.Cloud.BE.Core.DL;
using ESP.Cloud.BE.Core.Model;
using ESP.Cloud.BE.Infrastructure.Repository.Base;
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

        public async Task<List<object>> GetMakeByGarageId(Guid garageId)
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

        public async Task<List<object>> GetModelByGarageId(Guid garageId, string make, int year)
        {
            var paramDicnary = new Dictionary<string, object>
                {
                    { $"@garageId", garageId },
                    { $"@make", make },
                    { $"@year", year }
                };
            var param = new DynamicParameters(paramDicnary);
            var sql = "select distinct c.model  from garage_services gs \r\nleft join cars c on c.cars_id = gs.cars_id \r\nwhere garage_id =  @garageId\r\nand c.make = @make and c.\"year\" = @year \r\n;";
            var models = await _uow.Connection.QueryAsync<object>(sql, param);

            return models.ToList();
        }

        public async Task<List<object>> GetYearByGarageId(Guid garageId, string make)
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
}
