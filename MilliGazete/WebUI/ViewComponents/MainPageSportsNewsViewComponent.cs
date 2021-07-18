using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using WebUI.Repository.Abstract;
using System.Threading.Tasks;
namespace WebUI.ViewComponents
{
    public class MainPageSportsNewsViewComponent : ViewComponent
    {
        private readonly ICategoryPageRepository _categoryPageRepository;
        public MainPageSportsNewsViewComponent(ICategoryPageRepository categoryPageRepository)
        {
            _categoryPageRepository = categoryPageRepository;
        }
        public async Task<ViewViewComponentResult> InvokeAsync(string url = "spor")
        {
            var result = await _categoryPageRepository.GetLastNewsByCategoryUrl(url, 6);
            return View(result.Data);
        }
    }
}
