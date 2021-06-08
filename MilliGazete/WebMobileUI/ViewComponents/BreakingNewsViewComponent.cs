using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using WebMobileUI.Repository.Abstract;

namespace WebMobileUI.ViewComponents
{
    public class BreakingNewsViewComponent : ViewComponent
    {
        private readonly IMainPageRepository _mainPageRepository;
        public BreakingNewsViewComponent(IMainPageRepository mainPageRepository)
        {
            _mainPageRepository = mainPageRepository;
        }
        public ViewViewComponentResult Invoke()
        {
            var result = _mainPageRepository.GetTopBreakingNews(10);
            return View(result.Data);
        }
    }
}
