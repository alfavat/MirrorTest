using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using WebUI.Repository.Abstract;

namespace WebUI.ViewComponents
{
    public class MostViewedArticlesViewComponent : ViewComponent
    {
        private readonly IArticlePageRepository _articlePageRepository;
        public MostViewedArticlesViewComponent(IArticlePageRepository articlePageRepository)
        {
            _articlePageRepository = articlePageRepository;
        }

        public async Task<ViewViewComponentResult> InvokeAsync()
        {
            var result = await _articlePageRepository.GetLastWeekMostViewedArticles(5);
            if (result.DataResultIsNotNull())
            {
                return View(result.Data);
            }
            return View();
        }
    }
}
