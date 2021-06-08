using Business.Managers.Abstract;
using Business.Managers.Concrete;
using UnitTest.DAL;
using UnitTest.Extra;
namespace UnitTest.BLL
{
    public class TestNewsPositionService : TestNewsPositionDal
    {

        #region setup
        public readonly INewsPositionService _newsPositionService;
        public TestNewsPositionService()
        {
            var _mapper = new TestAutoMapper()._mapper;
            _newsPositionService = new NewsPositionManager(new NewsPositionAssistantManager(newsPositionDal, _mapper), _mapper);
        }
        #endregion

        #region methods

        //[Fact(DisplayName = "UpdatePositions")]
        //[Trait("NewsPosition", "Edit")]
        //public void ServiceShouldUpdateNewsPositions()
        //{
        //    // arrange
        //    var list = db.NewsPosition.OrderBy(f => f.Order).Take(3);
        //    var data = list.Select(f => new NewsPositionUpdateDto
        //    {
        //        NewsId = f.NewsId,
        //        Order = f.Order + 1,
        //        PositionEntityId = 1
        //    }).ToList();
        //    // act
        //    var result = _newsPositionService.UpdateNewsPositionOrders(data);
        //    // assert
        //    Assert.NotNull(result);
        //    Assert.True(result.Success);
        //    Assert.Equal(result.Message, Messages.Updated);
        //    var ids = list.Select(f => f.Id).ToList();
        //    var entityIds = list.Select(f => f.Id).ToList();
        //    var d = db.NewsPosition.Where(f => !ids.Contains(f.Id) && 5 == f.PositionEntityId);
        //    Assert.True(d.All(f => f.Order == 0));
        //}


        #endregion
    }
}
