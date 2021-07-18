using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using System.Threading.Tasks;
using WebUI.Repository.Abstract;
namespace WebUI.ViewComponents
{
    public class MainPagePoliticsHeadingViewComponent : ViewComponent
    {
        private readonly ICategoryPageRepository _categoryPageRepository;
        public MainPagePoliticsHeadingViewComponent(ICategoryPageRepository categoryPageRepository, IMainPageRepository mainPageRepository)
        {
            _categoryPageRepository = categoryPageRepository;
        }
        public async Task<ViewViewComponentResult> InvokeAsync(string url = "siyaset")
        {
            var result = await _categoryPageRepository.GetLastMainHeadingNewsByCategoryUrl(url, 10);
            if (result.DataResultIsNotNull())
            {
                return View(result.Data);
            }
            return View();
        }
    }
}
