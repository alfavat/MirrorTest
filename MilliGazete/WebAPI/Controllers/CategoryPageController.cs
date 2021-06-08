using Business.Managers.Abstract;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryPageController : MainController
    {
        private readonly ICategoryPageService _categoryPageService;
        public CategoryPageController(ICategoryPageService categoryPageService)
        {
            _categoryPageService = categoryPageService;
        }

        [HttpGet("getlastmainheadingnewsbycategoryurl")]
        public async Task<IActionResult> GetLast15MainHeadingNewsByCategoryUrl(string url, int limit)
        {
            return GetResponse(await _categoryPageService.GetLastMainHeadingNewsByCategoryUrl(url, limit));
        }

        [HttpGet("getlastnewsbycategoryurl")]
        public async Task<IActionResult> GetLast20NewsByCategoryUrl(string url, int limit)
        {
            return GetResponse(await _categoryPageService.GetLastNewsByCategoryUrl(url, limit));
        }

        [HttpGet("getlastmainheadingnewsbycategoryid")]
        public async Task<IActionResult> GetLast15MainHeadingNewsByCategoryId(int id, int limit)
        {
            return GetResponse(await _categoryPageService.GetLastMainHeadingNewsByCategoryId(id, limit));
        }

        [HttpGet("getlastnewsbycategoryid")]
        public async Task<IActionResult> GetLast20NewsByCategoryId(int id, int limit)
        {
            return GetResponse(await _categoryPageService.GetLastNewsByCategoryId(id, limit));
        }

        [HttpGet("getbyurl")]
        public async Task<IActionResult> GetByUrl(string url)
        {
            return GetResponse(await _categoryPageService.GetByUrl(url));
        }

        [HttpGet("getbyid")]
        public async Task<IActionResult> GetById(int id)
        {
            return GetResponse(await _categoryPageService.GetById(id));
        }
    }
}