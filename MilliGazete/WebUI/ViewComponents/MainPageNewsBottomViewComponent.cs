using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using System.Threading.Tasks;
using WebUI.Models;
using WebUI.Repository.Abstract;
namespace WebUI.ViewComponents
{
    public class MainPageNewsBottomViewComponent : ViewComponent
    {
        private readonly IMainPageRepository _mainPageRepository;
        public MainPageNewsBottomViewComponent(IMainPageRepository mainPageRepository)
        {
            _mainPageRepository = mainPageRepository;
        }

        public async Task<ViewViewComponentResult> InvokeAsync()
        {
            MainPageNewsModel rtrn = new MainPageNewsModel();
            var result = await _mainPageRepository.GetTopMainPageNews(24);
            if (result.DataResultIsNotNull())
            {
                var resultData = result.Data;
                if (resultData.Count > 12) resultData.RemoveRange(0, 12);
                rtrn.mainPageNews = resultData;
            }

            var result2 = await _mainPageRepository.GetTopWideHeadingNews(8);
            if (result2.DataResultIsNotNull() && result2.Data.HasValue())
            {
                var result2Data = result2.Data;
                if (result2Data.Count > 4) result2Data.RemoveRange(0, 4);
                rtrn.mainPageWideNews = result2Data;
            }
            return View(rtrn);
        }
    }
}
