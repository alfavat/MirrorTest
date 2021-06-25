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
    public class TestCategoryService : TestCategoryDal
    {

        #region setup
        private readonly ICategoryService _categoryService;
        public TestCategoryService()
        {
            var _mapper = new TestAutoMapper()._mapper;
            _categoryService = new CategoryManager(new CategoryAssistantManager(categoryDal, _mapper), _mapper);
        }
        #endregion

        #region methods
        //[Theory(DisplayName = "PaginationWithNullQuery")]
        //[Trait("Category", "Get")]
        //[InlineData(1)]
        //public void ServiceShouldReturnCategoryListWithPagination(int page)
        //{
        //    // arrange
        //    var dto = new CategoryPagingDto
        //    {
        //        Limit = 10,
        //        OrderBy = "Id",
        //        PageNumber = page
        //    };
        //    int total = 0;
        //    // act
        //    var result = _categoryService.GetListByPaging(dto, out total);
        //    // assert
        //    Assert.NotNull(result);
        //    Assert.True(result.Success);
        //    Assert.True(result.Data.Count <= dto.Limit);
        //    var cnt = db.Category.Count(f => !f.Deleted);
        //    Assert.Equal(total, cnt);
        //}

        //[Theory(DisplayName = "PaginationWithQuery")]
        //[Trait("Category", "Get")]
        //[InlineData(1)]
        //public void ServiceShouldReturnCategoryItemsWhichConstainsTheGivenQueryWithPagination(int page)
        //{
        //    // arrange
        //    var dto = new CategoryPagingDto
        //    {
        //        Limit = 10,
        //        OrderBy = "Id",
        //        PageNumber = page,
        //        Query = "8"
        //    };
        //    int total = 0;
        //    // act
        //    var result = _categoryService.GetListByPaging(dto, out total);
        //    // assert
        //    Assert.NotNull(result);
        //    Assert.True(result.Success);
        //    Assert.True(result.Data.Count <= dto.Limit);
        //    var cnt = db.Category.Where(f => !f.Deleted && (f.CategoryName.Contains(dto.Query) || f.Url.Contains(dto.Query))).Count();
        //    Assert.Equal(total, cnt);
        //}

        //[Fact(DisplayName = "GetList")]
        //[Trait("Category", "Get")]
        //public async Task ServiceShouldReturnCategoryList()
        //{
        //    // arrange
        //    // act
        //    var result = await _categoryService.GetList();
        //    // assert
        //    Assert.NotNull(result);
        //    Assert.True(result.Success);
        //    Assert.Equal(result.Data.Count, db.Category.Count(f => !f.Deleted));
        //}

        [Theory(DisplayName = "GetById")]
        [Trait("Category", "Get")]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async Task ServiceShouldReturnCategoryById(int id)
        {
            // arrange
            // act
            var result = await _categoryService.GetById(id);
            // assert
            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.Equal(result.Data.CategoryName, db.Categories.FirstOrDefault(f => f.Id == id & !f.Deleted).CategoryName);
        }
        [Theory(DisplayName = "GetByIdError")]
        [Trait("Category", "Get")]
        [InlineData(-1)]
        [InlineData(-2)]
        [InlineData(-3)]
        public async Task ServiceShouldReturnErrorForNotValidCategoryId(int id)
        {
            // arrange
            // act
            var result = await _categoryService.GetById(id);
            // assert
            Assert.NotNull(result);
            Assert.False(result.Success);
            Assert.Null(result.Data);
            Assert.Equal(result.Message, Messages.RecordNotFound);
        }

        [Fact(DisplayName = "Add")]
        [Trait("Category", "Add")]
        public async Task ServiceShouldAddNewCategoryToList()
        {
            // arrange
            var category = new CategoryAddDto()
            {
                Active = true,
                CategoryName = "added category",
                IsStatic = false,
                SeoDescription = "test seo",
                SeoKeywords = "test keyword",
                StyleCode = "test style code",
                Symbol = "test symbol",
                Url = "test url"
            };
            // act
            var result = await _categoryService.Add(category);
            // assert
            Assert.NotNull(result);
            Assert.True(result.Success);
            var newCategory = db.Categories.FirstOrDefault(f => f.SeoKeywords == category.SeoKeywords);
            Assert.NotNull(newCategory);
            Assert.Equal(newCategory.CreatedAt.Date, DateTime.Now.Date);
            Assert.Equal(result.Message, Messages.Added);

        }

        [Fact(DisplayName = "ChangeStatus")]
        [Trait("Category", "Edit")]
        public async Task ServiceShouldChangecategoryStatus()
        {
            // arrange
            var category = db.Categories.FirstOrDefault();
            var status = new ChangeActiveStatusDto()
            {
                Active = !category.Active,
                Id = category.Id
            };
            // act
            var result = await _categoryService.ChangeActiveStatus(status);
            // assert
            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.Equal(result.Message, Messages.Updated);
            Assert.Equal(status.Active, db.Categories.FirstOrDefault(f => f.Id == category.Id).Active);
        }
        [Fact(DisplayName = "ChangeStatusError")]
        [Trait("category", "Edit")]
        public async Task ServiceShouldNotChangecategoryStatusIfcategoryIdIsWrong()
        {
            // arrange
            var status = new ChangeActiveStatusDto()
            {
                Active = true,
                Id = -1
            };
            // act
            var result = await _categoryService.ChangeActiveStatus(status);
            // assert
            Assert.NotNull(result);
            Assert.False(result.Success);
            Assert.Equal(result.Message, Messages.RecordNotFound);
        }

        [Fact(DisplayName = "Update")]
        [Trait("Category", "Edit")]
        public async Task ServiceShouldUpdateCategory()
        {
            // arrange
            var category = db.Categories.FirstOrDefault(f => !f.Deleted);
            var dto = new CategoryUpdateDto
            {
                CategoryName = "Edited name",
                Active = !category.Active,
                Id = category.Id,
                IsStatic = category.IsStatic,
                ParentCategoryId = db.Categories.ToList()[2].Id,
                SeoDescription = "edited seo",
                SeoKeywords = "edited seo",
                StyleCode = "edited code",
                Symbol = category.Symbol,
                Url = "edited url"
            };
            // act
            var result = await _categoryService.Update(dto);
            // assert
            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.Equal(result.Message, Messages.Updated);
            var updatedCategory = db.Categories.FirstOrDefault(f => f.Id == category.Id);
            Assert.Equal(updatedCategory.Active, dto.Active);
            Assert.Equal(updatedCategory.Symbol, dto.Symbol);
            Assert.Equal(updatedCategory.Url, dto.Url);
        }


        #endregion
    }
}
