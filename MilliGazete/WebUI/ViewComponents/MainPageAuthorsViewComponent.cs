using Entity.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebUI.Repository.Abstract;

namespace WebUI.ViewComponents
{
    public class MainPageAuthorsViewComponent : ViewComponent
    {
        private readonly IAuthorPageRepository _authorPageRepository;
        public MainPageAuthorsViewComponent(IAuthorPageRepository authorPageRepository)
        {
            _authorPageRepository = authorPageRepository;
        }
        public async Task<ViewViewComponentResult> InvokeAsync()
        {
            var result = await _authorPageRepository.GetAuthorList();
            if (result.DataResultIsNotNull())
            {
                var data = result.Data;
                if (data.Count > 6) data.RemoveRange(6, data.Count - 1);
                return View(data);
            }
            return View();
        }
    }
}
