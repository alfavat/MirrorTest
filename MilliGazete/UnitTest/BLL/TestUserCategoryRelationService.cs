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
    public class TestUserCategoryRelationService : TestUserCategoryRelationDal
    {

        #region setup
        private readonly IUserCategoryRelationService _userCategoryRelationService;
        public TestUserCategoryRelationService()
        {
            var _mapper = new TestAutoMapper()._mapper;
            _userCategoryRelationService = new UserCategoryRelationManager(
                new UserCategoryRelationAssistantManager(userCategoryRelationDal, _mapper), _mapper);
        }
        #endregion

        #region methods

        //[Theory(DisplayName = "GetListByUserId")]
        //[Trait("UserCategoryRelation", "Get")]
        //[InlineData(1)]
        //[InlineData(2)]
        //[InlineData(3)]
        //public async Task ServiceShouldReturnUserCategoryIdList(int id)
        //{
        //    // arrange
        //    // act
        //    var result = await _userCategoryRelationService.GetListByUserId(id);
        //    // assert
        //    Assert.NotNull(result);
        //    Assert.True(result.Success);
        //    Assert.Equal(result.Data.Count, db.UserCategoryRelation.Count(f => f.UserId == id));
        //}

        [Fact(DisplayName = "Update")]
        [Trait("UserCategoryRelation", "Edit")]
        public async Task ServiceShouldChangeUserCategoryRelationStatus()
        {
            // arrange
            var data = db.UserCategoryRelations.FirstOrDefault();
            var categoryIds = db.UserCategoryRelations.Where(f => f.UserId != data.UserId).Select(f => f.CategoryId).Take(4).ToList();
            // act
            var result = await _userCategoryRelationService.Update(new UserCategoryRelationUpdateDto
            {
                UserId = data.UserId,
                CategoryIds = categoryIds
            });
            // assert
            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.Equal(result.Message, Messages.Updated);
            Assert.Equal(4, db.UserCategoryRelations.Count(f => f.UserId == data.UserId));
        }
        #endregion
    }
}
