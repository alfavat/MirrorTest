using Business.Managers.Abstract;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DistrictsController : MainController
    {
        private IDistrictService _districtService;
        public DistrictsController(IDistrictService districtService)
        {
            _districtService = districtService;
        }

        [HttpGet("getlistbycityid")]
        public async Task<IActionResult> GetListByCityId(int id)
        {
            return GetResponse(await _districtService.GetListByCityId(id));
        }

        [HttpGet("getlist")]
        public async Task<IActionResult> GetList()
        {
            return GetResponse(await _districtService.GetList());
        }
    }
}