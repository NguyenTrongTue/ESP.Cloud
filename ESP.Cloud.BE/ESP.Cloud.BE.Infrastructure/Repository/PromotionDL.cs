using Dapper;
using ESP.Cloud.BE.Core.BaseDL;
using ESP.Cloud.BE.Core.DL;
using ESP.Cloud.BE.Core.Enum;
using ESP.Cloud.BE.Core.Model;
using System.Data;

namespace ESP.Cloud.BE.Infrastructure.Repository
{
    public class PromotionDL : BaseReadRepository<PromotionEntity>, IPromotionDL
    {
        #region Fields
        protected readonly IUnitOfWork _uow;
        #endregion

        #region Constructor
        public PromotionDL(IUnitOfWork entityOfWork) : base(entityOfWork)
        {
            _uow = entityOfWork;
        }
        #endregion

        public async Task<List<object>> GetListFilteredPromotions(int? timeFilterType, string? carName, Guid? garageId)
        {
            var sql = "select * from public.promotions where 1 = 1";

            var paramDictionary = new Dictionary<string, object>();

            if (!string.IsNullOrEmpty(carName))
            {
                sql += $" AND (UPPER(CAST(summary AS TEXT)) LIKE '%{carName.ToUpper()}%' OR UPPER(CAST(title AS TEXT)) LIKE '%{carName.ToUpper()}%' OR UPPER(CAST(description AS TEXT)) LIKE '%{carName.ToUpper()}%')";
            }

            if (garageId != null)
            {
                sql += " AND garage_id = @garageId";
                paramDictionary.Add("@garageId", garageId);
            }
            var param = new DynamicParameters(paramDictionary);

            if (timeFilterType != null)
            {
                switch (timeFilterType)
                {
                    case (int)TimeFilterTypeEnum.Day:
                        sql += " AND (EXTRACT(DAY FROM start_date) = EXTRACT(DAY FROM NOW()) OR EXTRACT(DAY FROM end_date) = EXTRACT(DAY FROM NOW()))";
                        break;
                    case (int)TimeFilterTypeEnum.Month:
                        sql += " AND (EXTRACT(MONTH FROM start_date) = EXTRACT(MONTH FROM NOW()) OR EXTRACT(MONTH FROM end_date) = EXTRACT(MONTH FROM NOW()))";
                        break;
                    case (int)TimeFilterTypeEnum.Year:
                        sql += " AND (EXTRACT(YEAR FROM start_date) = EXTRACT(YEAR FROM NOW()) OR EXTRACT(YEAR FROM end_date) = EXTRACT(YEAR FROM NOW()))";
                        break;
                }
            }

            var result = await _uow.Connection.QueryAsync<object>(sql, param, commandType: CommandType.Text);

            return result.ToList();
        }
    }
}
