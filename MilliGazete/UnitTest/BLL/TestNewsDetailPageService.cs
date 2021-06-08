using AutoMapper;
using Business.Managers.Abstract;
using Business.Managers.Concrete;
using UnitTest.DAL;
using UnitTest.Extra;
using Xunit;

namespace UnitTest.BLL
{
    public class TestNewsDetailPageService : TestNewsDal
    {
        #region setup
        private readonly INewsDetailPageService _NewsDetailPageService;
        private readonly IMapper _mapper;
        public TestNewsDetailPageService()
        {
            _mapper = new TestAutoMapper()._mapper;
            //NewsDal NewsDal, INewsTagDal newsTagDal, INewsCategoryDal newsCategoryDal, IMapper mapper
            //_NewsDetailPageService = new NewsDetailPageManager(new MainPageAssistantManager(newsDal, new TestNewsTagDal().newsTagDal, _mapper));
        }

        #endregion

       // #region methods

        //[Fact(DisplayName = "Get")]
        //[Trait("NewsDetailPage", "Get")]
        //public void ServiceShouldReturnNewsDetailPage()
        //{
        //    // arrange
        //    // act
        //    var result = _NewsDetailPageService.GetLastWeekMostViewedNews(5);
        //    // assert
        //    Assert.NotNull(result);
        //    Assert.True(result.Success);
        //    Assert.NotNull(result.Data);
        //    Assert.Equal(result.Data.WebsiteTitle, db.NewsDetailPage.FirstOrDefault().WebsiteTitle);
        //}

    }
}
