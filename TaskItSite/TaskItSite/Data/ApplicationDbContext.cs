using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaskItSite.Models;
using Microsoft.AspNetCore.Http;

namespace TaskItSite.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // User accounts already created with ASP.NET so I've extended it (ApplicationUser)
        public DbSet<Achievement> GlobalAchievements { get; set; }
        public DbSet<AchievementCategory> AchievementCategories { get; set; }
        public DbSet<Reminder> Reminders { get; set; }
        public DbSet<Models.Task> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Achievement>().ToTable("GlobalAchievements");
            modelBuilder.Entity<AchievementCategory>().ToTable("AchievementCategories");
            modelBuilder.Entity<Reminder>().ToTable("Reminders");
            modelBuilder.Entity<Models.Task>().ToTable("Tasks");

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<TaskItSite.Models.ApplicationUser> ApplicationUser { get; set; }
    }
}
