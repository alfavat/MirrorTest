using Business.Constants;
using Business.Managers.Abstract;
using Business.Managers.Concrete;
using Entity.Dtos;
using System;
using System.Linq;
using System.Threading.Tasks;
using UnitTest.DAL;
using UnitTest.Extra;
using Xunit;
namespace UnitTest.BLL
{
    public class TestNewsCommentService : TestNewsCommentDal
    {

        #region setup
        private readonly INewsCommentService _newsCommentService;
        public TestNewsCommentService()
        {
            var _mapper = new TestAutoMapper()._mapper;
            _newsCommentService = new NewsCommentManager(new NewsCommentAssistantManager(newsCommentDal, _mapper),
                _mapper, new NewsAssistantManager(new TestNewsDal().newsDal, _mapper),
                new TestBaseService()._baseService, new TestNewsCommentsHelper()._helper);
        }
        #endregion

        #region methods
        //[Theory(DisplayName = "PaginationWithNullQuery")]
        //[Trait("NewsComment", "Get")]
        //[InlineData(1)]
        //public void ServiceShouldReturnNewsCommentListWithPagination(int page)
        //{
        //    // arrange
        //    var dto = new NewsCommentPagingDto
        //    {
        //        Limit = 10,
        //        OrderBy = "Id",
        //        PageNumber = page
        //    };
        //    int total = 0;
        //    // act
        //    var result = _newsCommentService.GetListByPaging(dto, out total);
        //    // assert
        //    Assert.NotNull(result);
        //    Assert.True(result.Success);
        //    Assert.True(result.Data.Count <= dto.Limit);
        //    var cnt = db.NewsComment.Count(f => !f.Deleted);
        //    Assert.Equal(total, cnt);
        //}

        //[Theory(DisplayName = "PaginationWithQuery")]
        //[Trait("NewsComment", "Get")]
        //[InlineData(1)]
        //public void ServiceShouldReturnNewsCommentItemsWhichConstainsTheGivenQueryWithPagination(int page)
        //{
        //    // arrange
        //    var dto = new NewsCommentPagingDto
        //    {
        //        Limit = 10,
        //        OrderBy = "Id",
        //        PageNumber = page,
        //        Query = "8"
        //    };
        //    int total = 0;
        //    // act
        //    var result = _newsCommentService.GetListByPaging(dto, out total);
        //    // assert
        //    Assert.NotNull(result);
        //    Assert.True(result.Success);
        //    Assert.True(result.Data.Count <= dto.Limit);
        //    var cnt = db.NewsComment.Where(f => !f.Deleted && f.Approved && (f.Title.Contains(dto.Query) || f.Content.Contains(dto.Query))).Count();
        //    Assert.Equal(total, cnt);
        //}


        [Theory(DisplayName = "GetById")]
        [Trait("NewsComment", "Get")]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async Task ServiceShouldReturnNewsCommentById(int id)
        {
            // arrange
            // act
            var result = await _newsCommentService.GetById(id);
            // assert
            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.Equal(result.Data.Title, db.NewsComment.FirstOrDefault(f => f.Id == id & !f.Deleted).Title);
        }

        [Theory(DisplayName = "GetByIdError")]
        [Trait("NewsComment", "Get")]
        [InlineData(-1)]
        [InlineData(-2)]
        [InlineData(-3)]
        public async Task ServiceShouldReturnErrorForNotValidNewsCommentId(int id)
        {
            // arrange
            // act
            var result = await _newsCommentService.GetById(id);
            // assert
            Assert.NotNull(result);
            Assert.False(result.Success);
            Assert.Null(result.Data);
            Assert.Equal(result.Message, Messages.RecordNotFound);
        }

        [Fact(DisplayName = "Add")]
        [Trait("NewsComment", "Add")]
        public async Task ServiceShouldAddNewNewsCommentToList()
        {
            // arrange
            var NewsComment = new NewsCommentAddDto()
            {
                Content = "added NewsComment Content",
                Title = "added title",
                NewsId = 1
            };
            // act
            var result = await _newsCommentService.Add(NewsComment);
            // assert
            Assert.NotNull(result);
            Assert.True(result.Success);
            var newNewsComment = db.NewsComment.FirstOrDefault(f => f.Title == NewsComment.Title);
            Assert.NotNull(newNewsComment);
            Assert.Equal(newNewsComment.CreatedAt.Date, DateTime.Now.Date);
            Assert.Equal(1, newNewsComment.UserId);
            Assert.Equal(result.Message, Messages.Added);

        }


        [Fact(DisplayName = "Update")]
        [Trait("NewsComment", "Edit")]
        public async Task ServiceShouldUpdateNewsComment()
        {
            // arrange
            var NewsComment = db.NewsComment.FirstOrDefault(f => !f.Deleted);
            var dto = new NewsCommentUpdateDto
            {
                Id = NewsComment.Id,
                Content = "edited NewsComment Content",
                Title = "edited title",
                UserId = NewsComment.UserId,
                NewsId = NewsComment.NewsId,
                Approved = false,
                TotalLikeCount = 10
            };
            // act
            var result = await _newsCommentService.Update(dto);
            // assert
            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.Equal(result.Message, Messages.Updated);
            var updatedNewsComment = db.NewsComment.FirstOrDefault(f => f.Id == NewsComment.Id);
            Assert.Equal(updatedNewsComment.Approved, dto.Approved);
            Assert.Equal(updatedNewsComment.TotalLikeCount, dto.TotalLikeCount);
            Assert.Equal(updatedNewsComment.Title, dto.Title);
        }


        #endregion
    }
}
