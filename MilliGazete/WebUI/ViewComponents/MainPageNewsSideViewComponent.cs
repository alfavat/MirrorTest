using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using WebUI.Repository.Abstract;
using System.Threading.Tasks;
namespace WebUI.ViewComponents
{
    public class MainPageNewsSideViewComponent : ViewComponent
    {
        private readonly IMainPageRepository _mainPageRepository;
        public MainPageNewsSideViewComponent(IMainPageRepository mainPageRepository)
        {
            _mainPageRepository = mainPageRepository;
        }


        public async Task<ViewViewComponentResult> InvokeAsync()
        {
            var result = await _mainPageRepository.GetMainPageSideNews(2);
            if (result.DataResultIsNotNull())
            {
                return View(result.Data);
            }
            return View();
        }
    }
}
