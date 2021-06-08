using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebUI.Repository.Abstract;

namespace WebUI.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryPageRepository _categoryPageRepository;

        public CategoriesController(ICategoryPageRepository categoryPageRepository)
        {
            _categoryPageRepository = categoryPageRepository;
        }

        [Route("{url?}")]
        public async Task<IActionResult> Index(string url = "")
        {
            if (url.StringIsNullOrEmpty())
            {
                return Redirect("/index");
            }

            var result = await _categoryPageRepository.GetByUrl(url);
            if (result.DataResultIsNotNull())
            {
                return View(result.Data);
            }
            return Redirect("/index");
        }
    }
}
