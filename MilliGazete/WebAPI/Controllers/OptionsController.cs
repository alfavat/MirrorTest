using Business.Managers.Abstract;
using Entity.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OptionsController : MainController
    {
        private IOptionService _OptionService;
        public OptionsController(IOptionService OptionService)
        {
            _OptionService = OptionService;
        }

        [HttpGet("get")]
        public async Task<IActionResult> Get()
        {
            return GetResponse(await _OptionService.Get());
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update(OptionUpdateDto OptionUpdateDto)
        {
            return GetResponse(await _OptionService.Update(OptionUpdateDto));
        }
    }
}
