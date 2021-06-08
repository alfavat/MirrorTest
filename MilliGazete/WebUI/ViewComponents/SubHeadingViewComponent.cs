using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using WebUI.Repository.Abstract;

namespace WebUI.ViewComponents
{
    public class SubHeadingViewComponent : ViewComponent
    {
        private readonly IMainPageRepository _mainPageRepository;
        public SubHeadingViewComponent(IMainPageRepository mainPageRepository)
        {
            _mainPageRepository = mainPageRepository;
        }

        public async Task<ViewViewComponentResult> InvokeAsync()
        {
            var result = await _mainPageRepository.GetSubHeadingNews(5);
            if (result.DataResultIsNotNull())
            {
                return View(result.Data);
            }
            return View();
        }
    }
}
