using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using WebMobileUI.Repository.Abstract;

namespace WebMobileUI.ViewComponents
{
    public class MostViewedArticlesViewComponent : ViewComponent
    {
        private readonly IArticlePageRepository _articlePageRepository;
        public MostViewedArticlesViewComponent(IArticlePageRepository articlePageRepository)
        {
            _articlePageRepository = articlePageRepository;
        }

        public ViewViewComponentResult Invoke()
        {
            var result = _articlePageRepository.GetLastWeekMostViewedArticles(5);
            if (result.DataResultIsNotNull())
                return View(result.Data);
            return View();
        }
    }
}
