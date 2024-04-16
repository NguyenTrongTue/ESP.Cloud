using ESP.Cloud.BE.Application.Interface.Base;
using ESP.Cloud.BE.Core.Model;

namespace ESP.Cloud.BE.Application.Interface
{
    public interface IPromotionService : IBaseReadService<PromotionEntity>
    {
        Task<List<object>> GetListFilteredPromotions(int? timeFilterType, string? carName, Guid? garageId);
    }
}
