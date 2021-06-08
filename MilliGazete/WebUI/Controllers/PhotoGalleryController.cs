using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebUI.Repository.Abstract;

namespace WebUI.Controllers
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
        public async Task<IActionResult> Index()
        {
            var result = await _mainPageRepository.GetTopImageNews(20);
            if (result.DataResultIsNotNull())
            {
                return View(result.Data);
            }
            return Redirect("/index");
        }
    }
}
