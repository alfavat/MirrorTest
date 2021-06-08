using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebUI.Repository.Abstract;

namespace WebUI.Controllers
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
        public async Task<IActionResult> Index()
        {
            var result = await _mainPageRepository.GetTopVideoNews(20);
            if (result.DataResultIsNotNull())
            {
                return View(result.Data);
            }
            return Redirect("/index");
        }
    }
}
