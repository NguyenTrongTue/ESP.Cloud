using AutoMapper;
using ESP.Cloud.BE.Application.Dto;
using ESP.Cloud.BE.Application.Dto.CarReview;
using ESP.Cloud.BE.Application.Interface;
using ESP.Cloud.BE.Application.Param;
using ESP.Cloud.BE.Application.Service.Base;
using ESP.Cloud.BE.Core.DL;
using ESP.Cloud.BE.Core.Model;
using ESP.Cloud.BE.Infrastructure;
using ESP.Cloud.BE.Infrastructure.Repository;
using ESP.Cloud.BE.Model;
using Microsoft.AspNetCore.SignalR;
using System.Text.Json;

namespace ESP.Cloud.BE.Application.Service
{
    public class CarReviewService : BaseService<CarReviewEntity, CreateCarReviewDto, UpdateCarReviewDto>, ICarReviewService
    {
        private readonly ICarReviewDL _questionDL;
        private readonly IUserLikesDL _userLikesDL;
        private readonly INotificationDL _notificationDL;
        private readonly IHubContext<NotificationsHub, INotificationClient> _context;
        public CarReviewService(ICarReviewDL questionDL, IUserLikesDL userLikesDL, IMapper mapper, IHubContext<NotificationsHub, INotificationClient> context, INotificationDL notificationDL) : base(questionDL, mapper)
        {
            _questionDL = questionDL;
            _userLikesDL = userLikesDL;
            _context = context;
            _notificationDL = notificationDL;
        }

        public async Task<CarReviewData> GetQuestionPopular(Guid userId)
        {
            var result = await _questionDL.GetQuestionPopular(userId);

            return result;
        }

        public async Task<List<object>> GetQuestionByCarAsync(Guid carId)
        {
            var result = await _questionDL.GetQuestionByCarAsync(carId);

            return result;
        }

        public async Task<CarReviewData> GetQuestionByMakeAsync(Guid userId, string make)
        {
            var result = await _questionDL.GetQuestionByMakeAsync(make, userId);

            return result;
        }

        public async Task<CarReviewData> GetQuestionByModelAsync(string make, string model, Guid userId)
        {
            var result = await _questionDL.GetQuestionByModelAsync(make, model, userId);

            return result;
        }


        public override async Task<CarReviewEntity> MapCreateDtoToEntity(CreateCarReviewDto createBookingDto)
        {
            var questions = _mapper.Map<CarReviewEntity>(createBookingDto);
            questions.car_review_id = Guid.NewGuid();

            return questions;
        }

        public override Task<CarReviewEntity> MapUpdateDtoToEntity(Guid id, UpdateCarReviewDto entityUpdateDto)
        {
            throw new NotImplementedException();
        }

        public async Task<List<object>> SearchQuestionByTitleAsync(string title)
        {
            var result = await _questionDL.SearchQuestionByTitleAsync(title);

            return result;
        }

        public async Task<CarReviewOverviewRatingDto> GetOverviewRatingAsync(string? make = "")
        {
            var datas = await _questionDL.GetOverviewRatingAsync(make);

            var result = new CarReviewOverviewRatingDto();


            if (datas.Count > 0)
            {
                foreach (var item in datas)
                {
                    if (item.rating == 5)
                    {
                        result.five_stars += item.total_rating;
                    }
                    else if (item.rating == 4)
                    {
                        result.four_stars += item.total_rating;
                    }
                    else if (item.rating == 3)
                    {
                        result.three_stars += item.total_rating;
                    }
                    else if (item.rating == 2)
                    {
                        result.two_stars += item.total_rating;
                    }
                    else if (item.rating == 1)
                    {
                        result.one_stars += item.total_rating;
                    }

                    result.total_rating += item.total_rating;
                }

                result.avg_rating = Math.Round((double)((result.five_stars * 5) + (result.four_stars * 4) + (result.three_stars * 3) + (result.two_stars * 2) + result.one_stars) / result.total_rating, 1);


                result.cars_id = datas[0].cars_id;
                result.make = datas[0].make;
                result.model = datas[0].model;
                result.year = datas[0].year;
            }

            return result;
        }

        public async Task LikeOrUnLikeAsync(LikeOrUnLikeParam param)
        {

            try
            {
                if (param.liked == false)
                {
                    await _userLikesDL.DeteleUserLikes(param.car_review_id, param.user_id);
                }
                else
                {
                    var entity = new UserLikesEntity()
                    {

                        user_likes_id = Guid.NewGuid(),
                        car_review_id = param.car_review_id,
                        user_id = param.user_id,
                        created_time = DateTime.Now
                    };

                    await _userLikesDL.InsertAsync(entity);
                    await SendNotificationWhenLike(param);
                }
            }
            catch (Exception)
            {
                throw;
            }


        }
        /// <summary>
        /// Hàm gửi thông báo về cho user khi có người khác like bài review của người đó
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// Created by: nttue 04/05/2024
        private async Task SendNotificationWhenLike(LikeOrUnLikeParam entity)
        {
            try
            {
                if (entity.user_id != entity.user_id_of_car_review)
                {
                    var notifications = new List<Notifications>();
                    var notification = new Notifications()
                    {
                        user_notifications_id = Guid.NewGuid(),
                        user_id = entity.user_id_of_car_review,
                        type = 2,
                        unread = true,
                        description = $"{entity.fullname} vừa thích bài review xe của bạn",
                    };

                    notifications.Add(notification);
                    if (notifications.Count > 0)
                    {
                        var jsonData = JsonSerializer.Serialize(notifications);
                        await _context.Clients.All.ReceiveNotification(jsonData);
                        await _notificationDL.InsertBatchAsync(notifications);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
