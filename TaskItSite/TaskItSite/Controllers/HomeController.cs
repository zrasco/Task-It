using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TaskItSite.Models;
using Microsoft.AspNetCore.Authorization;

namespace TaskItSite.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Login()
        {
            ViewData["Message"] = "Your login page.";

            //return Challenge("Google");

            return View();
        }

        public IActionResult Journal()
        {
            ViewData["Message"] = "Your journal.";

            return View();
        }

        public IActionResult Subscriptions()
        {
            ViewData["Message"] = "Your subscriptions.";

            return View();
        }

        public IActionResult Achievements()
        {
            ViewData["Message"] = "Your achievements.";

            return View();
        }

        public IActionResult Options()
        {
            ViewData["Message"] = "Your options.";

            return View();
        }
   
    }
}
