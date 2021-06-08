using Microsoft.AspNetCore.Mvc;
using WebMobileUI.Models;
using WebMobileUI.Repository.Abstract;

namespace WebMobileUI.Controllers
{
    public class SearchNewsController : Controller
    {
        private readonly ISearchPageRepository _searchPageRepository;
        private int limit = 10;
        public SearchNewsController(ISearchPageRepository searchPageRepository)
        {
            _searchPageRepository = searchPageRepository;
        }

        [Route("haber")]
        public IActionResult SearchNews(string ara = "")
        {
            if (ara.StringIsNullOrEmpty())
            {
                return Redirect("/index");
            }
            var result = _searchPageRepository.GetListByPaging(ara, limit, "Id", 1, 1);
            if (result != null && result.Data != null && result.Data.Data != null)
            {
                SearchNews tagNews = new SearchNews()
                {
                    Query = ara,
                    Data = result.Data.Data,
                    TotalCount = result.Data.Count,
                    Limit = limit
                };
                return View(tagNews);
            }
            return Redirect("/index");
        }

        [Route("haber-sayfa")]
        public PartialViewResult SearchNewsPaging(string query = "", int page = 1)
        {
            var data = _searchPageRepository.GetListByPaging(query, limit, "id", page, 1);
            if (data.DataResultIsNotNull())
            {
                return PartialView("_SearchNews", data.Data.Data);
            }
            return PartialView("_SearchNews");
        }

        [Route("etiket/{url}")]
        public IActionResult SearchNewsByTag(string url = "")
        {
            if (url.StringIsNullOrEmpty())
            {
                return Redirect("/index");
            }

            var result = _searchPageRepository.GetNewsByTagUrl(url, 20);
            if (result.DataResultIsNotNull())
            {
                TagNews tagNews = new TagNews()
                {
                    Tag = url,
                    Data = result.Data
                };
                return View(tagNews);
            }
            return Redirect("/index");
        }
    }
}
