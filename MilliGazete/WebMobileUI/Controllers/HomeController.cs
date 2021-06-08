using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using System;
using WebMobileUI.Repository.Abstract;

namespace WebMobileUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMainPageRepository _mainPageRepository;
        public HomeController(IMainPageRepository mainPageRepository)
        {
            _mainPageRepository = mainPageRepository;
        }

        [Route("")]
        [Route("index")]
        [Route("index.html")]
        [Route("default")]
        public IActionResult Index()
        {
            var result = _mainPageRepository.GetStoryNewsList(20);
            return View(result.Data);
        }

        [HttpPost("setLanguage")]
        public IActionResult SetLanguage(string culture)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return Ok();
        }
    }
}
