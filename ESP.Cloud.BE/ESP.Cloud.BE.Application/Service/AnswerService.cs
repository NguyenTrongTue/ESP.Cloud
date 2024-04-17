using AutoMapper;
using ESP.Cloud.BE.Application.Dto;
using ESP.Cloud.BE.Application.Interface;
using ESP.Cloud.BE.Application.Service.Base;
using ESP.Cloud.BE.Core.DL;
using ESP.Cloud.BE.Core.Model;

namespace ESP.Cloud.BE.Application.Service
{
    public class AnswerService : BaseService<AnswerEntity, CreateAnswerDto, UpdateAnswerDto>, IAnswerService
    {
        private readonly IAnswerDL _answerDL;
        public AnswerService(IAnswerDL answerDL, IMapper mapper) : base(answerDL, mapper)
        {
            _answerDL = answerDL;
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


    }
}
