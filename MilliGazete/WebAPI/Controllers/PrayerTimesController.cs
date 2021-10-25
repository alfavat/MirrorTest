using Business.Managers.Abstract;
using Entity.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrayerTimesController : MainController
    {
        private IPrayerTimeService _prayerTimeService;
        public PrayerTimesController(IPrayerTimeService PrayerTimeService)
        {
            _prayerTimeService = PrayerTimeService;
        }

        [HttpGet("getlistbycityid")]
        public async Task<IActionResult> GetListByCityIdAsync(int cityId)
        {
            var result = await _prayerTimeService.GetPrayerTimeByCityId(cityId);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("getlist")]
        public async Task<IActionResult> GetList()
        {
            return GetResponse(await _prayerTimeService.GetList());
        }

        [HttpPost("addarray")]
        public async Task<IActionResult> AddArray(List<PrayerTimeAddDto> prayerTimes)
        {
            return GetResponse(await _prayerTimeService.AddArray(prayerTimes));
        }
    }
}