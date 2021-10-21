using Business.Managers.Abstract;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MainPageController : MainController
    {
        private readonly IMainPageService _mainPageService;
        private readonly INewsService newsService;

        public MainPageController(IMainPageService mainPageService, INewsService newsService)
        {
            _mainPageService = mainPageService;
            this.newsService = newsService;
        }

        [HttpGet("getmainheadingnews")]
        public async Task<IActionResult> GetMainHeadingNews(int limit = 10)
        {
            return GetResponse(await _mainPageService.GetMainHeadingNews(limit));
        }

        [HttpGet("getmainpagemenulist")]
        public IActionResult GetMainPageMenuList()
        {
            return GetResponse(_mainPageService.GetMainPageMenuList());
        }

        [HttpGet("getstreaminglist")]
        public IActionResult GetStreamingList()
        {
            return GetResponse(_mainPageService.GetStreamingList());
        }

        [HttpGet("getsubheadingnews")]
        public async Task<IActionResult> GetSubHeadingNews(int limit = 10)
        {
            return GetResponse(await _mainPageService.GetSubHeadingNews(limit));
        }

        [HttpGet("getsubheadingnews2")]
        public async Task<IActionResult> GetSubHeadingNews2(int limit = 10)
        {
            return GetResponse(await _mainPageService.GetSubHeadingNews2(limit));
        }

        [HttpGet("getsuperleaguestandings")]
        public IActionResult GetSuperLeagueStandings()
        {
            return GetResponse(_mainPageService.GetSuperLeagueStandings());
        }

        [HttpGet("gettopbreakingnews")]
        public async Task<IActionResult> GetTopBreakingNews(int limit = 10)
        {
            return GetResponse(await _mainPageService.GetTopBreakingNews(limit));
        }

        [HttpGet("gettopvideonews")]
        public async Task<IActionResult> GetTopVideoNews(int limit = 10)
        {
            return GetResponse(await _mainPageService.GetTopVideoNewsAsync(limit));
        }

        [HttpGet("gettopimagenews")]
        public async Task<IActionResult> GetTopImageNews(int limit = 10)
        {
            return GetResponse(await _mainPageService.GetTopImageNewsAsync(limit));
        }

        [HttpGet("gettopmainpagenews")]
        public async Task<IActionResult> GetTopMainPageNews(int limit = 10)
        {
            return GetResponse(await _mainPageService.GetTopMainPageNews(limit));
        }

        [HttpGet("getweatherinfo")]
        public IActionResult GetWeatherInfo()
        {
            return GetResponse(_mainPageService.GetWeatherInfo());
        }

        [HttpGet("getstorynews")]
        public async Task<IActionResult> GetStoryNews(int limit = 10)
        {
            return GetResponse(await _mainPageService.GetStoryNewsAsync(limit));
        }

        [HttpGet("gettopmainpagefourhillnews")]
        public async Task<IActionResult> GetTopMainPageFourHillNews(int limit = 10)
        {
            return GetResponse(await _mainPageService.GetTopMainPageFourHillNews(limit));
        }

        [HttpGet("getwideheadingnews")]
        public async Task<IActionResult> GetTopWideHeadingNews(int limit = 10)
        {
            return GetResponse(await _mainPageService.GetTopWideHeadingNews(limit));
        }

    }
}