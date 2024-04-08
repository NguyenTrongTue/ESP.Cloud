using AutoMapper;
using ESP.Cloud.BE.Application.Dto;
using ESP.Cloud.BE.Application.Interface;
using ESP.Cloud.BE.Application.Service.Base;
using ESP.Cloud.BE.Core.DL;
using ESP.Cloud.BE.Core.Model;

namespace ESP.Cloud.BE.Application.Service
{
    public class QuestionsService : BaseService<QuestionsEntity, CreateQuestionsDto, UpdateQuestionsDto>, IQuestionsService
    {
        private readonly IQuestionsDL _questionDL;
        public QuestionsService(IQuestionsDL questionDL, IMapper mapper) : base(questionDL, mapper)
        {
            _questionDL = questionDL;
        }

        public async Task<List<object>> GetQuestionPopular()
        {
            var result = await _questionDL.GetQuestionPopular();

            return result;
        }

        public async Task<List<object>> GetQuestionByCarAsync(Guid carId)
        {
            var result = await _questionDL.GetQuestionByCarAsync(carId);

            return result;
        }

        public async Task<List<object>> GetQuestionByMakeAsync(string make)
        {
            var result = await _questionDL.GetQuestionByMakeAsync(make);

            return result;
        }

        public async Task<List<object>> GetQuestionByModelAsync(string make, int year, string model)
        {
            var result = await _questionDL.GetQuestionByModelAsync(make, year, model);

            return result;
        }

        public async Task<List<object>> GetQuestionByYearAsync(string make, int year)
        {
            var result = await _questionDL.GetQuestionByYearAsync(make, year);

            return result;
        }

        public override async Task<QuestionsEntity> MapCreateDtoToEntity(CreateQuestionsDto createBookingDto)
        {
            var questions = _mapper.Map<QuestionsEntity>(createBookingDto);
            questions.questions_id = Guid.NewGuid();

            return questions;
        }

        public override Task<QuestionsEntity> MapUpdateDtoToEntity(Guid id, UpdateQuestionsDto entityUpdateDto)
        {
            throw new NotImplementedException();
        }

        public async Task<List<object>> SearchQuestionByTitleAsync(string title)
        {
            var result = await _questionDL.SearchQuestionByTitleAsync(title);

            return result;
        }
    }
}
