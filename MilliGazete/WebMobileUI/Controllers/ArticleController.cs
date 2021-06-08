using Microsoft.AspNetCore.Mvc;
using WebMobileUI.Repository.Abstract;

namespace WebMobileUI.Controllers
{
    public class ArticleController : Controller
    {
        public ArticleController(IArticlePageRepository articlePageRepository)
        {
            _articlePageRepository = articlePageRepository;
        }

        private readonly IArticlePageRepository _articlePageRepository;
               
        [Route("makale/{url}")]
        public IActionResult ArticleDetails(string url)
        {
            var result = _articlePageRepository.GetArticleByUrl(url);
            if (result.DataResultIsNotNull())
            {
                return View(result.Data);
            }
            return View();
        }
    }
}
