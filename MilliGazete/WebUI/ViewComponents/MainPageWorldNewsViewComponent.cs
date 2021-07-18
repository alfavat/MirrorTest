using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using WebUI.Repository.Abstract;
using System.Threading.Tasks;
namespace WebUI.ViewComponents
{
    public class MainPageWorldNewsViewComponent : ViewComponent
    {
        private readonly ICategoryPageRepository _categoryPageRepository;
        public MainPageWorldNewsViewComponent(ICategoryPageRepository categoryPageRepository)
        {
            _categoryPageRepository = categoryPageRepository;
        }
        public async Task<ViewViewComponentResult> InvokeAsync(string url = "dunya")
        {
            var result = await _categoryPageRepository.GetLastNewsByCategoryUrl(url, 8);
            return View(result.Data);
        }
    }
}
