using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskItSite.Models;

namespace TaskItSite.Data
{
    public class TaskItContext : DbContext
    {
        public TaskItContext(DbContextOptions<TaskItContext> options) : base(options)
        {

        }
        
        public DbSet<Achievement> GlobalAchievements { get; set; }
        public DbSet<AchievementCategory> AchievementCategories { get; set; }
        public DbSet<Reminder> Reminders { get; set; }
        public DbSet<Models.Task> Tasks { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Achievement>().ToTable("GlobalAchievements");
            modelBuilder.Entity<AchievementCategory>().ToTable("AchievementCategories");
            modelBuilder.Entity<Reminder>().ToTable("Reminders");
            modelBuilder.Entity<Models.Task>().ToTable("Tasks");
            modelBuilder.Entity<User>().ToTable("Users");
        }
    }
}
