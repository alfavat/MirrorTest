using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using System.Threading.Tasks;
using WebUI.Repository.Abstract;
namespace WebUI.ViewComponents
{
    public class MainHeadingViewComponent : ViewComponent
    {
        private readonly IMainPageRepository _mainPageRepository;
        private readonly ICategoryPageRepository _categoryPageRepository;
        public MainHeadingViewComponent(ICategoryPageRepository categoryPageRepository, IMainPageRepository mainPageRepository)
        {
            _mainPageRepository = mainPageRepository;
            _categoryPageRepository = categoryPageRepository;
        }
        public async Task<ViewViewComponentResult> InvokeAsync(string url = "")
        {
            if (url.StringIsNullOrEmpty())
            {
                var result = await _mainPageRepository.GetMainHeadingNews(15);
                if (result.DataResultIsNotNull())
                {
                    return View(result.Data);
                }
                return View();
            }
            else
            {
                var result = await _categoryPageRepository.GetLastMainHeadingNewsByCategoryUrl(url, 15);
                if (result.DataResultIsNotNull())
                {
                    return View(result.Data);
                }
                return View();
            }
        }
    }
}
