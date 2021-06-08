using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebUI.Repository.Abstract;

namespace WebUI.Controllers
{
    public class AuthorsController : Controller
    {
        public AuthorsController(IAuthorPageRepository authorPageRepository)
        {
            _authorPageRepository = authorPageRepository;
        }

        private readonly IAuthorPageRepository _authorPageRepository;

        [Route("yazarlar")]
        public async Task<IActionResult> Index()
        {
            var result = await _authorPageRepository.GetAuthorList();
            if (result.DataResultIsNotNull())
            {
                return View(result.Data);
            }
            return View();
        }

        [Route("yazar/{nameSurename}")]
        public async Task<IActionResult> AuthorDetails(string nameSurename)
        {
            var result = await _authorPageRepository.GetAuthorByName(nameSurename.FromUrlFormat());
            if (result.DataResultIsNotNull())
            {
                return View(result.Data);
            }
            return View();
        }
    }
}
