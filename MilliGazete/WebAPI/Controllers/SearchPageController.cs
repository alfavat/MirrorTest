using Business.Managers.Abstract;
using Entity.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchPageController : MainController
    {
        private readonly ISearchPageService _searchPage;
        public SearchPageController(ISearchPageService searchPage)
        {
            _searchPage = searchPage;
        }

        [HttpGet("getnewsbytagurl")]
        public async Task<IActionResult> GetNewsByTagUrl(string url, int limit)
        {
            return GetResponse(await _searchPage.GetNewsByTagUrl(url, limit));
        }

        [HttpGet("getlistbypaging")]
        public IActionResult GetListByPaging(string query, int limit = 10, string orderBy = "Id", int page = 1, int ascending = 1)
        {
            return GetResponse(_searchPage.GetListByPaging(new NewsPagingDto()
            {
                Query = query,
                Limit = limit,
                OrderBy = orderBy + (ascending == 1 ? " ascending" : " descending"),
                PageNumber = page
            }, out int total),total);
        }

    }
}
