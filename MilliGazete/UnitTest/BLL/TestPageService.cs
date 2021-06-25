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
    public class TestPageService : TestPageDal
    {

        #region setup
        private readonly IPageService _pageService;
        public TestPageService()
        {
            var _mapper = new TestAutoMapper()._mapper;
            _pageService = new PageManager(new PageAssistantManager(pageDal, _mapper), _mapper);
        }
        #endregion

        #region methods
        //[Theory(DisplayName = "PaginationWithNullQuery")]
        //[Trait("Page", "Get")]
        //[InlineData(1)]
        //public void ServiceShouldReturnPageListWithPagination(int page)
        //{
        //    // arrange
        //    var dto = new PagePagingDto
        //    {
        //        Limit = 10,
        //        OrderBy = "Id",
        //        PageNumber = page
        //    };
        //    int total = 0;
        //    // act
        //    var result = _pageService.GetListByPaging(dto, out total);
        //    // assert
        //    Assert.NotNull(result);
        //    Assert.True(result.Success);
        //    Assert.True(result.Data.Count <= dto.Limit);
        //    var cnt = db.Page.Count(f => !f.Deleted);
        //    Assert.Equal(total, cnt);
        //}

        //[Theory(DisplayName = "PaginationWithQuery")]
        //[Trait("Page", "Get")]
        //[InlineData(1)]
        //public void ServiceShouldReturnPageItemsWhichConstainsTheGivenQueryWithPagination(int page)
        //{
        //    // arrange
        //    var dto = new PagePagingDto
        //    {
        //        Limit = 10,
        //        OrderBy = "Id",
        //        PageNumber = page,
        //        Query = "8"
        //    };
        //    int total = 0;
        //    // act
        //    var result = _pageService.GetListByPaging(dto, out total);
        //    // assert
        //    Assert.NotNull(result);
        //    Assert.True(result.Success);
        //    Assert.True(result.Data.Count <= dto.Limit);
        //    var cnt = db.Page.Where(f => !f.Deleted && (f.Title.Contains(dto.Query) || f.Url.Contains(dto.Query))).Count();
        //    Assert.Equal(total, cnt);
        //}

        //[Theory(DisplayName = "GetById")]
        //[Trait("Page", "Get")]
        //[InlineData(1)]
        //[InlineData(2)]
        //[InlineData(3)]
        //public async Task ServiceShouldReturnPageById(int id)
        //{
        //    // arrange
        //    // act
        //    var result = await _pageService.GetById(id);
        //    // assert
        //    Assert.NotNull(result);
        //    Assert.True(result.Success);
        //    Assert.Equal(result.Data.Title, db.Page.FirstOrDefault(f => f.Id == id & !f.Deleted).Title);
        //}
        //[Theory(DisplayName = "GetByIdError")]
        //[Trait("Page", "Get")]
        //[InlineData(-1)]
        //[InlineData(-2)]
        //[InlineData(-3)]
        //public async Task ServiceShouldReturnErrorForNotValidPageId(int id)
        //{
        //    // arrange
        //    // act
        //    var result = await _pageService.GetById(id);
        //    // assert
        //    Assert.NotNull(result);
        //    Assert.False(result.Success);
        //    Assert.Null(result.Data);
        //    Assert.Equal(result.Message, Messages.RecordNotFound);
        //}

        [Fact(DisplayName = "Add")]
        [Trait("Page", "Add")]
        public async Task ServiceShouldAddNewPageToList()
        {
            // arrange
            var Page = new PageAddDto()
            {

                SeoDescription = "test seo",
                SeoKeywords = "test keyword",
                Title = "added title",
                Url = "test url"
            };
            // act
            var result = await _pageService.Add(Page);
            // assert
            Assert.NotNull(result);
            Assert.True(result.Success);
            var newPage = db.Pages.FirstOrDefault(f => f.SeoKeywords == Page.SeoKeywords);
            Assert.NotNull(newPage);
            Assert.Equal(newPage.CreatedAt.Date, DateTime.Now.Date);
            Assert.Equal(result.Message, Messages.Added);

        }

        [Fact(DisplayName = "Update")]
        [Trait("Page", "Edit")]
        public async Task ServiceShouldUpdatePage()
        {
            // arrange
            var Page = db.Pages.FirstOrDefault(f => !f.Deleted);
            var dto = new PageUpdateDto
            {
                Id = Page.Id,
                SeoDescription = "edited seo",
                SeoKeywords = "edited seo",
                Title = "edited title",
                Url = "edited url"
            };
            // act
            var result = await _pageService.Update(dto);
            // assert
            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.Equal(result.Message, Messages.Updated);
            var updatedPage = db.Pages.FirstOrDefault(f => f.Id == Page.Id);
            Assert.Equal(updatedPage.Title, dto.Title);
            Assert.Equal(updatedPage.Url, dto.Url);
        }


        #endregion
    }
}
