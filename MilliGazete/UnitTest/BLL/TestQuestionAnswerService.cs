using Business.Constants;
using Business.Managers.Abstract;
using Business.Managers.Concrete;
using Entity.Dtos;
using System;
using System.Linq;
using System.Threading.Tasks;
using UnitTest.DAL;
using UnitTest.Extra;
using Xunit;
namespace UnitTest.BLL
{
    public class TestQuestionAnswerService : TestQuestionAnswerDal
    {

        #region setup
        private readonly IQuestionAnswerService _questionAnswerService;
        public TestQuestionAnswerService()
        {
            var _mapper = new TestAutoMapper()._mapper;
            _questionAnswerService = new QuestionAnswerManager(new QuestionAnswerAssistantManager(questionAnswerDal, _mapper), _mapper);
        }
        #endregion

        #region methods

        [Fact(DisplayName = "Add")]
        [Trait("QuestionAnswer", "Add")]
        public async Task ServiceShouldAddNewQuestionAnswerToList()
        {
            // arrange
            var questionAnswer = new QuestionAnswerAddDto()
            {
                Answer = "Test add questionAnswer",
                QuestionId = 1
            };
            // act
            var result = await _questionAnswerService.Add(questionAnswer);
            // assert
            Assert.NotNull(result);
            Assert.True(result.Success);
            var newQuestionAnswer = db.QuestionAnswers.FirstOrDefault(f => f.Answer == questionAnswer.Answer);
            Assert.NotNull(newQuestionAnswer);
            Assert.Equal(result.Message, Messages.Added);

        }

        [Fact(DisplayName = "Update")]
        [Trait("QuestionAnswer", "Edit")]
        public async Task ServiceShouldUpdateQuestionAnswer()
        {
            // arrange
            var questionAnswer = db.QuestionAnswers.FirstOrDefault(f => !f.Deleted);
            var dto = new QuestionAnswerUpdateDto
            {
                Id = questionAnswer.Id,
                Answer = "Test update",
                QuestionId = 1
            };
            // act
            var result = await _questionAnswerService.Update(dto);
            // assert
            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.Equal(result.Message, Messages.Updated);
            var updatedQuestionAnswer = db.QuestionAnswers.FirstOrDefault(f => f.Id == questionAnswer.Id);
            Assert.Equal(updatedQuestionAnswer.Answer, dto.Answer);
            Assert.Equal(updatedQuestionAnswer.QuestionId, dto.QuestionId);
        }


        #endregion
    }
}
