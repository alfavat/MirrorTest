using Business.Managers.Abstract;
using Entity.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsBookmarksController : MainController
    {
        private readonly INewsBookmarkService _newsBookmarkService;

        public NewsBookmarksController(INewsBookmarkService newsBookmarkService)
        {
            _newsBookmarkService = newsBookmarkService;
        }

        [HttpGet("getlist")]
        public async Task<IActionResult> GetList()
        {
            return GetResponse(await _newsBookmarkService.GetList());
        }

        [HttpGet("hasuserbookmarkednews")]
        public async Task<IActionResult> HasUserBookmarkedNews(int newsId)
        {
            return GetResponse(await _newsBookmarkService.HasUserBookmarkedNews(newsId));
        }

        [HttpGet("getbynewsurl")]
        public async Task<IActionResult> GetByNewsUrl(string newsUrl)
        {
            return GetResponse(await _newsBookmarkService.GetByNewsUrl(newsUrl));
        }

        [HttpGet("getbynewsid")]
        public async Task<IActionResult> GetByNewsId(int newsId)
        {
            return GetResponse(await _newsBookmarkService.GetByNewsId(newsId));
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(NewsBookmarkAddDto dto)
        {
            return GetResponse(await _newsBookmarkService.Add(dto));
        }

        [HttpPost("deletebynewsid")]
        public async Task<IActionResult> DeleteByNewsId(int newsId)
        {
            return GetResponse(await _newsBookmarkService.DeleteByNewsId(newsId));
        }
    }
}
