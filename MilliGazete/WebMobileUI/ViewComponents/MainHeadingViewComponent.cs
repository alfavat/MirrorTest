using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using System.Collections.Generic;
using WebMobileUI.Models;
using WebMobileUI.Repository.Abstract;

namespace WebMobileUI.ViewComponents
{
    public class MainHeadingViewComponent : ViewComponent
    {
        private readonly IMainPageRepository _mainPageRepository;
        private readonly ICategoryPageRepository _categoryPageRepository;
        public MainHeadingViewComponent(IMainPageRepository mainPageRepository, ICategoryPageRepository categoryPageRepository)
        {
            _mainPageRepository = mainPageRepository;
            _categoryPageRepository = categoryPageRepository;
        }

        public ViewViewComponentResult Invoke(string url = "")
        {
            if (url.StringIsNullOrEmpty())
            {
                List<CustomHeading> list = new List<CustomHeading>();
                var subResult = _mainPageRepository.GetSubHeadingNews(5);
                if (subResult.Success && subResult.Data != null) list.AddRange(subResult.Data);

                var result = _mainPageRepository.GetMainHeadingNews(13); 
                if (result.Success && result.Data!=null) list.AddRange(result.Data);
                return View(list);
            }
            else
            {
                var result = _categoryPageRepository.GetLastMainHeadingNewsByCategoryUrl(url, 15);
                return View(result.Data);
            }
        }
    }
}
