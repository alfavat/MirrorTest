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
    public class TestUserService : TestUserDal
    {
        #region setup
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public TestUserService()
        {
            _mapper = new TestAutoMapper()._mapper;
            _userService = new UserManager(new UserAssistantManager(userDal, _mapper), _mapper, new TestBaseService()._baseService);
        }

        #endregion

        #region methods

        [Fact(DisplayName = "GetList")]
        [Trait("User", "Get")]
        public async Task ServiceShouldReturnUserList()
        {
            // arrange
            // act
            var result = await _userService.GetList();
            // assert
            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.Equal(result.Data.Count, db.Users.Count(f => !f.Deleted));
        }


        [Theory(DisplayName = "GetById")]
        [Trait("User", "Get")]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async Task ServiceShouldReturnUserById(int id)
        {
            // arrange
            // act
            var result = await _userService.GetById(id);
            // assert
            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.Equal(result.Data.UserName, db.Users.FirstOrDefault(f => f.Id == id & !f.Deleted).UserName);
        }

        [Theory(DisplayName = "GetByIdError")]
        [Trait("User", "Get")]
        [InlineData(-1)]
        [InlineData(-2)]
        [InlineData(-3)]
        public async Task ServiceShouldReturnErrorForNotValidUserId(int id)
        {
            // arrange
            // act
            var result = await _userService.GetById(id);
            // assert
            Assert.NotNull(result);
            Assert.False(result.Success);
            Assert.Null(result.Data);
            Assert.Equal(result.Message, Messages.UserNotFound);
        }

        [Theory(DisplayName = "PaginationWithNullQuery")]
        [Trait("User", "Get")]
        [InlineData(1)]
        public void ServiceShouldReturnNewsListWithPagination(int page)
        {
            // arrange
            var dto = new UserPagingDto
            {
                Limit = 10,
                OrderBy = "Id",
                PageNumber = page
            };
            int total = 0;
            // act
            var result = _userService.GetListByPaging(dto, out total);
            // assert
            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.True(result.Data.Count <= dto.Limit);
            Assert.Equal(total, db.Users.Count(f => !f.Deleted));
        }

        [Fact(DisplayName = "Add")]
        [Trait("User", "Add")]
        public async Task ServiceShouldAddNewUserToList()
        {
            // arrange
            var user = new UserAddDto()
            {
                Email = "register@test.com",
                UserName = "added_User",
                Password = "123456789",
                Active = true,
                FirstName = "f name",
                LastName = "l name"
            };
            // act
            var result = await _userService.AddUser(user);
            // assert
            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.True(result.Data > 0);
            Assert.Equal(result.Message, Messages.UserRegistered);
            Assert.Contains(db.Users, f => f.Id == result.Data && f.Email == user.Email);
        }

        [Fact(DisplayName = "AddError")]
        [Trait("User", "Add")]
        public async Task ServiceShouldReturnErrorForExistEmail()
        {
            // arrange
            var email = db.Users.FirstOrDefault(f => !f.Deleted).Email;
            var user = new UserAddDto()
            {
                Email = email,
                UserName = "added_User",
                Password = "123456789",
                Active = true,
                FirstName = "f name",
                LastName = "l name"
            };
            // act
            var result = await _userService.AddUser(user);
            // assert
            Assert.NotNull(result);
            Assert.False(result.Success);
            Assert.True(result.Data <= 0);
            Assert.Equal(result.Message, Messages.EmailAlreadyExists);
        }

        [Fact(DisplayName = "Register")]
        [Trait("User", "Add")]
        public async Task ServiceShouldRegisterUserAndSetIsEmployeeFieldFalse()
        {
            // arrange
            var User = new RegisterDto()
            {
                Email = "register@test.com",
                UserName = "added_User",
                Password = "123456789",
                PasswordConfirm = "123456789"
            };
            // act
            var result = await _userService.RegisterUser(User);
            // assert
            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.Contains(db.Users, f => f.UserName == User.UserName);
            Assert.Equal(result.Message, Messages.UserRegistered);
            Assert.False(db.Users.FirstOrDefault(f => f.UserName == User.UserName).IsEmployee);
        }


        [Fact(DisplayName = "ChangeStatus")]
        [Trait("User", "Edit")]
        public async Task ServiceShouldChangeUserStatus()
        {
            // arrange
            var User = db.Users.FirstOrDefault(f => !f.Deleted);
            var status = new ChangeActiveStatusDto()
            {
                Active = !User.Active,
                Id = User.Id
            };
            // act
            var result = await _userService.ChangeActiveStatus(status);
            // assert
            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.Equal(result.Message, Messages.Updated);
            Assert.Equal(status.Active, db.Users.FirstOrDefault(f => f.Id == User.Id).Active);
        }
        [Fact(DisplayName = "ChangeStatusError")]
        [Trait("User", "Edit")]
        public async Task ServiceShouldNotChangeUserStatusIfUserIdIsWrong()
        {
            // arrange
            var status = new ChangeActiveStatusDto()
            {
                Active = true,
                Id = -1
            };
            // act
            var result = await _userService.ChangeActiveStatus(status);
            // assert
            Assert.NotNull(result);
            Assert.False(result.Success);
            Assert.Equal(result.Message, Messages.UserNotFound);
        }

        [Fact(DisplayName = "UpdateUser")]
        [Trait("User", "Edit")]
        public async Task ServiceShouldUpdateUserDetails()
        {
            // arrange
            var dto = new UserUpdateDto
            {
                Email = "updated@test.com",
                FirstName = "updated fname",
                Password = "987654321",
                Active = true,
                LastName = "edit last name",
                Id = 1,
                UserName = "edit_user_name"
            };
            // act
            var result = await _userService.UpdateUser(dto);
            // assert
            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.Equal(result.Message, Messages.Updated);
            var user = await _userService.GetById(dto.Id);
            Assert.Equal(dto.Email, user.Data.Email);
            Assert.Equal(dto.FirstName, user.Data.FirstName);
            Assert.Equal(dto.LastName, user.Data.LastName);
        }

        [Fact(DisplayName = "UpdateCurrentUser")]
        [Trait("User", "Edit")]
        public async Task ServiceShouldUpdateCurrentUserDetails()
        {
            // arrange
            var dto = new CurrentUserUpdateDto
            {
                Email = "updated@test.com",
                FirstName = "updated fname",
                LastName = "edit last name",
                UserName = "edit_user_name"
            };
            // act
            var result = await _userService.UpdateCurrentUser(dto);
            // assert
            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.Equal(result.Message, Messages.Updated);
            Assert.True(db.Users.Any(f => f.UserName == dto.UserName && f.FirstName == dto.FirstName && f.LastName == dto.LastName && f.Email == dto.Email));
        }

        [Fact(DisplayName = "UpdateCurrentUserError")]
        [Trait("User", "Edit")]
        public async Task ServiceShouldNotUpdateCurrentUserWithExistEmailDetails()
        {
            // arrange
            var user = db.Users.FirstOrDefault(f => f.Id != 1 && !f.Deleted).Email;
            var dto = new CurrentUserUpdateDto
            {
                Email = user,
                FirstName = "updated fname",
                LastName = "edit last name",
                UserName = "edit_user_name"
            };
            // act
            var result = await _userService.UpdateCurrentUser(dto);
            // assert
            Assert.NotNull(result);
            Assert.False(result.Success);
            Assert.Equal(result.Message, Messages.EmailAlreadyExists);
            Assert.False(db.Users.Any(f => f.UserName == dto.UserName && f.FirstName == dto.FirstName && f.LastName == dto.LastName && f.Email == dto.Email));
        }


        #endregion
    }
}
