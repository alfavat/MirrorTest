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
    public class TestMenuService : TestMenuDal
    {

        #region setup
        private readonly IMenuService _menuService;
        public TestMenuService()
        {
            var _mapper = new TestAutoMapper()._mapper;
            _menuService = new MenuManager(new MenuAssistantManager(menuDal, _mapper), _mapper);
        }
        #endregion

        #region methods
        [Fact(DisplayName = "GetActiveList")]
        [Trait("Menu", "Get")]
        public async Task ServiceShouldReturnActiveMenuList()
        {
            // arrange
            // act
            var result = await _menuService.GetActiveList();
            // assert
            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.True(result.Data.Count <= db.Menu.Count(f => f.Active && !f.Deleted));
        }

        [Fact(DisplayName = "GetList")]
        [Trait("Menu", "Get")]
        public async Task ServiceShouldReturnMenuList()
        {
            // arrange
            // act
            var result = await _menuService.GetList();
            // assert
            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.Equal(result.Data.Count, db.Menu.Count(f => !f.Deleted));
        }

        [Theory(DisplayName = "GetById")]
        [Trait("Menu", "Get")]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async Task ServiceShouldReturnMenuById(int id)
        {
            // arrange
            // act
            var result = await _menuService.GetById(id);
            // assert
            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.Equal(result.Data.Title, db.Menu.FirstOrDefault(f => f.Id == id & !f.Deleted).Title);
        }
        [Theory(DisplayName = "GetByIdError")]
        [Trait("Menu", "Get")]
        [InlineData(-1)]
        [InlineData(-2)]
        [InlineData(-3)]
        public async Task ServiceShouldReturnErrorForNotValidMenuId(int id)
        {
            // arrange
            // act
            var result = await _menuService.GetById(id);
            // assert
            Assert.NotNull(result);
            Assert.False(result.Success);
            Assert.Null(result.Data);
            Assert.Equal(result.Message, Messages.RecordNotFound);
        }

        [Fact(DisplayName = "Add")]
        [Trait("Menu", "Add")]
        public async Task ServiceShouldAddNewMenuToList()
        {
            // arrange
            var Menu = new MenuAddDto()
            {
                Title = "add title",
                Url = "test url",
                ParentMenuId = null
            };
            // act
            var result = await _menuService.Add(Menu);
            // assert
            Assert.NotNull(result);
            Assert.True(result.Success);
            var newMenu = db.Menu.FirstOrDefault(f => f.Title == Menu.Title);
            Assert.NotNull(newMenu);
            Assert.Equal(newMenu.CreatedAt.Date, DateTime.Now.Date);
            Assert.Equal(result.Message, Messages.Added);

        }

        [Fact(DisplayName = "ChangeStatus")]
        [Trait("Menu", "Edit")]
        public async Task ServiceShouldChangeMenuStatus()
        {
            // arrange
            var Menu = db.Menu.FirstOrDefault();
            var status = new ChangeActiveStatusDto()
            {
                Active = !Menu.Active,
                Id = Menu.Id
            };
            // act
            var result = await _menuService.ChangeActiveStatus(status);
            // assert
            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.Equal(result.Message, Messages.Updated);
            Assert.Equal(status.Active, db.Menu.FirstOrDefault(f => f.Id == Menu.Id).Active);
        }
        [Fact(DisplayName = "ChangeStatusError")]
        [Trait("Menu", "Edit")]
        public async Task ServiceShouldNotChangeMenuStatusIfMenuIdIsWrong()
        {
            // arrange
            var status = new ChangeActiveStatusDto()
            {
                Active = true,
                Id = -1
            };
            // act
            var result = await _menuService.ChangeActiveStatus(status);
            // assert
            Assert.NotNull(result);
            Assert.False(result.Success);
            Assert.Equal(result.Message, Messages.RecordNotFound);
        }

        [Fact(DisplayName = "Update")]
        [Trait("Menu", "Edit")]
        public async Task ServiceShouldUpdateMenu()
        {
            // arrange
            var Menu = db.Menu.FirstOrDefault(f => !f.Deleted);
            var dto = new MenuUpdateDto
            {
                Title = "Edited name",
                Active = !Menu.Active,
                Id = Menu.Id,
                ParentMenuId = db.Menu.ToList()[2].Id,
                Url = "edited url"
            };
            // act
            var result = await _menuService.Update(dto);
            // assert
            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.Equal(result.Message, Messages.Updated);
            var updatedMenu = db.Menu.FirstOrDefault(f => f.Id == Menu.Id);
            Assert.Equal(updatedMenu.Active, dto.Active);
            Assert.Equal(updatedMenu.Title, dto.Title);
            Assert.Equal(updatedMenu.Url, dto.Url);
        }


        #endregion
    }
}
