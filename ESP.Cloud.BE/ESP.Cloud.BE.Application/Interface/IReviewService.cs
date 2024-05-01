using ESP.Cloud.BE.Application.Dto;
using ESP.Cloud.BE.Application.Interface.Base;
using ESP.Cloud.BE.Core.Model;

namespace ESP.Cloud.BE.Application.Interface
{
    public interface IReviewService : IBaseService<GarageReviewsEntity, CreateReviewDto, UpdateReviewDto>
    {

    }
}
