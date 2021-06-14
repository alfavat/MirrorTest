using Business.Constants;
using Business.Managers.Abstract;
using Business.Managers.Concrete;
using Core.Utilities.Helper.Abstract;
using Entity.Dtos;
using System;
using System.Linq;
using System.Threading.Tasks;
using UnitTest.DAL;
using UnitTest.Extra;
using Xunit;
namespace UnitTest.BLL
{
    public class TestSubscriptionService : TestSubscriptionDal
    {

        #region setup
        private readonly ISubscriptionService _subscriptionService;
        private readonly IOptionAssistantService _optionAssistantService;
        private readonly IMailHelper _mailHelper;
        public TestSubscriptionService()
        {
            var _mapper = new TestAutoMapper()._mapper;
            _subscriptionService = new SubscriptionManager(new SubscriptionAssistantManager(subscriptionDal, _mailHelper, _optionAssistantService, _mapper), _mapper);
        }
        #endregion

        #region methods
        [Theory(DisplayName = "GetById")]
        [Trait("Subscription", "Get")]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async Task ServiceShouldReturnSubscriptionById(int id)
        {
            // arrange
            // act
            var result = await _subscriptionService.GetById(id);
            // assert
            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.Equal(result.Data.Email, db.Subscriptions.FirstOrDefault(f => f.Id == id).Email);
        }
        [Theory(DisplayName = "GetByIdError")]
        [Trait("Subscription", "Get")]
        [InlineData(-1)]
        [InlineData(-2)]
        [InlineData(-3)]
        public async Task ServiceShouldReturnErrorForNotValidSubscriptionId(int id)
        {
            // arrange
            // act
            var result = await _subscriptionService.GetById(id);
            // assert
            Assert.NotNull(result);
            Assert.False(result.Success);
            Assert.Null(result.Data);
            Assert.Equal(result.Message, Messages.RecordNotFound);
        }

        [Fact(DisplayName = "Add")]
        [Trait("Subscription", "Add")]
        public async Task ServiceShouldAddNewSubscriptionToList()
        {
            // arrange
            var subscription = new SubscriptionAddDto()
            {
                Address = "Test Address",
                Email = "shakibaitADDTEST@gmail.com",
                DistrictId = 423,
                CityId = 34,
                Description = "Description Add Test",
                FullName = "Saeid Shakiba",
                Phone = "05524353057"
            };
            // act
            var result = await _subscriptionService.Add(subscription);
            // assert
            Assert.NotNull(result);
            Assert.True(result.Success);
            var newSubscription = db.Subscriptions.FirstOrDefault(f => f.Email == subscription.Email);
            Assert.NotNull(newSubscription);
            Assert.Equal(newSubscription.CreatedAt.Date, DateTime.Now.Date);
            Assert.Equal(result.Message, Messages.Added);

        }
        #endregion
    }
}
