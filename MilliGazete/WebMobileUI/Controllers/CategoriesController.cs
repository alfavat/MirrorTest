using Microsoft.AspNetCore.Mvc;
using WebMobileUI.Repository.Abstract;

namespace WebMobileUI.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryPageRepository _categoryPageRepository;

        public CategoriesController(ICategoryPageRepository categoryPageRepository)
        {
            _categoryPageRepository = categoryPageRepository;
        }

        [Route("{url?}")]
        public IActionResult Index(string url = "")
        {
            if (url.StringIsNullOrEmpty())
            {
                return Redirect("/index");
            }

            var result = _categoryPageRepository.GetByUrl(url);
            if (result.DataResultIsNotNull())
            {
                return View(result.Data);
            }
            return Redirect("/index");
        }
    }
}
