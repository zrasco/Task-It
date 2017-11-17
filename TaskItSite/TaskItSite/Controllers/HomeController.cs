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
using TaskItSite.Data;
using TaskItSite.Models.MainViewModels;

namespace TaskItSite.Controllers
{
    public class HomeController : Controller
    {
        #region Dependency injection
        private readonly ApplicationDbContext _appDbContext = null;

        private readonly UserManager<ApplicationUser> _userManager = null;
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        public HomeController(UserManager<ApplicationUser> userManager, ApplicationDbContext appDbContext)
        {
            _userManager = userManager;
            _appDbContext = appDbContext;
        }
        #endregion

        [TempData]
        public string StatusMessage { get; set; }

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

        [HttpGet]
        public async Task<IActionResult> Subscriptions()
        {
            ViewData["Message"] = "Your Subscriptions.";

            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            // Subscriptions must be loaded explicitly due to lazy loading. Would optimize in production app
            _appDbContext.Entry(user).Collection(x => x.Subs).Load();

            var model = new SubscriptionsViewModel
            {
                SubscriptionWrapperList = new List<SubscriptionWrapper>(),
                CurrentUser = user,
                StatusMessage = StatusMessage
            };

            foreach (GlobalSubscription a in _appDbContext.GlobalSubscriptions)
            {
                SubscriptionWrapper newW = new SubscriptionWrapper
                {
                    Subscription = a
                };

                // Find matching Subscription
                Subscription targetSubscription = model.CurrentUser.Subs.Where(x => x.GlobalSubscriptionID == a.GlobalSubscriptionID).SingleOrDefault();

                if (targetSubscription != null)
                    newW.IsSubscribed = true;
                else
                    newW.IsSubscribed = false;

                model.SubscriptionWrapperList.Add(newW);
            }


            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Subscriptions(SubscriptionsViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            // Subscriptions must be loaded explicitly due to lazy loading. Would optimize in production app
            _appDbContext.Entry(user).Collection(x => x.Subs).Load();

            for (int i = 0; i < model.SubscriptionWrapperList.Count; i++)
            {

                // Find matching Subscription. Don't use foreach here since it gets messed up by the .Where() below
                SubscriptionWrapper aw = model.SubscriptionWrapperList[i];
                Subscription targetSubscription = user.Subs.Where(x => x.GlobalSubscriptionID == aw.Subscription.GlobalSubscriptionID).SingleOrDefault();

                // Only update if there's a change
                if (targetSubscription == null && aw.IsSubscribed == true)
                {
                    Subscription toAdd = new Subscription
                    {
                        GlobalSubscriptionID = aw.Subscription.GlobalSubscriptionID,
                        SubscribedTime = DateTime.Now,
                        ApplicationUserID = user.Id
                    };

                    user.Subs.Add(toAdd);
                }
                else if (targetSubscription != null && aw.IsSubscribed == false)
                    user.Subs.Remove(targetSubscription);

            }


            var setResult = await _userManager.UpdateAsync(user);

            if (!setResult.Succeeded)
            {
                throw new ApplicationException($"Unexpected error occurred setting Subscriptions for user with ID '{user.Id}'.");
            }

            StatusMessage = "Your settings have been updated";
            return RedirectToAction(nameof(Subscriptions));
        }

        [HttpGet]
        public async Task<IActionResult> Achievements()
        {
            ViewData["Message"] = "Your achievements.";

            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            // Achievements must be loaded explicitly due to lazy loading. Would optimize in production app
            _appDbContext.Entry(user).Collection(x => x.Achivements).Load();
            
            var model = new AchievementsViewModel
            {
                AchievementWrapperList = new List<AchievementWrapper>(),
                CurrentUser = user,
                StatusMessage = StatusMessage
            };

            foreach (GlobalAchievement a in _appDbContext.GlobalAchievements)
            {
                AchievementWrapper newW = new AchievementWrapper
                {
                    Achievement = a
                };

                // Find matching achievement
                UserAchievement targetAchievement = model.CurrentUser.Achivements.Where(x => x.GlobalAchievementID == a.GlobalAchievementID).SingleOrDefault();

                if (targetAchievement != null)
                    newW.IsAchieved = true;
                else
                    newW.IsAchieved = false;

                model.AchievementWrapperList.Add(newW);
            }
                

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Achievements(AchievementsViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            // Achievements must be loaded explicitly due to lazy loading. Would optimize in production app
            _appDbContext.Entry(user).Collection(x => x.Achivements).Load();

            for (int i = 0; i < model.AchievementWrapperList.Count; i++)
            {
                
                // Find matching achievement. Don't use foreach here since it gets messed up by the .Where() below
                AchievementWrapper aw = model.AchievementWrapperList[i];
                UserAchievement targetAchievement = user.Achivements.Where(x => x.GlobalAchievementID == aw.Achievement.GlobalAchievementID).SingleOrDefault();

                // Only update if there's a change
                if (targetAchievement == null && aw.IsAchieved == true)
                {
                    UserAchievement toAdd = new UserAchievement
                    {
                        GlobalAchievementID = aw.Achievement.GlobalAchievementID,
                        AchievedTime = DateTime.Now,
                        ApplicationUserID = user.Id
                    };

                    user.Achivements.Add(toAdd);
                }
                else if (targetAchievement != null && aw.IsAchieved == false)
                    user.Achivements.Remove(targetAchievement);
               
            }
            

            var setResult = await _userManager.UpdateAsync(user);

            if (!setResult.Succeeded)
            {
                throw new ApplicationException($"Unexpected error occurred setting achievements for user with ID '{user.Id}'.");
            }

            StatusMessage = "Your settings have been updated";
            return RedirectToAction(nameof(Achievements));
        }
    }
}
