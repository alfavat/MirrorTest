using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebUI.Repository.Abstract;

namespace WebUI.Controllers
{
    public class ArticleController : Controller
    {
        public ArticleController(IArticlePageRepository articlePageRepository)
        {
            _articlePageRepository = articlePageRepository;
        }

        private readonly IArticlePageRepository _articlePageRepository;
               
        [Route("makale/{url}")]
        public async Task<IActionResult> ArticleDetails(string url)
        {
            var result = await _articlePageRepository.GetArticleByUrl(url);
            if (result.DataResultIsNotNull())
            {
                return View(result.Data);
            }
            return View();
        }
    }
}
