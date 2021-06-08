using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMobileUI.Controllers
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
