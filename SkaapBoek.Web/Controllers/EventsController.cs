using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkaapBoek.Web.Controllers
{
    public class EventsController : Controller
    {
        public IActionResult Overview()
        {
            return View();
        }
    }
}
