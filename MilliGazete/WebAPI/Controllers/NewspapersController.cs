using Business.Managers.Abstract;
using Entity.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewspapersController : MainController
    {
        private INewspaperService _newspaperService;
        public NewspapersController(INewspaperService newspaperService)
        {
            _newspaperService = newspaperService;
        }

        [HttpGet("gettodaylist")]
        public async Task<IActionResult> GetList()
        {
            return GetResponse(await _newspaperService.GetTodayList());
        }

        [HttpGet("getmilligazetenewspaper")]
        public async Task<IActionResult> GetMilliGazeteNewpaper()
        {
            return GetResponse(await _newspaperService.GetByName(AppSettingsExtension.GetValue<string>("MilliGazeteNewpaperName")));
        }

        [HttpPost("addarray")]
        public async Task<IActionResult> Add(List<NewspaperAddDto> dto)
        {
            return GetResponse(await _newspaperService.AddArray(dto));
        }
    }
}