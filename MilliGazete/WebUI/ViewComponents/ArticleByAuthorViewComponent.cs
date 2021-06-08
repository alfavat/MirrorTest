using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using System.Threading.Tasks;
using WebUI.Repository.Abstract;

namespace WebUI.ViewComponents
{
    public class ArticleByAuthorViewComponent : ViewComponent
    {
        private readonly IAuthorPageRepository _authorPageRepository;
        public ArticleByAuthorViewComponent(IAuthorPageRepository authorPageRepository)
        {
            _authorPageRepository = authorPageRepository;
        }
        public async Task<ViewViewComponentResult> InvokeAsync(string nameSurename = "")
        {
            var result = await _authorPageRepository.GetAuthorByName(nameSurename.FromUrlFormat());
            if (result.DataResultIsNotNull())
            {
                return View(result.Data);
            }
            return View();
        }
    }
}
