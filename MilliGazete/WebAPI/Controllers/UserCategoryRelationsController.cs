using Business.Managers.Abstract;
using Entity.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserCategoryRelationsController : MainController
    {
        private IUserCategoryRelationService _userCategoryRelationService;
        public UserCategoryRelationsController(IUserCategoryRelationService UserCategoryRelationService)
        {
            _userCategoryRelationService = UserCategoryRelationService;
        }

        [HttpGet("getbyuserid")]
        public async Task<IActionResult> GetCategoryListByUserId(int userId)
        {
            return GetResponse(await _userCategoryRelationService.GetListByUserId(userId));
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update(UserCategoryRelationUpdateDto dto)
        {
            return GetResponse(await _userCategoryRelationService.Update(dto));
        }
    }
}