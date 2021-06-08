using Business.Managers.Abstract;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationClaimsController : MainController
    {
        private IOperationClaimService _OperationClaimService;

        public OperationClaimsController(IOperationClaimService OperationClaimService)
        {
            _OperationClaimService = OperationClaimService;
        }

        [HttpGet("getlist")]
        public async Task<IActionResult> GetList()
        {
            return GetResponse(await _OperationClaimService.GetList());
        }
    }
}