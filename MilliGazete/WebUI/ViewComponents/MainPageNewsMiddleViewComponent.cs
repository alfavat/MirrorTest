using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using WebUI.Repository.Abstract;
using System.Threading.Tasks;
using WebUI.Models;

namespace WebUI.ViewComponents
{
    public class MainPageNewsMiddleViewComponent : ViewComponent
    {
        private readonly IMainPageRepository _mainPageRepository;
        public MainPageNewsMiddleViewComponent(IMainPageRepository mainPageRepository)
        {
            _mainPageRepository = mainPageRepository;
        }

        public async Task<ViewViewComponentResult> InvokeAsync()
        {
            MainPageNewsModel rtrn = new MainPageNewsModel();
            var result = await _mainPageRepository.GetTopMainPageNews(12);
            if (result.DataResultIsNotNull())
            {
                rtrn.mainPageNews = result.Data;
            }

            var result2 = await _mainPageRepository.GetTopWideHeadingNews(4);
            if (result2.DataResultIsNotNull())
            {
                rtrn.mainPageWideNews = result2.Data;
            }
            return View(rtrn);
        }
    }
}
