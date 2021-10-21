using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using WebUI.Repository.Abstract;

namespace WebUI.ViewComponents
{
    public class MainPageMostViewedNewsViewComponent : ViewComponent
    {
        private readonly INewsDetailPageRepository _newsDetailPageRepository;
        public MainPageMostViewedNewsViewComponent(INewsDetailPageRepository newsDetailPageRepository)
        {
            _newsDetailPageRepository = newsDetailPageRepository;
        }

        public async Task<ViewViewComponentResult> InvokeAsync()
        {
            var result = await _newsDetailPageRepository.GetLastWeekMostViewedNews(4);
            if (result.DataResultIsNotNull())
            {
                return View(result.Data);
            }
            return View();
        }
    }
}
