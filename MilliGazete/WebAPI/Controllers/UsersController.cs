using Business.Managers.Abstract;
using Entity.Dtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : MainController
    {
        private IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("getlist")]
        public async Task<IActionResult> GetList()
        {
            return GetResponse(await _userService.GetList());
        }

        [HttpGet("getcurrentuser")]
        public async Task<IActionResult> GetCurrentUser()
        {
            return GetResponse(await _userService.GetCurrentUser());
        }

        [HttpGet("getbyid")]
        public async Task<IActionResult> GetUserById(int userId)
        {
            return GetResponse(await _userService.GetById(userId));
        }


        [HttpGet("getlistbypaging")]
        public IActionResult GetListByPaging(DateTime? fromCreatedAt, DateTime? toCreatedAt, string query, bool? isEmployee = null, bool? active = null,
            int limit = 10, string orderBy = "Id", int page = 1, int ascending = 1)
        {
            return GetResponse(_userService.GetListByPaging(new UserPagingDto()
            {
                Query = query,
                Limit = limit,
                OrderBy = orderBy + (ascending == 1 ? " ascending" : " descending"),
                PageNumber = page,
                Active = active,
                IsEmployee = isEmployee,
                FromCreatedAt = fromCreatedAt,
                ToCreatedAt = toCreatedAt
            }, out int total), total);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            return GetResponse(await _userService.RegisterUser(registerDto));
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddUser(UserAddDto dto)
        {
            return GetResponse(await _userService.AddUser(dto));
        }

        [HttpPost("delete")]
        public async Task<IActionResult> Delete(int userId)
        {
            return GetResponse(await _userService.Delete(userId));
        }

        [HttpPost("changepassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordDto vm)
        {
            return GetResponse(await _userService.ChangePassword(vm));
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update(UserUpdateDto dto)
        {
            return GetResponse(await _userService.UpdateUser(dto));
        }

        [HttpPost("updatecurrentuser")]
        public async Task<IActionResult> UpdateCurrentUser(CurrentUserUpdateDto dto)
        {
            return GetResponse(await _userService.UpdateCurrentUser(dto));
        }

        [HttpPost("changeactivestatus")]
        public async Task<IActionResult> ChangeActiveStatus(ChangeActiveStatusDto dto)
        {
            return GetResponse(await _userService.ChangeActiveStatus(dto));
        }
    }
}