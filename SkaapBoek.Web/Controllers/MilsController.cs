using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkaapBoek.Web.Controllers
{
    public class MilsController : Controller
    {
        public MilsController()
        {

        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
