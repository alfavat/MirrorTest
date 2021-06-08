using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using WebUI.Repository.Abstract;

namespace WebUI.ViewComponents
{
    public class SubHeading2ViewComponent : ViewComponent
    {
        private readonly IMainPageRepository _mainPageRepository;
        public SubHeading2ViewComponent(IMainPageRepository mainPageRepository)
        {
            _mainPageRepository = mainPageRepository;
        }

        public async Task<ViewViewComponentResult> InvokeAsync()
        {
            var result = await _mainPageRepository.GetSubHeadingNews2(5);
            if (result.DataResultIsNotNull())
            {
                return View(result.Data);
            }
            return View();
        }
    }
}
