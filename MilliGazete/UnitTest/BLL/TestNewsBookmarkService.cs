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
    public class TestNewsBookmarkService : TestNewsBookmarkDal
    {
        private readonly IMapper _mapper;

        #region setup
        private readonly INewsBookmarkService _newsBookmarkService;
        private readonly IFileAssistantService _fileAssistantService;
        private readonly IBaseService _baseService;
        public TestNewsBookmarkService()
        {
            _mapper = new TestAutoMapper()._mapper;
            _newsBookmarkService = new NewsBookmarkManager(new NewsBookmarkAssistantManager(newsBookmarkDal, _mapper), _mapper,
                new NewsAssistantManager(new TestNewsDal().newsDal, _fileAssistantService , _mapper,_baseService), new TestBaseService()._baseService);
        }

        #endregion

        #region methods

        [Fact(DisplayName = "GetByNewsUrl")]
        [Trait("NewsBookmark", "Get")]
        public async Task ServiceShouldReturnNewsBookmarksWhichTheyBelongToRequestedUserAndNewsUrlEqualsToRequestedUrl()
        {
            // arrange
            string url = db.News.Where(f => !f.Deleted).Select(f => f.Url).FirstOrDefault();
            // act
            var result = await _newsBookmarkService.GetByNewsUrl(url);
            // assert
            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.NotNull(result.Data);
            Assert.Equal(result.Data.NewsId, db.NewsBookmarks.FirstOrDefault(f => !f.News.Deleted && f.News.Url.ToLower() == url.ToLower()).NewsId);
        }

        [Fact(DisplayName = "GetByNewsUrl")]
        [Trait("NewsBookmark", "Get")]
        public async Task ServiceShouldReturnTopTwentyNewsBookmarksIfNewsBookmarkNameParameterIsImpty()
        {
            // arrange
            int newsId = db.News.Where(f => !f.Deleted).Select(f => f.Id).FirstOrDefault();
            // act
            var result = await _newsBookmarkService.GetByNewsId(newsId);
            // assert
            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.Equal(newsId, result.Data.NewsId);
        }

        [Fact(DisplayName = "GetList")]
        [Trait("NewsBookmark", "Get")]
        public async Task ServiceShouldReturnNewsBookmarkList()
        {
            // arrange
            // act
            var result = await _newsBookmarkService.GetList();
            // assert
            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.NotNull(result.Data);
        }

        [Fact(DisplayName = "Add")]
        [Trait("NewsBookmark", "Add")]
        public async Task ServiceShouldAddNewNewsBookmarkToList()
        {
            // arrange
            int newsId = db.News.Where(f => !f.Deleted).Select(f => f.Id).FirstOrDefault();
            // act
            var result = await _newsBookmarkService.Add(new NewsBookmarkAddDto { NewsId = newsId });
            // assert
            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.Contains(db.NewsBookmarks, f => f.NewsId == newsId && f.UserId == 1);
            Assert.Equal(result.Message, Messages.Added);
        }

        [Fact(DisplayName = "DeleteByNewsId")]
        [Trait("NewsBookmark", "Delete")]
        public async Task ServiceShouldDeleteBookmarkWhichBelongToUser()
        {
            // arrange
            int newsId = 1;
            // act
            var result = await _newsBookmarkService.DeleteByNewsId(newsId);
            // assert
            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.DoesNotContain(db.NewsBookmarks, f => f.NewsId == newsId && f.UserId == 1);
            Assert.Equal(result.Message, Messages.Deleted);
        }



        #endregion
    }
}
