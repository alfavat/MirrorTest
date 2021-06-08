using Business.Constants;
using Business.Managers.Abstract;
using Business.Managers.Concrete;
using Entity.Dtos;
using Entity.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnitTest.DAL;
using UnitTest.Extra;
using Xunit;
namespace UnitTest.BLL
{
    public class TestNewsService : TestNewsDal
    {

        #region setup
        private readonly INewsService _newsService;
        public TestNewsService()
        {
            var _mapper = new TestAutoMapper()._mapper;
            _newsService = new NewsManager(new NewsAssistantManager(newsDal, _mapper),
                new NewsPositionAssistantManager(new TestNewsPositionService().newsPositionDal, _mapper),
                new TestNewsHelper().newsHelper, _mapper);
        }
        #endregion

        #region methods
        //[Theory(DisplayName = "PaginationWithNullQuery")]
        //[Trait("News", "Get")]
        //[InlineData(1)]
        //public void ServiceShouldReturnNewsListWithPagination(int page)
        //{
        //    // arrange
        //    var dto = new NewsPagingDto
        //    {
        //        Limit = 10,
        //        OrderBy = "Id",
        //        PageNumber = page
        //    };
        //    int total = 0;
        //    // act
        //    var result = _newsService.GetListByPaging(dto, out total);
        //    // assert
        //    Assert.NotNull(result);
        //    Assert.True(result.Success);
        //    Assert.True(result.Data.Count <= dto.Limit);
        //    Assert.Equal(total, db.News.Count(f => !f.Deleted && f.IsLastNews));
        //}

        //[Theory(DisplayName = "PaginationWithQuery")]
        //[Trait("News", "Get")]
        //[InlineData(1)]
        //public void ServiceShouldReturnNewsItemsWhichConstainsTheGivenQueryWithPagination(int page)
        //{
        //    // arrange
        //    var dto = new NewsPagingDto
        //    {
        //        Limit = 10,
        //        OrderBy = "Id",
        //        PageNumber = page,
        //        Query = "8"
        //    };
        //    int total = 0;
        //    // act
        //    var result = _newsService.GetListByPaging(dto, out total);
        //    // assert
        //    Assert.NotNull(result);
        //    Assert.True(result.Success);
        //    Assert.True(result.Data.Count <= dto.Limit);
        //}

        //[Fact(DisplayName = "GetList")]
        //[Trait("News", "Get")]
        //public async Task ServiceShouldReturnNewsList()
        //{
        //    // arrange
        //    // act
        //    var result = await _newsService.GetList();
        //    // assert
        //    Assert.NotNull(result);
        //    Assert.True(result.Success);
        //    Assert.NotNull(result.Data);
        //}

        //[Theory(DisplayName = "GetById")]
        //[Trait("News", "Get")]
        //[InlineData(1)]
        //[InlineData(2)]
        //[InlineData(3)]
        //public async Task ServiceShouldReturnNewsById(int id)
        //{
        //    // arrange
        //    // act
        //    var result = await _newsService.GetViewById(id);
        //    // assert
        //    Assert.NotNull(result);
        //    Assert.True(result.Success);
        //    Assert.Equal(result.Data.Title, db.News.FirstOrDefault(f => f.Id == id & !f.Deleted).Title);
        //}

        [Theory(DisplayName = "GetByIdError")]
        [Trait("News", "Get")]
        [InlineData(-1)]
        [InlineData(-2)]
        [InlineData(-3)]
        public async Task ServiceShouldReturnErrorForNotValidNewsId(int id)
        {
            // arrange
            // act
            var result = await _newsService.GetViewById(id);
            // assert
            Assert.NotNull(result);
            Assert.False(result.Success);
            Assert.Null(result.Data);
            Assert.Equal(result.Message, Messages.RecordNotFound);
        }

        [Fact(DisplayName = "Add")]
        [Trait("News", "Add")]
        public async Task ServiceShouldAddNewNewsToList()
        {
            // arrange
            var categories = new List<NewsCategoryAddDto> { new NewsCategoryAddDto { CategoryId = 6 } };
            var files = new List<NewsFileAddDto> { new NewsFileAddDto { FileId = 1, Description = "", NewsFileTypeEntityId = 1, Order = 1, VideoCoverFileId = null, Title = "test title" } };
            var positions = new List<NewsPositionAddDto> { new NewsPositionAddDto { Order = 1, PositionEntityId = 1, Value = true } };
            var propertires = new List<NewsPropertyAddDto> { new NewsPropertyAddDto { PropertyEntityId = 1, Value = false } };
            var relatedNews = new List<NewsRelatedNewsAddDto> { new NewsRelatedNewsAddDto { RelatedNewsId = 4 } };
            var tags = new List<NewsTagAddDto> { new NewsTagAddDto { TagId = 6 } };
            NewsAddDto data = new NewsAddDto
            {
                UserId = 1,
                HtmlContent = "test",
                IsDraft = false,
                // NewsAgencyEntityId = 1,
                NewsCategoryList = categories,
                NewsFileList = files,
                NewsPositionList = positions,
                NewsPropertyList = propertires,
                NewsRelatedNewsList = relatedNews,
                NewsTagList = tags,
                //NewsTypeEntityId = 1,
                PublishDate = null,
                PublishTime = null,
                SeoDescription = "test",
                SeoKeywords = "",
                SeoTitle = "",
                ShortDescription = "",
                SocialDescription = "",
                SocialTitle = "",
                Title = "test title",
                Url = "test url"
            };
            // act
            var result = await _newsService.Add(data);
            // assert
            Assert.NotNull(result);
            Assert.True(result.Success);
            var newNews = db.News.FirstOrDefault(f => f.SeoKeywords == data.SeoKeywords);
            Assert.NotNull(newNews);
            Assert.Equal(newNews.CreatedAt.Date, DateTime.Now.Date);
            Assert.False(newNews.Deleted);
            Assert.Equal(result.Message, Messages.Added);

            Assert.Equal(newNews.ShortDescription, data.ShortDescription);
            Assert.Equal(newNews.Url, data.Url);
            Assert.Null(newNews.UpdateUserId);
            Assert.Equal(newNews.AddUserId, data.UserId);

            Assert.True(newNews.IsLastNews);
            Assert.True(!db.News.Any(f => !f.Deleted && f.Id != newNews.Id && f.IsLastNews));
            var counterEntities = db.Entity.Where(f => f.EntityGroupId == (int)EntityGroupType.CounterEntities).Select(g => g.Id).ToList();
            counterEntities.ForEach(entityId =>
            {
                Assert.True(db.NewsCounter.Any(f => f.CounterEntityId == entityId && f.NewsId == newNews.Id && f.Value == 0));
            });
        }

        [Fact(DisplayName = "ChangeStatus")]
        [Trait("News", "Edit")]
        public async Task ServiceShouldChangeNewsStatus()
        {
            // arrange
            var data = db.News.FirstOrDefault();
            var status = new ChangeActiveStatusDto()
            {
                Active = !data.Active,
                Id = data.Id
            };
            // act
            var result = await _newsService.ChangeActiveStatus(status);
            // assert
            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.Equal(result.Message, Messages.Updated);
            Assert.Equal(status.Active, db.News.FirstOrDefault(f => f.Id == data.Id).Active);
        }

        [Fact(DisplayName = "ChangeStatusError")]
        [Trait("News", "Edit")]
        public async Task ServiceShouldNotChangeNewsStatusIfNewsIdIsWrong()
        {
            // arrange
            var status = new ChangeActiveStatusDto()
            {
                Active = true,
                Id = -1
            };
            // act
            var result = await _newsService.ChangeActiveStatus(status);
            // assert
            Assert.NotNull(result);
            Assert.False(result.Success);
            Assert.Equal(result.Message, Messages.RecordNotFound);
        }

        [Fact(DisplayName = "Update")]
        [Trait("News", "Edit")]
        public async Task ServiceShouldUpdateNews()
        {
            // arrange
            var oldNews = db.News.FirstOrDefault(f => !f.Deleted);
            var id = oldNews.Id;
            var categories = new List<NewsCategoryAddDto> { new NewsCategoryAddDto { CategoryId = 6 } };
            var files = new List<NewsFileAddDto> { new NewsFileAddDto { FileId = 1, Description = "", NewsFileTypeEntityId = 1, Order = 1, VideoCoverFileId = null, Title = "test title" } };
            var positions = new List<NewsPositionAddDto> { new NewsPositionAddDto { Order = 1, PositionEntityId = 1, Value = true } };
            var propertires = new List<NewsPropertyAddDto> { new NewsPropertyAddDto { PropertyEntityId = 1, Value = false } };
            var relatedNews = new List<NewsRelatedNewsAddDto> { new NewsRelatedNewsAddDto { RelatedNewsId = 4 } };
            var tags = new List<NewsTagAddDto> { new NewsTagAddDto { TagId = 6 } };
            NewsAddDto dto = new NewsAddDto
            {
                NewsId = id,
                UserId = 1,
                HtmlContent = "add test",
                IsDraft = true,
                NewsAgencyEntityId = null,
                NewsCategoryList = categories,
                NewsFileList = files,
                NewsPositionList = positions,
                NewsPropertyList = propertires,
                NewsRelatedNewsList = relatedNews,
                NewsTagList = tags,
                NewsTypeEntityId = 1,
                PublishDate = "2020-01-02",
                PublishTime = "10:30",
                SeoDescription = "add test",
                SeoKeywords = "add",
                SeoTitle = "aa seo title",
                ShortDescription = "add",
                SocialDescription = "add",
                SocialTitle = "add",
                Title = "test add title",
                Url = "test add url"
            };

            // act
            var result = await _newsService.Add(dto);
            // assert
            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.Equal(result.Message, Messages.Added);
            var updatedNews = db.News.FirstOrDefault(f => f.SeoTitle == dto.SeoTitle);
            Assert.Equal(updatedNews.ShortDescription, dto.ShortDescription);
            Assert.Equal(updatedNews.Url, dto.Url);
            Assert.Equal(updatedNews.UpdateUserId, dto.UserId);
            Assert.Equal(updatedNews.AddUserId, oldNews.AddUserId);
            if (DateTime.TryParse(dto.PublishDate, out DateTime dt))
                Assert.Equal(updatedNews.PublishDate, dt);

            if (TimeSpan.TryParse(dto.PublishTime, out TimeSpan sp))
                Assert.Equal(updatedNews.PublishTime, sp);

            Assert.True(updatedNews.IsLastNews);
            Assert.True(!db.News.Any(f => !f.Deleted && f.Id != updatedNews.Id && f.IsLastNews));

            var counterEntities = db.Entity.Where(f => f.EntityGroupId == (int)EntityGroupType.CounterEntities).Select(g => g.Id).ToList();
            counterEntities.ForEach(entityId =>
            {

                Assert.True(db.NewsCounter.Any(f => f.CounterEntityId == entityId && f.NewsId == updatedNews.Id));
            });
        }

        #endregion
    }
}
