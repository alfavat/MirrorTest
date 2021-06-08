using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using System.Threading.Tasks;
using WebUI.Repository.Abstract;

namespace WebUI.ViewComponents
{
    public class VideoNewsViewComponent : ViewComponent
    {
        private readonly IMainPageRepository _mainPageRepository;
        public VideoNewsViewComponent(IMainPageRepository mainPageRepository)
        {
            _mainPageRepository = mainPageRepository;
        }

        public async Task<ViewViewComponentResult> InvokeAsync()
        {
            var result = await _mainPageRepository.GetTopVideoNews(4);
            if (result.DataResultIsNotNull())
            {
                return View(result.Data);
            }
            return View();
        }
    }
}
