using Business.Managers.Abstract;
using Entity.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserOperationClaimsController : MainController
    {
        private IUserOperationClaimService _userOperationClaimService;

        public UserOperationClaimsController(IUserOperationClaimService UserOperationClaimService)
        {
            _userOperationClaimService = UserOperationClaimService;
        }

        [HttpGet("getbyuserid")]
        public async Task<IActionResult> GetByUserId(int userId)
        {
            return GetResponse(await _userOperationClaimService.GetByUserId(userId));
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update(UserOperationClaimUpdateDto dto)
        {
            return GetResponse(await _userOperationClaimService.Update(dto));
        }
    }
}