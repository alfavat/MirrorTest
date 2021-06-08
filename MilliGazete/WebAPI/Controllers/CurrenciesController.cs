using Business.Managers.Abstract;
using Entity.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrenciesController : MainController
    {
        private ICurrencyService _currencyService;

        public CurrenciesController(ICurrencyService currencyService)
        {
            _currencyService = currencyService;
        }

        [HttpGet("getlist")]
        public async Task<IActionResult> GetList()
        {
            return GetResponse(await _currencyService.GetList());
        }

        [HttpPost("addarray")]
        public async Task<IActionResult> AddArray(List<CurrencyAddDto> list)
        {
            return GetResponse(await _currencyService.AddArray(list));
        }
    }
}