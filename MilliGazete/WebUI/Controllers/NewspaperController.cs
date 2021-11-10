using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebUI.Models;
using WebUI.Repository.Abstract;

namespace WebUI.Controllers
{
    public class NewspaperController : Controller
    {
        private readonly INewspaperRepository _newspaperRepository;
        private readonly IPageRepository _pageRepository;

        public NewspaperController(INewspaperRepository newspaperRepository, IPageRepository pageRepository)
        {
            _newspaperRepository = newspaperRepository;
            _pageRepository = pageRepository;
        }

        [Route("gazete-mansetleri")]
        public async Task<IActionResult> AllNewspapers()
        {
            NewspapersModel model = new NewspapersModel();
            var newspapers = await _newspaperRepository.GetList();
            model.Newspapers = newspapers.Data;
            model.Page = _pageRepository.GetByUrl("gazete-mansetleri").Result.Data;
            return View(model);
        }

        [Route("bugunku-milli-gazete")]
        public async Task<IActionResult> MilliGazeteNewspaper()
        {
            NewspapersModel model = new NewspapersModel();
            var newspapers = await _newspaperRepository.GetMilliGazeteNewspaper();
            model.Newspaper = newspapers.Data;
            model.Page = _pageRepository.GetByUrl("bugunku-milli-gazete").Result.Data;
            return View(model);
        }
    }
}
