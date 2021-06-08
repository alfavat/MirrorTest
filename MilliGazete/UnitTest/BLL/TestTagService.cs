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
    public class TestTagService : TestTagDal
    {
        private readonly IMapper _mapper;
        #region setup
        private readonly ITagService _tagService;
        public TestTagService()
        {
            _mapper = new TestAutoMapper()._mapper;
            _tagService = new TagManager(new TagAssistantManager(tagDal, _mapper), _mapper);
        }

        #endregion

        #region methods

        [Fact(DisplayName = "SearchByTagName")]
        [Trait("Tag", "Get")]
        public async Task ServiceShouldReturnTagsWhichTheyStartWithTagName()
        {
            // arrange
            string tagName = "tag";
            // act
            var result = await _tagService.SearchByTagName(tagName);
            // assert
            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.Equal(result.Data.Count, db.Tag.Count(f => !f.Deleted && f.TagName.StartsWith(tagName)));
        }

        [Fact(DisplayName = "SearchByTagNameEmptyTagName")]
        [Trait("Tag", "Get")]
        public async Task ServiceShouldReturnTopTwentyTagsIfTagNameParameterIsImpty()
        {
            // arrange
            string tagName = null;
            // act
            var result = await _tagService.SearchByTagName(tagName);
            // assert
            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.True(20 >= result.Data.Count);
        }

        [Theory(DisplayName = "PaginationWithNullQuery")]
        [Trait("Tag", "Get")]
        [InlineData(1)]
        public void ServiceShouldReturnTagListWithPagination(int page)
        {
            // arrange
            var dto = new TagPagingDto
            {
                Limit = 10,
                OrderBy = "Id",
                PageNumber = page,
                Query = ""
            };
            int total = 0;
            // act
            var result = _tagService.GetListByPaging(dto, out total);
            // assert
            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.True(result.Data.Count <= dto.Limit);
        }

        [Theory(DisplayName = "PaginationWithQuery")]
        [Trait("Tag", "Get")]
        [InlineData(1)]
        public void ServiceShouldReturnTagItemsWhichConstainsTheGivenQueryWithPagination(int page)
        {
            // arrange
            var dto = new TagPagingDto
            {
                Limit = 10,
                OrderBy = "Id",
                PageNumber = page,
                Query = "8"
            };
            int total = 0;
            // act
            var result = _tagService.GetListByPaging(dto, out total);
            // assert
            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.True(result.Data.Count <= dto.Limit);
        }

        [Fact(DisplayName = "GetList")]
        [Trait("Tag", "Get")]
        public async Task ServiceShouldReturnTagList()
        {
            // arrange
            // act
            var result = await _tagService.GetList();
            // assert
            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.NotNull(result.Data);
        }
        [Theory(DisplayName = "GetById")]
        [Trait("Tag", "Get")]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async Task ServiceShouldReturnTagById(int id)
        {
            // arrange
            // act
            var result = await _tagService.GetById(id);
            // assert
            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.Equal(result.Data.TagName, db.Tag.FirstOrDefault(f => f.Id == id & !f.Deleted).TagName);
        }
        [Theory(DisplayName = "GetByIdError")]
        [Trait("Tag", "Get")]
        [InlineData(-1)]
        [InlineData(-2)]
        [InlineData(-3)]
        public async Task ServiceShouldReturnErrorForNotValidTagId(int id)
        {
            // arrange
            // act
            var result = await _tagService.GetById(id);
            // assert
            Assert.NotNull(result);
            Assert.False(result.Success);
            Assert.Null(result.Data);
            Assert.Equal(result.Message, Messages.RecordNotFound);
        }

        [Fact(DisplayName = "Add")]
        [Trait("Tag", "Add")]
        public async Task ServiceShouldAddNewTagToList()
        {
            // arrange
            var tag = new TagAddDto()
            {
                Active = true,
                TagName = "added Tag",
                Url = "added url"
            };
            // act
            var result = await _tagService.Add(tag);
            // assert
            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.Contains(db.Tag, f => f.TagName == tag.TagName);
            Assert.Equal(result.Message, Messages.Added);
        }


        [Fact(DisplayName = "ChangeStatus")]
        [Trait("Tag", "Edit")]
        public async Task ServiceShouldChangeTagStatus()
        {
            // arrange
            var tag = db.Tag.FirstOrDefault();
            var status = new ChangeActiveStatusDto()
            {
                Active = true,
                Id = tag.Id
            };
            // act
            var result = await _tagService.ChangeActiveStatus(status);
            // assert
            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.Equal(result.Message, Messages.Updated);
            Assert.Equal(status.Active, db.Tag.FirstOrDefault(f => f.Id == tag.Id).Active);
        }
        [Fact(DisplayName = "ChangeStatusError")]
        [Trait("Tag", "Edit")]
        public async Task ServiceShouldNotChangeTagStatusIfTagIdIsWrong()
        {
            // arrange
            var status = new ChangeActiveStatusDto()
            {
                Active = true,
                Id = -1
            };
            // act
            var result = await _tagService.ChangeActiveStatus(status);
            // assert
            Assert.NotNull(result);
            Assert.False(result.Success);
            Assert.Equal(result.Message, Messages.RecordNotFound);
        }

        [Fact(DisplayName = "Update")]
        [Trait("Tag", "Edit")]
        public async Task ServiceShouldUpdateTagDetails()
        {
            // arrange
            var tag = db.Tag.FirstOrDefault(f => !f.Deleted);

            TagUpdateDto data = new TagUpdateDto
            {
                Id = tag.Id,
                Active = tag.Active
            };
            data.TagName = "edited title";
            data.Url = "edited url";
            // act
            var result = await _tagService.Update(data);
            // assert
            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.Equal(result.Message, Messages.Updated);
        }

        [Fact(DisplayName = "UpdateError")]
        [Trait("Tag", "Edit")]
        public async Task ServiceShouldReturnNotFoundErrorForInValidIds()
        {
            // arrange
            var tag = db.Tag.FirstOrDefault(f => !f.Deleted);

            TagUpdateDto data = new TagUpdateDto
            {
                Id = tag.Id,
                Active = tag.Active
            };
            data.TagName = "edited title";
            data.Url = "edited url";
            data.Id = -1;
            // act
            var result = await _tagService.Update(data);
            // assert
            Assert.NotNull(result);
            Assert.False(result.Success);
            Assert.Equal(result.Message, Messages.RecordNotFound);
        }

        #endregion
    }
}
