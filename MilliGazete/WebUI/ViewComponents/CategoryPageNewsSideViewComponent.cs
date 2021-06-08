using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using WebUI.Repository.Abstract;
using System.Threading.Tasks;
namespace WebUI.ViewComponents
{
    public class CategoryPageNewsSideViewComponent : ViewComponent
    {
        private readonly ICategoryPageRepository _categoryPageRepository;
        public CategoryPageNewsSideViewComponent(ICategoryPageRepository categoryPageRepository)
        {
            _categoryPageRepository = categoryPageRepository;
        }

        public async Task<ViewViewComponentResult> InvokeAsync(string url = "")
        {
            var result = await _categoryPageRepository.GetLastNewsByCategoryUrl(url, 20);
            if (result.DataResultIsNotNull())
            {
                return View(result.Data);
            }
            return View();
        }
    }
}
