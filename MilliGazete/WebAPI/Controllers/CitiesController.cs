using Business.Managers.Abstract;
using Entity.Dtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : MainController
    {
        private ICityService _cityService;
        public CitiesController(ICityService cityService)
        {
            _cityService = cityService;
        }

        [HttpGet("getlist")]
        public async Task<IActionResult> GetList()
        {
            return GetResponse(await _cityService.GetList());
        }
    }
}