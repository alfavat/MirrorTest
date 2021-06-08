using Microsoft.AspNetCore.Mvc;
using WebMobileUI.Repository.Abstract;

namespace WebMobileUI.Controllers
{
    public class AuthorController : Controller
    {
        public AuthorController(IAuthorPageRepository authorPageRepository)
        {
            _authorPageRepository = authorPageRepository;
        }

        private readonly IAuthorPageRepository _authorPageRepository;

        [Route("yazarlar")]
        public IActionResult Index()
        {
            var result = _authorPageRepository.GetAuthorList();
            if (result.DataResultIsNotNull())
            {
                return View(result.Data);
            }
            return View();
        }

        [Route("yazar/{nameSurename}")]
        public IActionResult AuthorDetails(string nameSurename)
        {
            var result = _authorPageRepository.GetAuthorByName(nameSurename.FromUrlFormat());
            if (result.DataResultIsNotNull())
            {
                return View(result.Data);
            }
            return View();
        }
    }
}
