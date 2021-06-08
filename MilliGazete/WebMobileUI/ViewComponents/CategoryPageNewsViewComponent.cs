using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using WebMobileUI.Repository.Abstract;

namespace WebMobileUI.ViewComponents
{
    public class CategoryPageNewsViewComponent : ViewComponent
    {
        private readonly ICategoryPageRepository _categoryPageRepository;
        public CategoryPageNewsViewComponent(ICategoryPageRepository categoryPageRepository)
        {
            _categoryPageRepository = categoryPageRepository;
        }

        public ViewViewComponentResult Invoke(string url = "")
        {
            var result = _categoryPageRepository.GetLastNewsByCategoryUrl(url, 20);
            return View(result.Data);
        }
    }
}
