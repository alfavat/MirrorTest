using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebUI.Repository.Abstract;

namespace WebUI.Controllers
{
    public class PageController : Controller
    {
        private readonly IPageRepository _pageRepository;

        public PageController(IPageRepository pageRepository)
        {
            _pageRepository = pageRepository;
        }

        [Route("sayfa/{url}")]
        public async Task<IActionResult> Index(string url = "")
        {
            if (url.StringIsNullOrEmpty())
            {
                return Redirect("/index");
            }

            var result = await _pageRepository.GetByUrl(url);
            if (result.DataResultIsNotNull())
            {
                return View(result.Data);
            }
            return View("NotFound");//404 sayfasına yönlendirme olacak
        }
    }
}
