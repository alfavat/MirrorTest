using Business.Constants;
using Business.Managers.Abstract;
using Business.Managers.Concrete;
using Entity.Dtos;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Threading.Tasks;
using UnitTest.DAL;
using UnitTest.Extra;
using Xunit;
namespace UnitTest.BLL
{
    public class TestUserQuestionAnswerService : TestUserQuestionAnswerDal
    {

        #region setup
        private readonly IUserQuestionAnswerService _userQuestionAnswerService;
        private readonly IQuestionAnswerService _questionAnswerService;
        private readonly IHttpContextAccessor _httpContext;
        public TestUserQuestionAnswerService()
        {
            var _mapper = new TestAutoMapper()._mapper;
            _userQuestionAnswerService = new UserQuestionAnswerManager(new UserQuestionAnswerAssistantManager(userQuestionAnswerDal, _mapper),_questionAnswerService,_httpContext, _mapper);
        }
        #endregion

        #region methods

        [Fact(DisplayName = "Add")]
        [Trait("UserQuestionAnswer", "Add")]
        public async Task ServiceShouldAddNewUserQuestionAnswerToList()
        {
            // arrange
            var userQuestionAnswer = new UserQuestionAnswerAddDto()
            {
                AnswerId = 1
            };
            // act
            var result = await _userQuestionAnswerService.Add(userQuestionAnswer);
            // assert
            Assert.NotNull(result);
            Assert.True(result.Success);
            var newUserQuestionAnswer = db.UserQuestionAnswers.FirstOrDefault(f => f.AnswerId == userQuestionAnswer.AnswerId);
            Assert.NotNull(newUserQuestionAnswer);
            Assert.Equal(newUserQuestionAnswer.CreatedAt.Date, DateTime.Now.Date);
            Assert.Equal(result.Message, Messages.Added);

        }

        #endregion
    }
}
