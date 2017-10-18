using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TaskItSite.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;

namespace TaskItSite.Controllers
{
    public class HomeController : Controller
    {
        #region Dependency injection
        private readonly UserManager<ApplicationUser> _userManager = null;
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        public HomeController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        #endregion



        public async Task<IActionResult> Index()
        { 
            if (HttpContext.Session.Get("sentToHomePage") == null)
            {
                var currentUser = await GetCurrentUserAsync();

                if (currentUser != null)
                {
                    // Send user to their chosen homepage upon login
                    HttpContext.Session.SetString("sentToHomePage", "sent");
                    return LocalRedirect(currentUser.GetHomeScreenURL());
                }
                else
                    return View();

            }
            else
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

        [Authorize]
        public async Task<IActionResult> Feed()
        {
            // Feed for the current user
            var currentUser = await GetCurrentUserAsync();

            return View(currentUser);
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
    }
}
