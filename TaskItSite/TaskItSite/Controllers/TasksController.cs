using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskItSite.Data;
using TaskItSite.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using System.Runtime.InteropServices;

namespace TaskItSite.Controllers
{
    public class TasksController : Controller
    {
        #region Dependency injection
        private readonly ApplicationDbContext _context = null;

        private readonly UserManager<ApplicationUser> _userManager = null;
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        public TasksController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }
        #endregion

        // GET: Tasks
        public async Task<IActionResult> Index()
        {
            var currentUser = await GetCurrentUserAsync();
            var tasklist = await _context.Tasks.ToListAsync();
            var usertask = new List<Models.Task>();

            foreach (var item in tasklist)
            {
                if(item.UserID == currentUser.Id && !item.IsActive)
                {
                    usertask.Add(item);

                }
            }
                return View(usertask);
        }

        
        [HttpGet]
        //POST: Partial view of the task at position id
        public ActionResult GetDetails(  )
        {
            //uses previously made get
            var tasklist = _context.Tasks.ToList();
            return PartialView("~/Views/Tasks/Details.cshtml", tasklist[0]);
        }

        // GET: Tasks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _context.Tasks
                .SingleOrDefaultAsync(m => m.ID == id);
            if (task == null)
            {
                return NotFound();
            }

            return View(task);
        }

        // GET: Tasks/Create
        public IActionResult Create()
        {
            return View();
        }

        public async Task<IActionResult> QuickAdd(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (id != null)
            {
                var task = await _context.Tasks.SingleOrDefaultAsync(m => m.ID == id);

                if (task == null)
                {
                    return NotFound();
                }
                return View(task);
            }

            return View();
        }

        public async Task<IActionResult> Create1()
        {
            var currentUser = await GetCurrentUserAsync();
            var tasklist = await _context.Tasks.ToListAsync();
            var usertask = new List<Models.Task>();

            foreach (var item in tasklist)
            {
                if (item.UserID == currentUser.Id && item.IsActive)
                {
                    usertask.Add(item);

                }
            }
            return View(usertask);
        }

        // POST: Tasks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,CreatedDate,DueDate,Summary,Description,IsPin,UserID,IsPrivate")] Models.Task task)
        {
            var currentUser = await GetCurrentUserAsync();
            List<GlobalAchievement> globalAchievementList = _context.GlobalAchievements.ToList();

            if (ModelState.IsValid)
            {
                task.CreatedDate = DateTime.Now;
                task.UserID = currentUser.Id;
                task.IsActive = false;
                currentUser.TasksCreatedCount += 1;
                _context.Users.Update(currentUser);
                _context.Add(task);
                await _context.SaveChangesAsync();

                if(currentUser.TasksCreatedCount == 1)
                {
                    currentUser.AddUserAchievement(globalAchievementList, "Created 1 task!");
                }

                if(currentUser.TasksCreatedCount == 5)
                {
                    currentUser.AddUserAchievement(globalAchievementList, "Created 5 tasks!");
                }

                if (currentUser.TasksCreatedCount == 10)
                {
                    currentUser.AddUserAchievement(globalAchievementList, "Created 10 tasks!");
                }

                if (currentUser.TasksCreatedCount == 20)
                {
                    currentUser.AddUserAchievement(globalAchievementList, "Created 20 tasks!");
                }

                if (task.IsPrivate)
                {
                    currentUser.AddUserAchievement(globalAchievementList, "Created a private task!");
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(task);
        }

        // GET: Tasks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _context.Tasks.SingleOrDefaultAsync(m => m.ID == id);
            if (task == null)
            {
                return NotFound();
            }
            return View(task);
        }

        // POST: Tasks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,CreatedDate,DueDate,Summary,Description,IsPin,IsPrivate")] Models.Task task)
        {
            if (id != task.ID)
            {
                return NotFound();
            }
            var currentUser = await GetCurrentUserAsync();

            if (ModelState.IsValid)
            {
                try
                {
                    task.UserID = currentUser.Id;
                    task.CreatedDate = DateTime.Now;
                    task.IsActive = false;
                    _context.Update(task);
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
                return RedirectToAction(nameof(Index));
            }
            return View(task);
        }

        // GET: Tasks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _context.Tasks
                .SingleOrDefaultAsync(m => m.ID == id);
            if (task == null)
            {
                return NotFound();
            }

            return View(task);
        }

        // POST: Tasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var task = await _context.Tasks.SingleOrDefaultAsync(m => m.ID == id);
            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<ActionResult> UpdateCustomer(bool check, int customerId)
        {
            var task = await _context.Tasks.SingleOrDefaultAsync(m => m.ID == customerId);
            var currentUser = await GetCurrentUserAsync();
            List<GlobalAchievement> globalAchievementList = _context.GlobalAchievements.ToList();

            try
            {
                task.IsActive = check;
                if (check)
                {
                    currentUser.TasksCompletedCount += 1;
                    _context.Users.Update(currentUser);
                }
                _context.Update(task);

                if(currentUser.TasksCompletedCount == 1)
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
