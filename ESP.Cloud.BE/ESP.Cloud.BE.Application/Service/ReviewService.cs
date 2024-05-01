using AutoMapper;
using ESP.Cloud.BE.Application.Dto;
using ESP.Cloud.BE.Application.Interface;
using ESP.Cloud.BE.Application.Service.Base;
using ESP.Cloud.BE.Core.DL;
using ESP.Cloud.BE.Core.Model;

namespace ESP.Cloud.BE.Application.Service
{
    public class ReviewService : BaseService<GarageReviewsEntity, CreateReviewDto, UpdateReviewDto>, IReviewService
    {
        private readonly IReviewDL _reviewDL;
        public ReviewService(IReviewDL reviewDL, IMapper mapper) : base(reviewDL, mapper)
        {
            _reviewDL = reviewDL;
        }


        public override async Task<GarageReviewsEntity> MapCreateDtoToEntity(CreateReviewDto createBookingDto)
        {
            var questions = _mapper.Map<GarageReviewsEntity>(createBookingDto);
            questions.garage_reviews_id = Guid.NewGuid();

            return questions;
        }

        public override Task<GarageReviewsEntity> MapUpdateDtoToEntity(Guid id, UpdateReviewDto entityUpdateDto)
        {
            throw new NotImplementedException();
        }


    }
}
