using Business.Managers.Abstract;
using Entity.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdvertisementsController : MainController
    {
        private IAdvertisementService _advertisementService;
        public AdvertisementsController(IAdvertisementService advertisementService)
        {
            _advertisementService = advertisementService;
        }

        [HttpGet("getlist")]
        public async Task<IActionResult> GetList()
        {
            return GetResponse(await _advertisementService.GetList());
        }

        [HttpGet("getactivelist")]
        public async Task<IActionResult> GetActiveLis()
        {
            return GetResponse(await _advertisementService.GetActiveList());
        }

        [HttpGet("getbyid")]
        public async Task<IActionResult> GetById(int id)
        {
            return GetResponse(await _advertisementService.GetById(id));
        }

        [HttpPost("changeactivestatus")]
        public async Task<IActionResult> ChangeActiveStatus(ChangeActiveStatusDto changeActiveStatusDto)
        {
            return GetResponse(await _advertisementService.ChangeActiveStatus(changeActiveStatusDto));
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(AdvertisementAddDto dto)
        {
            return GetResponse(await _advertisementService.Add(dto));
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update(AdvertisementUpdateDto dto)
        {
            return GetResponse(await _advertisementService.Update(dto));
        }

        [HttpPost("delete")]
        public async Task<IActionResult> Delete(int id)
        {
            return GetResponse(await _advertisementService.Delete(id));
        }
    }
}