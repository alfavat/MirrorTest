using Business.Managers.Abstract;
using Business.Managers.Concrete;
using Entity.Enums;
using System.Linq;
using System.Threading.Tasks;
using UnitTest.DAL;
using UnitTest.Extra;
using Xunit;
namespace UnitTest.BLL
{
    public class TestEntityService : TestEntityDal
    {

        #region setup
        private readonly IEntityService _entityService;
        public TestEntityService()
        {
            var _mapper = new TestAutoMapper()._mapper;
            _entityService = new EntityManager(new EntityAssistantManager(entityDal, _mapper));
        }
        #endregion

        #region methods

        [Fact(DisplayName = "GetNewsTypeEntities")]
        [Trait("Entity", "Get")]
        public async Task ServiceShouldReturnNewsTypeEntities()
        {
            // arrange
            // act
            var result = await _entityService.GetNewsTypeEntities();
            // assert
            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.Equal(result.Data.Count, db.Entities.Count(f => f.EntityGroupId == (int)EntityGroupType.NewsTypeEntities));
        }

        [Fact(DisplayName = "GetCounterEntities")]
        [Trait("Entity", "Get")]
        public async Task ServiceShouldReturnCounterEntities()
        {
            // arrange
            // act
            var result = await _entityService.GetCounterEntities();
            // assert
            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.Equal(result.Data.Count, db.Entities.Count(f => f.EntityGroupId == (int)EntityGroupType.CounterEntities));
        }

        [Fact(DisplayName = "GetPositionEntities")]
        [Trait("Entity", "Get")]
        public async Task ServiceShouldReturnPositionEntities()
        {
            // arrange
            // act
            var result = await _entityService.GetPositionEntities();
            // assert
            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.Equal(result.Data.Count, db.Entities.Count(f => f.EntityGroupId == (int)EntityGroupType.PositionEntities));
        }
        #endregion
    }
}
