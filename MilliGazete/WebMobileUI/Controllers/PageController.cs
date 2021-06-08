using Microsoft.AspNetCore.Mvc;
using WebMobileUI.Repository.Abstract;

namespace WebMobileUI.Controllers
{
    public class PageController : Controller
    {
        private readonly IPageRepository _pageRepository;

        public PageController(IPageRepository pageRepository)
        {
            _pageRepository = pageRepository;
        }

        [Route("sayfa/{url}")]
        public IActionResult Index(string url = "")
        {
            if (url.StringIsNullOrEmpty())
            {
                return Redirect("/index");
            }

            var result = _pageRepository.GetByUrl(url);
            if (result.DataResultIsNotNull())
            {
                return View(result.Data);
            }
            return View("NotFound");//404 sayfasına yönlendirme olacak
        }
    }
}
