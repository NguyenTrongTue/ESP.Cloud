using ESP.Cloud.BE.Core.BaseDL.Repository;
using ESP.Cloud.BE.Core.Model;

namespace ESP.Cloud.BE.Core.DL
{
    public interface IPromotionDL : IBaseReadRepository<PromotionEntity>
    {
        Task<List<object>> GetListFilteredPromotions(int? timeFilterType, string? carName, Guid? garageId);
    }

}
