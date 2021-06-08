using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using System.Threading.Tasks;
using WebUI.Repository.Abstract;

namespace WebUI.ViewComponents
{
    public class MostSharedNewsViewComponent : ViewComponent
    {
        private readonly INewsDetailPageRepository _newsDetailPageRepository;
        public MostSharedNewsViewComponent(INewsDetailPageRepository newsDetailPageRepository)
        {
            _newsDetailPageRepository = newsDetailPageRepository;
        }

        public async Task<ViewViewComponentResult> InvokeAsync()
        {
            var result = await _newsDetailPageRepository.GetMostSharedNewsList(8);
            if (result.DataResultIsNotNull())
            {
                return View(result.Data);
            }
            return View();
        }
    }
}
