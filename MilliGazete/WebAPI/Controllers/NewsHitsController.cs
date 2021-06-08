using Business.Managers.Abstract;
using Entity.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsHitsController : MainController
    {
        private INewsHitService _newsHitService;
        public NewsHitsController(INewsHitService newsHitService)
        {
            _newsHitService = newsHitService;
        }

        [HttpGet("getlist")]
        public async Task<IActionResult> GetList()
        {
            return GetResponse(await _newsHitService.GetList());
        }

        [HttpGet("getlistbynewsid")]
        public async Task<IActionResult> GetListByNewsId(int newsId)
        {
            return GetResponse(await _newsHitService.GetListByNewsId(newsId));
        }

        //[HttpPost("add")]
        //public async Task<IActionResult> Add(NewsHitAddDto dto)
        //{
        //    return GetResponse(await _newsHitService.AddWithDetail(dto));
        //}
    }
}