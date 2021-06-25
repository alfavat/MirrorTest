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
    public class TestArticleService : TestArticleDal
    {

        #region setup
        private readonly IArticleService _articleService;
        public TestArticleService()
        {
            var _mapper = new TestAutoMapper()._mapper;
            _articleService = new ArticleManager(new ArticleAssistantManager(articleDal, _mapper), _mapper);
        }
        #endregion


        #region methods
        //[Theory(DisplayName = "PaginationWithNullQuery")]
        //[Trait("Article", "Get")]
        //[InlineData(1)]
        //public void ServiceShouldReturnArticleListWithPagination(int page)
        //{
        //    // arrange
        //    var dto = new ArticlePagingDto
        //    {
        //        Limit = 10,
        //        OrderBy = "Id",
        //        PageNumber = page
        //    };
        //    int total = 0;
        //    // act
        //    var result = _articleService.GetListByPaging(dto, out total);
        //    // assert
        //    Assert.NotNull(result);
        //    Assert.True(result.Success);
        //    Assert.True(result.Data.Count <= dto.Limit);
        //    var cnt = db.Article.Count(f => !f.Deleted);
        //    Assert.Equal(total, cnt);
        //}

        //[Theory(DisplayName = "PaginationWithQuery")]
        //[Trait("Article", "Get")]
        //[InlineData(1)]
        //public void ServiceShouldReturnArticleItemsWhichConstainsTheGivenQueryWithPagination(int page)
        //{
        //    // arrange
        //    var dto = new ArticlePagingDto
        //    {
        //        Limit = 10,
        //        OrderBy = "Id",
        //        PageNumber = page,
        //        Query = "8"
        //    };
        //    int total = 0;
        //    // act
        //    var result = _articleService.GetListByPaging(dto, out total);
        //    // assert
        //    Assert.NotNull(result);
        //    Assert.True(result.Success);
        //    Assert.True(result.Data.Count <= dto.Limit);
        //    var cnt = db.Article.Where(f => !f.Deleted && (f.Title.Contains(dto.Query) || f.Url.Contains(dto.Query))).Count();
        //    Assert.Equal(total, cnt);
        //}

        [Theory(DisplayName = "GetById")]
        [Trait("Article", "Get")]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async Task ServiceShouldReturnArticleById(int id)
        {
            // arrange
            // act
            var result = await _articleService.GetById(id);
            // assert
            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.Equal(result.Data.Title, db.Articles.FirstOrDefault(f => f.Id == id & !f.Deleted).Title);
        }
        [Theory(DisplayName = "GetByIdError")]
        [Trait("Article", "Get")]
        [InlineData(-1)]
        [InlineData(-2)]
        [InlineData(-3)]
        public async Task ServiceShouldReturnErrorForNotValidArticleId(int id)
        {
            // arrange
            // act
            var result = await _articleService.GetById(id);
            // assert
            Assert.NotNull(result);
            Assert.False(result.Success);
            Assert.Null(result.Data);
            Assert.Equal(result.Message, Messages.RecordNotFound);
        }

        [Fact(DisplayName = "Add")]
        [Trait("Article", "Add")]
        public async Task ServiceShouldAddNewArticleToList()
        {
            // arrange
            var Article = new ArticleAddDto()
            {
                Approved = true,
                Title = "added Article",
                SeoDescription = "test seo",
                SeoKeywords = "test keyword",
                Url = "test url"
            };
            // act
            var result = await _articleService.Add(Article);
            // assert
            Assert.NotNull(result);
            Assert.True(result.Success);
            var newArticle = db.Articles.FirstOrDefault(f => f.SeoKeywords == Article.SeoKeywords);
            Assert.NotNull(newArticle);
            Assert.Equal(newArticle.CreatedAt.Date, DateTime.Now.Date);
            Assert.Equal(result.Message, Messages.Added);

        }

        [Fact(DisplayName = "Update")]
        [Trait("Article", "Edit")]
        public async Task ServiceShouldUpdateArticle()
        {
            // arrange
            var Article = db.Articles.FirstOrDefault(f => !f.Deleted);
            var dto = new ArticleUpdateDto
            {
                Title = "Edited name",
                Id = Article.Id,
                SeoDescription = "edited seo",
                SeoKeywords = "edited seo",
                Url = "edited url"
            };
            // act
            var result = await _articleService.Update(dto);
            // assert
            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.Equal(result.Message, Messages.Updated);
            var updatedArticle = db.Articles.FirstOrDefault(f => f.Id == Article.Id);
            Assert.Equal(updatedArticle.Approved, dto.Approved);
            Assert.Equal(updatedArticle.Title, dto.Title);
            Assert.Equal(updatedArticle.Url, dto.Url);
        }


        #endregion
    }
}
