using AutoMapper;
using Business.Constants;
using Business.Managers.Abstract;
using Business.Managers.Concrete;
using Entity.Dtos;
using System.Linq;
using System.Threading.Tasks;
using UnitTest.DAL;
using UnitTest.Extra;
using Xunit;
namespace UnitTest.BLL
{
    public class TestUserOperationClaimService : TestUserOperationClaimDal
    {
        private readonly IMapper _mapper;
        #region setup
        private readonly IUserOperationClaimService _userOperationClaimService;
        public TestUserOperationClaimService()
        {
            _mapper = new TestAutoMapper()._mapper;
            _userOperationClaimService = new UserOperationClaimManager(
                new UserOperationClaimAssistantManager(userOperationClaimDal, _mapper));
        }

        #endregion

        #region methods

        [Theory(DisplayName = "GetByUserId")]
        [Trait("UserOperationClaim", "Get")]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async Task ServiceShouldReturnUserOperationClaimById(int userId)
        {
            // arrange
            // act
            var result = await _userOperationClaimService.GetByUserId(userId);
            // assert
            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.Equal(result.Data.Count, db.UserOperationClaim.Count(f => f.UserId == userId));
        }

        [Theory(DisplayName = "GetByUserIdError")]
        [Trait("UserOperationClaim", "Get")]
        [InlineData(-1)]
        [InlineData(-2)]
        [InlineData(-3)]
        public async Task ServiceShouldReturnErrorForNotValidUserOperationClaimId(int userId)
        {
            // arrange
            // act
            var result = await _userOperationClaimService.GetByUserId(userId);
            // assert
            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.Empty(result.Data);
        }

        [Fact(DisplayName = "UpdateAndAdd")]
        [Trait("UserOperationClaim", "Update")]
        public async Task ServiceShouldAddNewUserOperationClaimToList()
        {
            // arrange
            var data = new UserOperationClaimUpdateDto()
            {
                UserId = 1,
                OperationClaimIds = new System.Collections.Generic.List<int> { 15, 16, 12 }
            };
            // act
            var result = await _userOperationClaimService.Update(data);
            // assert
            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.Contains(db.UserOperationClaim, f => f.OperationClaimId == 15);
            Assert.Equal(db.UserOperationClaim.Count(f => f.UserId == data.UserId), data.OperationClaimIds.Count());
            Assert.Equal(result.Message, Messages.Updated);
        }

        #endregion
    }
}
