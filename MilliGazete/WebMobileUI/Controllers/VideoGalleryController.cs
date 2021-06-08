using Microsoft.AspNetCore.Mvc;
using WebMobileUI.Repository.Abstract;

namespace WebMobileUI.Controllers
{
    public class VideoGalleryController : Controller
    {
        private readonly IMainPageRepository _mainPageRepository;

        public VideoGalleryController(IMainPageRepository mainPageRepository)
        {
            _mainPageRepository = mainPageRepository;
        }

        [Route("video")]
        [Route("video-galeri")]
        public IActionResult Index()
        {
            var result = _mainPageRepository.GetTopVideoNews(20);
            if (result.DataResultIsNotNull())
            {
                return View(result.Data);
            }
            return Redirect("/index");
        }
    }
}
