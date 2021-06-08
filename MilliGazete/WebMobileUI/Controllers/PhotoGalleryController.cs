using Microsoft.AspNetCore.Mvc;
using WebMobileUI.Repository.Abstract;

namespace WebMobileUI.Controllers
{
    public class PhotoGalleryController : Controller
    {
        private readonly IMainPageRepository _mainPageRepository;

        public PhotoGalleryController(IMainPageRepository mainPageRepository)
        {
            _mainPageRepository = mainPageRepository;
        }

        [Route("foto")]
        [Route("foto-galeri")]
        public IActionResult Index()
        {
            var result = _mainPageRepository.GetTopImageNews(20);
            if (result.DataResultIsNotNull())
            {
                return View(result.Data);
            }
            return Redirect("/index");
        }
    }
}
