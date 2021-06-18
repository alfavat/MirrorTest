using Business.Managers.Abstract;
using Entity.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsHitDetailsController : MainController
    {
        private INewsHitDetailService _newsHitDetailService;
        public NewsHitDetailsController(INewsHitDetailService newsHitDetailService)
        {
            _newsHitDetailService = newsHitDetailService;
        }

        [HttpGet("getlist")]
        public async Task<IActionResult> GetList()
        {
            return GetResponse(await _newsHitDetailService.GetList());
        }

        [HttpGet("getlistbynewsid")]
        public async Task<IActionResult> GetListByNewsId(int newsId)
        {
            return GetResponse(await _newsHitDetailService.GetListByNewsId(newsId));
        }

        [HttpGet("getlisthitgroupbynewsId")]
        public async Task<IActionResult> GetListHitGroupByNewsId(int newsId)
        {
            return GetResponse(await _newsHitDetailService.GetListHitGroupByNewsId(newsId));
        }

        [HttpGet("getlastnewshitdetails")]
        public async Task<IActionResult> GetLastNewHitDetail(int minutes = 5)
        {
            return GetResponse(await _newsHitDetailService.GetLastNewHitDetails(minutes));
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(NewsHitDetailAddDto dto)
        {
            return GetResponse(await _newsHitDetailService.Add(dto));
        }
    }
}