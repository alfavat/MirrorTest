using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using System.Threading.Tasks;
using WebUI.Models;
using WebUI.Repository.Abstract;

namespace WebUI.ViewComponents
{
    public class BreakingNewsViewComponent : ViewComponent
    {
        private readonly IMainPageRepository _mainPageRepository;
        public BreakingNewsViewComponent(IMainPageRepository mainPageRepository)
        {
            _mainPageRepository = mainPageRepository;
        }
        public async Task<ViewViewComponentResult> InvokeAsync()
        {
            BreakingNewsModel model = new BreakingNewsModel();
            var flashNews = await _mainPageRepository.GetLastFlashNews(1);
            var breakingNews = await _mainPageRepository.GetTopBreakingNews(10);
            
            model.FlashNews = flashNews.Data;
            model.BreakingNews = breakingNews.Data;
            return View(model);
        }
    }
}
