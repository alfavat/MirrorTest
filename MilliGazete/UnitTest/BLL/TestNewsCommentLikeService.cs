using Business.Constants;
using Business.Managers.Abstract;
using Business.Managers.Concrete;
using System.Linq;
using System.Threading.Tasks;
using UnitTest.DAL;
using UnitTest.Extra;
using Xunit;
namespace UnitTest.BLL
{
    public class TestNewsCommentLikeService : TestNewsCommentLikeDal
    {

        #region setup
        private readonly INewsCommentLikeService _newsCommentLikeService;
        public TestNewsCommentLikeService()
        {
            var _mapper = new TestAutoMapper()._mapper;
            _newsCommentLikeService = new NewsCommentLikeManager(new NewsCommentLikeAssistantManager(newsCommentLikeDal, _mapper), new TestBaseService()._baseService);
        }
        #endregion

        #region methods

        [Fact(DisplayName = "Add")]
        [Trait("NewsCommentLike", "Add")]
        public async Task ServiceShouldAddNewLikeAndUpdateTotalLikeCount()
        {
            // arrange
            var newsComment = db.NewsCommentLike.FirstOrDefault(f => f.UserId != 1);
            // act
            var result = await _newsCommentLikeService.AddOrDelete(newsComment.NewsCommentId);
            // assert
            Assert.NotNull(result);
            Assert.True(result.Success);
            var newComment = db.NewsComment.FirstOrDefault(f => f.Id == newsComment.NewsCommentId);
            Assert.NotNull(newComment);
            Assert.Equal(newComment.TotalLikeCount, db.NewsCommentLike.Count(f => f.NewsCommentId == newsComment.NewsCommentId));
            Assert.Equal(result.Message, Messages.Updated);

        }


        [Fact(DisplayName = "Delete")]
        [Trait("NewsCommentLike", "Edit")]
        public async Task ServiceShouldDeleteLikeAndUpdateTotalLikeCount()
        {
            // arrange
            var newsComment = db.NewsComment.FirstOrDefault(f => f.UserId == 1 && f.TotalLikeCount > 2);
            // act
            var result = await _newsCommentLikeService.AddOrDelete(newsComment.Id);
            // assert
            Assert.NotNull(result);
            Assert.True(result.Success);
            var newComment = db.NewsComment.FirstOrDefault(f => f.Id == newsComment.Id);
            Assert.NotNull(newComment);
            Assert.Equal(newComment.TotalLikeCount, db.NewsCommentLike.Count(f => f.NewsCommentId == newsComment.Id));
            Assert.Equal(result.Message, Messages.Updated);
        }


        #endregion
    }
}
