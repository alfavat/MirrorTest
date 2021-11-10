using Core.Utilities.Results;
using Entity.Dtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebUI.Models;
using WebUI.Repository.Abstract;

namespace WebUI.Controllers
{
    public class PrayerTimeController : Controller
    {

        private readonly IPrayerTimeRepository _prayerTimeRepository;
        private readonly IPageRepository _pageRepository;

        public PrayerTimeController(IPrayerTimeRepository prayerTimeRepository, IPageRepository pageRepository)
        {
            _prayerTimeRepository = prayerTimeRepository;
            _pageRepository = pageRepository;
        }

        [Route("{cityUrl}-namaz-vakitleri")]
        public async Task<IActionResult> Index(string cityUrl = "istanbul")
        {
            PrayerPageModel result = new PrayerPageModel();
            result.PrayerTime = _prayerTimeRepository.GetPrayerTime((int)Enum.Parse(typeof(CitiesEnum), cityUrl)).Result.Data;
            result.Page = _pageRepository.GetByUrl("namaz-vakitleri").Result.Data;
            return View(result);
        }

        [HttpGet]
        [Route("namaz-vakti")]
        public List<PrayerTimeDto> GetPrayerTimeAsync(int sehir = 34)
        {
            return _prayerTimeRepository.GetPrayerTime(sehir).Result.Data;
        }
    }
}
