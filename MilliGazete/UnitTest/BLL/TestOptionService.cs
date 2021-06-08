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
    public class TestOptionService : TestOptionDal
    {
        #region setup
        private readonly IOptionService _optionService;
        private readonly IMapper _mapper;
        public TestOptionService()
        {
            _mapper = new TestAutoMapper()._mapper;
            _optionService = new OptionManager(new OptionAssistantManager(optionDal, _mapper), _mapper);
        }

        #endregion

        #region methods

        [Fact(DisplayName = "Get")]
        [Trait("Option", "Get")]
        public async Task ServiceShouldReturnOption()
        {
            // arrange
            // act
            var result = await _optionService.Get();
            // assert
            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.NotNull(result.Data);
            Assert.Equal(result.Data.WebsiteTitle, db.Option.FirstOrDefault().WebsiteTitle);
        }

        [Fact(DisplayName = "Update")]
        [Trait("Option", "Edit")]
        public async Task ServiceShouldEditOption()
        {
            // arrange
            var option = db.Option.FirstOrDefault();
            string keywords = option.SeoKeywords;
            var data = _mapper.Map<OptionUpdateDto>(option);
            data.WebsiteTitle = "edited title";
            data.SeoDescription = "edited description";

            // act
            var result = await _optionService.Update(data);
            // assert
            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.Equal(result.Message, Messages.Updated);
            var editedOption = db.Option.FirstOrDefault(f => f.Id == option.Id);
            Assert.Equal(editedOption.WebsiteTitle, data.WebsiteTitle);
            Assert.Equal(editedOption.SeoDescription, data.SeoDescription);
            Assert.Equal(editedOption.SeoKeywords, option.SeoKeywords);
        }

        #endregion
    }
}
