using Business.Managers.Abstract;
using Entity.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsDetailPageController : MainController
    {
        private readonly INewsDetailPageService _newsDetailPageService;
        public NewsDetailPageController(INewsDetailPageService newsDetailPageService)
        {
            _newsDetailPageService = newsDetailPageService;
        }

        [HttpGet("getnewswithdetails")]
        public async Task<IActionResult> GetNewsWithDetails(string url)
        {
            return GetResponse(await _newsDetailPageService.GetNewsWithDetails(url));
        }

        [HttpGet("getnewswithdetailsbyid")]
        public async Task<IActionResult> GetNewsWithDetailsById(int id, bool preview = false)
        {
            return GetResponse(await _newsDetailPageService.GetNewsWithDetailsById(id, preview));
        }

        [HttpGet("getlastweekmostviewednews")]
        public async Task<IActionResult> GetLastWeekMostViewedNews(int limit = 10)
        {
            return GetResponse(await _newsDetailPageService.GetLastWeekMostViewedNews(limit));
        }

        [HttpGet("getmostsharenews")]
        public async Task<IActionResult> GetMostShareNewsList(int limit = 10)
        {
            return GetResponse(await _newsDetailPageService.GetMostShareNewsList(limit));
        }

        [HttpGet("getnewswithdetailsbypaging")]
        public async Task<IActionResult> GetNewsWithDetailsByPaging(string url, int? newsId, string query, int limit = 10, string orderBy = "Id", int page = 1, int ascending = 2)
        {
            var result = await _newsDetailPageService.GetNewsWithDetailsByPaging(new MainPageNewsPagingDto()
            {
                Query = query,
                Limit = limit,
                OrderBy = orderBy + (ascending == 1 ? " ascending" : " descending"),
                PageNumber = page,
                Url = url,
                NewsId = newsId
            });
            if (result.Success)
                return Ok(new { data = new { data = result.Data.Item1, count = result.Data.Item2 }, result.Message, result.Success });
            return BadRequest(result);
        }

        [HttpGet("getnewswithdetailsbypaging2")]
        public async Task<IActionResult> GetNewsWithDetailsByPaging2(string url, int? newsId, string query, int limit = 10, string orderBy = "Id", int page = 1, int ascending = 2)
        {
            var result = await _newsDetailPageService.GetNewsWithDetailsByPaging2(new MainPageNewsPagingDto()
            {
                Query = query,
                Limit = limit,
                OrderBy = orderBy + (ascending == 1 ? " ascending" : " descending"),
                PageNumber = page,
                Url = url,
                NewsId = newsId
            });
            if (result.Success)
                return Ok(new { data = new { data = result.Data.Item1, count = result.Data.Item2 }, result.Message, result.Success });
            return BadRequest(result);
        }
    }
}