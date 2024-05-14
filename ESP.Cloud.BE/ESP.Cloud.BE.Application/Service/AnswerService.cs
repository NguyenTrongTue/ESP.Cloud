using AutoMapper;
using ESP.Cloud.BE.Application.Dto;
using ESP.Cloud.BE.Application.Interface;
using ESP.Cloud.BE.Application.Service.Base;
using ESP.Cloud.BE.Core.DL;
using ESP.Cloud.BE.Core.Model;
using ESP.Cloud.BE.Infrastructure;
using ESP.Cloud.BE.Model;
using Microsoft.AspNetCore.SignalR;
using System.Text.Json;

namespace ESP.Cloud.BE.Application.Service
{
    public class AnswerService : BaseService<AnswerEntity, CreateAnswerDto, UpdateAnswerDto>, IAnswerService
    {
        private readonly IAnswerDL _answerDL;
        private readonly INotificationDL _notificationDL;
        private readonly IHubContext<NotificationsHub, INotificationClient> _context;
        public AnswerService(IAnswerDL answerDL, IMapper mapper, IHubContext<NotificationsHub, INotificationClient> context, INotificationDL notificationDL) : base(answerDL, mapper)
        {
            _answerDL = answerDL;
            _context = context;
            _notificationDL = notificationDL;
        }

        public async Task<Dictionary<string, object>> GetAnswerByQuestionIdAsync(Guid questionId)
        {

            var result = await _answerDL.GetAnswerByQuestionIdAsync(questionId);

            return result;
        }

        public async Task<List<object>> GetAnswerRecently(string make, string model, int year)
        {

            var result = await _answerDL.GetAnswerRecently(make, model, year);

            return result;
        }

        public override async Task<AnswerEntity> MapCreateDtoToEntity(CreateAnswerDto createBookingDto)
        {
            var questions = _mapper.Map<AnswerEntity>(createBookingDto);
            questions.answers_id = Guid.NewGuid();

            return questions;
        }

        public override Task<AnswerEntity> MapUpdateDtoToEntity(Guid id, UpdateAnswerDto entityUpdateDto)
        {
            throw new NotImplementedException();
        }


        protected override async Task AfterInsertEntity(CreateAnswerDto entity)
        {
            try
            {
                if (entity.user_id != entity.user_id_of_question)
                {
                    var notifications = new List<Notifications>();
                    var notification = new Notifications()
                    {
                        user_notifications_id = Guid.NewGuid(),
                        user_id = entity.user_id_of_question,
                        refid = entity.questions_id,
                        type = 3,
                        unread = true,
                        description = $"{entity.user_name} phản hồi câu hỏi của bạn",
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
