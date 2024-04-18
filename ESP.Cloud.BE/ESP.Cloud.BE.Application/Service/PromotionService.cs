using AutoMapper;
using ESP.Cloud.BE.Application.Interface;
using ESP.Cloud.BE.Application.Service.Base;
using ESP.Cloud.BE.Core.DL;
using ESP.Cloud.BE.Core.Model;

namespace ESP.Cloud.BE.Application.Service
{
    public class PromotionService : BaseReadService<PromotionEntity>, IPromotionService
    {
        private readonly IPromotionDL _promotionDL;
        public PromotionService(IPromotionDL promotionDL, IMapper mapper) : base(promotionDL, mapper)
        {
            _promotionDL = promotionDL;
        }

        public async Task<List<object>> GetListFilteredPromotions(int? timeFilterType, string? carName, Guid? garageId)
        {
            var result = await _promotionDL.GetListFilteredPromotions(timeFilterType, carName, garageId);

            return result;
        }
    }
}
