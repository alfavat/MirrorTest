using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using WebMobileUI.Repository.Abstract;

namespace WebMobileUI.ViewComponents
{
    public class StoryNewsViewComponent : ViewComponent
    {
        private readonly IMainPageRepository _mainPageRepository;
        public StoryNewsViewComponent(IMainPageRepository mainPageRepository)
        {
            _mainPageRepository = mainPageRepository;
        }

        public ViewViewComponentResult Invoke()
        {
            var result = _mainPageRepository.GetStoryNewsList(20);
            return View(result.Data);
        }
    }
}
