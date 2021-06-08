using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using WebMobileUI.Repository.Abstract;

namespace WebMobileUI.ViewComponents
{
    public class MainPageNewsViewComponent : ViewComponent
    {
        private readonly IMainPageRepository _mainPageRepository;
        public MainPageNewsViewComponent(IMainPageRepository mainPageRepository)
        {
            _mainPageRepository = mainPageRepository;
        }

        public ViewViewComponentResult Invoke()
        {
            var result = _mainPageRepository.GetTopMainPageNews(25);
            return View(result.Data);
        }
    }
}
