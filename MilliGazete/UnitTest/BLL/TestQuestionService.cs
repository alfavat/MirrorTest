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
    public class TestQuestionService : TestQuestionDal
    {

        #region setup
        private readonly IQuestionService _questionService;
        public TestQuestionService()
        {
            var _mapper = new TestAutoMapper()._mapper;
            _questionService = new QuestionManager(new QuestionAssistantManager(questionDal, _mapper), _mapper);
        }
        #endregion

        #region methods

        [Fact(DisplayName = "Add")]
        [Trait("Question", "Add")]
        public async Task ServiceShouldAddNewQuestionToList()
        {
            // arrange
            var question = new QuestionAddDto()
            {
                QuestionText = "Test add question",
                Active = true
            };
            // act
            var result = await _questionService.Add(question);
            // assert
            Assert.NotNull(result);
            Assert.True(result.Success);
            var newQuestion = db.Questions.FirstOrDefault(f => f.QuestionText == question.QuestionText);
            Assert.NotNull(newQuestion);
            Assert.Equal(newQuestion.CreatedAt.Date, DateTime.Now.Date);
            Assert.Equal(result.Message, Messages.Added);

        }

        [Fact(DisplayName = "Update")]
        [Trait("Question", "Edit")]
        public async Task ServiceShouldUpdateQuestion()
        {
            // arrange
            var question = db.Questions.FirstOrDefault(f => !f.Deleted);
            var dto = new QuestionUpdateDto
            {
                Id = question.Id,
                QuestionText = "Test update",
                Active = false
            };
            // act
            var result = await _questionService.Update(dto);
            // assert
            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.Equal(result.Message, Messages.Updated);
            var updatedQuestion = db.Questions.FirstOrDefault(f => f.Id == question.Id);
            Assert.Equal(updatedQuestion.QuestionText, dto.QuestionText);
            Assert.Equal(updatedQuestion.Active, dto.Active);
        }


        #endregion
    }
}
