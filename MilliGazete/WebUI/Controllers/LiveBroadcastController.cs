using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    public class LiveBroadcastController : Controller
    {
        [Route("canli-yayin")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
