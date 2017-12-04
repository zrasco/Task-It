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
using TaskItSite.Services;

namespace TaskItSite.Controllers
{ 
    public class HomeController : Controller
    {
        #region Dependency injection
        private readonly ApplicationDbContext _appDbContext = null;
        private readonly UserManager<ApplicationUser> _userManager = null;
        private readonly IImageCache _imageCache = null;
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        public HomeController(UserManager<ApplicationUser> userManager,
                                            ApplicationDbContext appDbContext,
                                            IImageCache imageCache)
        {
            _userManager = userManager;
            _appDbContext = appDbContext;
            _imageCache = imageCache;
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
                    return LocalRedirect("/Account/Login");

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
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            // Subscriptions must be explicitly loaded
            _appDbContext.Entry(user).Collection(x => x.Subs).Load();

            // Load all global achievements
            _appDbContext.GlobalAchievements.ToList();

            // Feed for the current user
            FeedViewModel model = new FeedViewModel()
            {
                CurrentUser = user,
                FeedItems = new List<FeedItem>()
            };
            
            foreach (Subscription sub in user.Subs)
            // Get each user
            {
                ApplicationUser nextUser = _userManager.Users.Where(x => x.Id == sub.SubscribingToUserID).SingleOrDefault();

                if (nextUser != null)
                // Only generate feed from valid users
                {
                    FeedItem baseFI = new FeedItem();

                    baseFI.FromUser = nextUser;

                    // See if their image URL is cached
                    baseFI.ImageData = _imageCache.GetImageEmbed(nextUser.Id);

                    if (baseFI.ImageData == null)
                    // Image isn't cached yet
                    {
                        if (nextUser.ProfileImageURL == null)
                        // Use Task-It logo
                        {
                            _imageCache.SetImage(nextUser.Id, "https://localhost:44395/images/logoattempt.png", true);
                            baseFI.ImageData = _imageCache.GetImageEmbed(nextUser.Id);
                        }
                        else
                        // Use profile image. If it fails, will fall back to task-it logo
                        {
                            _imageCache.SetImage(nextUser.Id, nextUser.ProfileImageURL,true);
                            baseFI.ImageData = _imageCache.GetImageEmbed(nextUser.Id);
                        }
                    }

                    // Create feed items for all public tasks, achievements, and posts within the last 7 days

                    // Posts
                    _appDbContext.Entry(nextUser).Collection(x => x.Posts).Load();
                    foreach (Post post in nextUser.Posts)
                    {
                        FeedItem postFI = new FeedItem(baseFI);

                        if (post.PostedTime > DateTime.Now.AddDays(-7))
                        {
                            // Add this to feed
                            postFI.Occured = post.PostedTime;
                            postFI.ItemType = FeedItemType.Post;
                            postFI.Text = post.Text;
                            postFI.Postid = post.PostID;

                            model.FeedItems.Add(postFI);

                        }

                    }

                    _appDbContext.Entry(nextUser).Collection(x => x.Achivements).Load();
                    foreach (UserAchievement ua in nextUser.Achivements)
                    {
                        FeedItem acFI = new FeedItem(baseFI);

                        if (ua.AchievedTime > DateTime.Now.AddDays(-7))
                        {
                            // Add this to feed
                            acFI.Occured = (DateTime)ua.AchievedTime;
                            acFI.ItemType = FeedItemType.Achievement;
                            acFI.Text = ua.GlobalAchievement.Name;

                            model.FeedItems.Add(acFI);
                        }

                    }

                    _appDbContext.Entry(nextUser).Collection(x => x.Tasks).Load();
                    foreach (Models.Task task in nextUser.Tasks)
                    {
                        FeedItem taskFI = new FeedItem(baseFI);

                        if (task.CreatedDate > DateTime.Now.AddDays(-7) && task.IsPrivate == false)
                        {
                            // Add this to feed
                            taskFI.Occured = (DateTime)task.CreatedDate;
                            taskFI.ItemType = FeedItemType.Task;
                            taskFI.Text = task.Summary;
                            taskFI.Taskid = task.ID;
                            model.FeedItems.Add(taskFI);
                        }

                    }
                }
            }

            // Sort feed items by descending date. Quick, dirty, and effective (makes an extra copy in memory)
            // model = model.FeedItems.OrderBy(x => x.Occured).ToList();
            model.FeedItems = model.FeedItems.OrderByDescending(x => x.Occured).ToList();
            return View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Feed(FeedViewModel model)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            // Add post for this user
            Post newPost = new Post
            {
                Text = model.MyPostText,
                PostedTime = DateTime.Now,
                ApplicationUserID = user.Id
            };


            // Posts must be explicitly loaded
            _appDbContext.Entry(user).Collection(x => x.Posts).Load();
            user.Posts.Add(newPost);

            var setResult = await _userManager.UpdateAsync(user);

            if (!setResult.Succeeded)
            {
                throw new ApplicationException($"Unexpected error occurred setting achievements for user with ID '{user.Id}'.");
            }

            StatusMessage = "Your post has been submitted.";

            return RedirectToAction(nameof(Feed));
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Journal()
        {

            ViewData["Message"] = "Your journal.";

            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            //Tasks loaded explicitly 
            _appDbContext.Entry(user).Collection(x => x.Tasks).Load();

            //model used in actual page
            var model = new TaskViewModel
            {
                TaskWrapperList = new List<TaskWrapper>(),
                CurrentUser = user,
                StatusMessage = StatusMessage
            };

            //ambiguity error - hence full path used 
            foreach (TaskItSite.Models.Task a in _appDbContext.Tasks)
            {
                TaskWrapper newW = new TaskWrapper
                {
                    Task = a
                };
         
                //Find matching task - this might be unecessary
                TaskItSite.Models.Task targetTask = model.CurrentUser.Tasks.Where(x => x.ID == a.ID).SingleOrDefault();

                if (targetTask != null)
                   newW.isTask = true;
                else
                newW.isTask = false;
                model.TaskWrapperList.Add(newW);
            }

            return View(model);
        }

      
        public async Task<ActionResult> SubDelete(string subscribee)
        {
            var user = await _userManager.GetUserAsync(User);

            Subscription targetSubscription = _appDbContext.Subscriptions.Where(x => x.SubscribingToUserID == subscribee && x.SubscribingUserID == user.Id).SingleOrDefault();
            
                _appDbContext.Subscriptions.Remove(targetSubscription);
                await _appDbContext.SaveChangesAsync();
            

            return RedirectToAction("Feed");
        }

        public async Task<ActionResult> PostDelete(int? id)
        {
            var user = await _userManager.GetUserAsync(User);

            Post targetPost = _appDbContext.Posts.Where(x => x.PostID == id).SingleOrDefault();

            _appDbContext.Posts.Remove(targetPost);
            await _appDbContext.SaveChangesAsync();


            return RedirectToAction("Feed");
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

            _appDbContext.Entry(user).Collection(x => x.Subs).Load();

            var model = new SubscriptionsViewModel
            {
                SubscriptionWrapperList = new List<SubscriptionWrapper>(),
                CurrentUser = user,
                StatusMessage = StatusMessage
            };

            // Fill in the subscription wrapper list with all users
            foreach (ApplicationUser someUser in _appDbContext.Users)
            {
                bool isSubcribed = false;

                foreach (Subscription cuSub in model.CurrentUser.Subs)
                {
                    if (cuSub.SubscribingToUserID == someUser.Id)
                    {
                        // Current user is subscribed to this user
                        isSubcribed = true;
                        break;
                    }
                }
                if (user.Id != someUser.Id)
                {
                    model.SubscriptionWrapperList.Add(
                        new SubscriptionWrapper
                        {
                            SubscribedUser = someUser,
                            SubscribedUserID = someUser.Id,
                            IsSubscribed = isSubcribed
                        }
                        );
                }
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

            _appDbContext.Entry(user).Collection(x => x.Subs).Load();
            _appDbContext.Entry(user).Collection(x => x.Achivements).Load();
            List<GlobalAchievement> globalAchievementList = _appDbContext.GlobalAchievements.ToList();

            for (int i = 0; i < model.SubscriptionWrapperList.Count; i++)
            {
                SubscriptionWrapper aw = model.SubscriptionWrapperList[i];
                Subscription targetSubscription = user.Subs.Where(x => x.SubscribingToUserID == aw.SubscribedUserID).SingleOrDefault();

                // Only update if there's a change
                if (targetSubscription == null && aw.IsSubscribed == true)
                {
                    Subscription toSub = new Subscription
                    {
                        SubscribingUserID = user.Id,
                        SubscribingToUserID = aw.SubscribedUserID

                    };

                    user.Subs.Add(toSub);
                }
                else if (targetSubscription != null && aw.IsSubscribed == false)
                    user.Subs.Remove(user.Subs.Where(x => x.SubscribingToUserID == aw.SubscribedUserID).SingleOrDefault()); 

            }
            if (user.Subs.Count() >= 2)
                user.AddUserAchievement(globalAchievementList, "Subscribed to 1 person!");
            if (user.Subs.Count() >= 5)
                user.AddUserAchievement(globalAchievementList, "Subscribed to 5 people!");
            if (user.Subs.Count() >= 10)
                user.AddUserAchievement(globalAchievementList, "Subscribed to 10 people!");
            if (user.Subs.Count() >= 20)
                user.AddUserAchievement(globalAchievementList, "Subscribed to 20 people!");


            var setResult = await _userManager.UpdateAsync(user);

            if (!setResult.Succeeded)
            {
                throw new ApplicationException($"Unexpected error occurred setting subscriptions for user with ID '{user.Id}'.");
            }

            StatusMessage = "Your subscriptions have been updated";
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
                try
                {
                    UserAchievement targetAchievement = model.CurrentUser.Achivements.First(x => x.GlobalAchievementID == a.GlobalAchievementID);
                    // Where(x => x.GlobalAchievementID == a.GlobalAchievementID).SingleOrDefault();
                    if (targetAchievement != null)
                        newW.IsAchieved = true;
                    else
                        newW.IsAchieved = false;
                }
                catch { }
                model.AchievementWrapperList.Add(newW);
            }


            model.AchievementWrapperList = model.AchievementWrapperList.OrderByDescending(x => x.IsAchieved).ToList();

            return View(model);
        }
        /*
        [HttpPost]
        public async Task<ActionResult> UpdateCustomer(bool check, string subId)
        {
            var currentUser = await GetCurrentUserAsync();
            List<GlobalAchievement> globalAchievementList = _appDbContext.GlobalAchievements.ToList();
            var subie = new Subscription();
            try
            {
               
                if (check)
                {
                    subie.SubscribingToUserID 
                    currentUser.TasksCompletedCount += 1;
                    _context.Users.Update(currentUser);
                }
                _context.Update(task);

                if (currentUser.TasksCompletedCount == 1)
                {
                    currentUser.AddUserAchievement(globalAchievementList, "Completed 1 task!");
                }

                if (currentUser.TasksCompletedCount == 5)
                {
                    currentUser.AddUserAchievement(globalAchievementList, "Completed 5 tasks!");
                }

                if (currentUser.TasksCompletedCount == 10)
                {
                    currentUser.AddUserAchievement(globalAchievementList, "Completed 10 tasks!");
                }
                if (currentUser.TasksCompletedCount == 20)
                {
                    currentUser.AddUserAchievement(globalAchievementList, "Completed 20 tasks!");
                }
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskExists(task.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return View();
        }

        private bool TaskExists(int id)
        {
            return _context.Tasks.Any(e => e.ID == id);
        }
    }
}
*/

        public async Task<ActionResult> ClonedTask(int id)
        {
            List<GlobalAchievement> globalAchievementList = _appDbContext.GlobalAchievements.ToList();
            var currentUser = await GetCurrentUserAsync();
            

            var task = _appDbContext.Tasks.First(x => x.ID == id);
            var taskUser = _appDbContext.Users.Where(x => x.Id == task.ApplicationUserId).SingleOrDefault();
            var tassk = new Models.Task
            {
                ApplicationUserId = currentUser.Id,
                Summary = "(Cloned from " + taskUser.FullName + ") " + task.Summary,
                Description = task.Description,
                CreatedDate = DateTime.Now,
                StartDate = task.StartDate,
                DueDate = task.DueDate,
                IsActive = task.IsActive
            };
            _appDbContext.Tasks.Add(tassk);

            _appDbContext.Entry(currentUser).Collection(x => x.Achivements).Load();
            currentUser.AddUserAchievement(globalAchievementList, "Cloned 1 task!");
            
            await _appDbContext.SaveChangesAsync();
            
            return RedirectToAction(nameof(Feed));
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
