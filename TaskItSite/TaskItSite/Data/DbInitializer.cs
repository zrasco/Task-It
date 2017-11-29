using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskItSite.Models;

namespace TaskItSite.Data
{
    public class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.AchievementCategories.Any())
            {
                return;
            }

            // Code to initalize database below
            /*
             *
             * 
             * 
             * 
             * 
            // Look for any students.
            if (context.Students.Any())
            {
                return;   // DB has been seeded
            }

            var students = new Student[]
            {
            new Student{FirstMidName="Carson",LastName="Alexander",EnrollmentDate=DateTime.Parse("2005-09-01")},
            new Student{FirstMidName="Meredith",LastName="Alonso",EnrollmentDate=DateTime.Parse("2002-09-01")},
            new Student{FirstMidName="Arturo",LastName="Anand",EnrollmentDate=DateTime.Parse("2003-09-01")},
            new Student{FirstMidName="Gytis",LastName="Barzdukas",EnrollmentDate=DateTime.Parse("2002-09-01")},
            new Student{FirstMidName="Yan",LastName="Li",EnrollmentDate=DateTime.Parse("2002-09-01")},
            new Student{FirstMidName="Peggy",LastName="Justice",EnrollmentDate=DateTime.Parse("2001-09-01")},
            new Student{FirstMidName="Laura",LastName="Norman",EnrollmentDate=DateTime.Parse("2003-09-01")},
            new Student{FirstMidName="Nino",LastName="Olivetto",EnrollmentDate=DateTime.Parse("2005-09-01")}
            };
            foreach (Student s in students)
            {
                context.Students.Add(s);
            }
            context.SaveChanges();

            var courses = new Course[]
            {
            new Course{CourseID=1050,Title="Chemistry",Credits=3},
            new Course{CourseID=4022,Title="Microeconomics",Credits=3},
            new Course{CourseID=4041,Title="Macroeconomics",Credits=3},
            new Course{CourseID=1045,Title="Calculus",Credits=4},
            new Course{CourseID=3141,Title="Trigonometry",Credits=4},
            new Course{CourseID=2021,Title="Composition",Credits=3},
            new Course{CourseID=2042,Title="Literature",Credits=4}
            };
            foreach (Course c in courses)
            {
                context.Courses.Add(c);
            }
            context.SaveChanges();

            var enrollments = new Enrollment[]
            {
            new Enrollment{StudentID=1,CourseID=1050,Grade=Grade.A},
            new Enrollment{StudentID=1,CourseID=4022,Grade=Grade.C},
            new Enrollment{StudentID=1,CourseID=4041,Grade=Grade.B},
            new Enrollment{StudentID=2,CourseID=1045,Grade=Grade.B},
            new Enrollment{StudentID=2,CourseID=3141,Grade=Grade.F},
            new Enrollment{StudentID=2,CourseID=2021,Grade=Grade.F},
            new Enrollment{StudentID=3,CourseID=1050},
            new Enrollment{StudentID=4,CourseID=1050},
            new Enrollment{StudentID=4,CourseID=4022,Grade=Grade.F},
            new Enrollment{StudentID=5,CourseID=4041,Grade=Grade.C},
            new Enrollment{StudentID=6,CourseID=1045},
            new Enrollment{StudentID=7,CourseID=3141,Grade=Grade.A},
            };
            foreach (Enrollment e in enrollments)
            {
                context.Enrollments.Add(e);
            }
            context.SaveChanges();
            */

            // Create categories here
            var achievementCategories = new AchievementCategory[]
            {
                new AchievementCategory {AchievementCategoryID = 1, Name = "Subscriptions", Description = "Earned when reaching a certain number of subscriptions." },
                new AchievementCategory {AchievementCategoryID = 2, Name = "Tasks", Description = "Earned when creating or completing a certain number of tasks." },
                new AchievementCategory {AchievementCategoryID = 3, Name = "Workout", Description = "Earned when completing a workout task." },
                new AchievementCategory {AchievementCategoryID = 4, Name = "Chore", Description = "Earned when completing a chore task." },
                new AchievementCategory {AchievementCategoryID = 5, Name = "Fun", Description = "Earned when completing a fun task." },
                new AchievementCategory {AchievementCategoryID = 6, Name = "School", Description = "Earned when completing a school task." }
                // Add as many as you want here...
            };

            foreach (AchievementCategory achievementCategory in achievementCategories)
            {
                context.AchievementCategories.Add(achievementCategory);
            }

            context.SaveChanges();

            // Create achievements here
            var globalAchievementList = new GlobalAchievement[]
            {
                new GlobalAchievement {AchievementCategoryID = 1, Name = "Subscribed to 1 person!", Description = "Earned after subscribing to 1 person." },
                new GlobalAchievement {AchievementCategoryID = 1, Name = "Subscribed to 5 people!", Description = "Earned after subscribing to 5 people." },
                new GlobalAchievement {AchievementCategoryID = 1, Name = "Subscribed to 10 people!", Description = "Earned after subscribing to 10 people." },
                new GlobalAchievement {AchievementCategoryID = 1, Name = "Subscribed to 20 people!", Description = "Earned after subscribing to 20 people." },
                new GlobalAchievement {AchievementCategoryID = 2, Name = "Created 1 task!", Description = "Earned after creating 1 task." },
                new GlobalAchievement {AchievementCategoryID = 2, Name = "Created 5 tasks!", Description = "Earned after creating 5 tasks." },
                new GlobalAchievement {AchievementCategoryID = 2, Name = "Created 10 tasks!", Description = "Earned after creating 10 tasks." },
                new GlobalAchievement {AchievementCategoryID = 2, Name = "Created 20 tasks!", Description = "Earned after creating 20 tasks." },
                new GlobalAchievement {AchievementCategoryID = 2, Name = "Completed 1 task!", Description = "Earned after completing 1 task." },
                new GlobalAchievement {AchievementCategoryID = 2, Name = "Completed 5 tasks!", Description = "Earned after completing 5 tasks." },
                new GlobalAchievement {AchievementCategoryID = 2, Name = "Completed 10 tasks!", Description = "Earned after completing 10 tasks." },
                new GlobalAchievement {AchievementCategoryID = 2, Name = "Completed 20 tasks!", Description = "Earned after completing 20 tasks." },
                new GlobalAchievement {AchievementCategoryID = 3, Name = "Workout Newbie", Description = "Completed 1 workout task." },
                new GlobalAchievement {AchievementCategoryID = 3, Name = "Workout Novice", Description = "Completed 2 workout tasks." },
                new GlobalAchievement {AchievementCategoryID = 3, Name = "Workout Rookie", Description = "Completed 5 workout tasks." },
                new GlobalAchievement {AchievementCategoryID = 3, Name = "Workout Begninner", Description = "Completed 10 workout tasks." },
                new GlobalAchievement {AchievementCategoryID = 3, Name = "Workout Advanced", Description = "Completed 15 workout tasks." },
                new GlobalAchievement {AchievementCategoryID = 3, Name = "Workout Expert", Description = "Completed 20 workout tasks." },
                new GlobalAchievement {AchievementCategoryID = 4, Name = "Press Start for Chores", Description = "Completed 1 chore task." },
                new GlobalAchievement {AchievementCategoryID = 4, Name = "The Choremaster", Description = "Completed 10 chore tasks." },
                new GlobalAchievement {AchievementCategoryID = 4, Name = "You're on a Roll!", Description = "Completed 25 chore tasks." },
                new GlobalAchievement {AchievementCategoryID = 4, Name = "Guru of Chores", Description = "Completed 20 chore tasks." },
                new GlobalAchievement {AchievementCategoryID = 5, Name = "Lukewarm Fun", Description = "Completed 1 fun task." },
                new GlobalAchievement {AchievementCategoryID = 5, Name = "Getting There", Description = "Completed 5 fun tasks." },
                new GlobalAchievement {AchievementCategoryID = 5, Name = "Into the Wild Blue Fun", Description = "Completed 10 fun tasks." },
                new GlobalAchievement {AchievementCategoryID = 5, Name = "The Final Goal: Fun", Description = "Completed 20 fun tasks." },
                new GlobalAchievement {AchievementCategoryID = 6, Name = "You Showed Up", Description = "Completed 1 school task." },
                new GlobalAchievement {AchievementCategoryID = 6, Name = "Mediocre Olympiad", Description = "Completed 5 school tasks." },
                new GlobalAchievement {AchievementCategoryID = 6, Name = "Honor Roll", Description = "Completed 10 school tasks." },
                new GlobalAchievement {AchievementCategoryID = 6, Name = "Teacher's Pet", Description = "Completed 20 school tasks." }
            };

            foreach (GlobalAchievement achievement in globalAchievementList)
            {
                context.GlobalAchievements.Add(achievement);
            }
            context.SaveChanges();

            GlobalAchievement test = new GlobalAchievement { AchievementCategoryID = 3, Name = "Not allowed!", Description = "Description of achievement" };
            context.GlobalAchievements.Add(test);
            context.SaveChanges();

            // Add users here
            // All dummy users have a password of 'test'
            var userStore = new UserStore<ApplicationUser>(context);

            // Don't change the order! Or you'll have milton bradley pumping iron ;)
            var dummyUserList = new ApplicationUser[]
            {
                new ApplicationUser { Email = "milton.bradley@monopoly.com", FullName = "Milton Bradley", ProfileImageURL = "http://www.iconninja.com/files/968/519/663/monopoly-icon.png" },
                new ApplicationUser { Email = "arnie@gmail.com", FullName = "Arnold Schwarzenegger", ProfileImageURL = "http://news.greatblogabout.org/wp-content/uploads/2007/03/a1-small1.jpg" }
            };

            foreach (ApplicationUser au in dummyUserList)
            {
                var password = new PasswordHasher<ApplicationUser>();
                var hashed = password.HashPassword(au, "test");
                au.PasswordHash = hashed;
                au.UserName = au.Email;
                au.NormalizedEmail = au.Email.ToUpper();
                au.NormalizedUserName = au.UserName.ToUpper();
                au.EmailConfirmed = true;
                au.PhoneNumberConfirmed = true;
                au.SecurityStamp = Guid.NewGuid().ToString("D");

                userStore.CreateAsync(au).Wait();
            }

            context.SaveChanges();

            // TODO: Seed subscriptions
            // TODO: Seed achievements for specific dummy users
            // TODO: Seed tasks for specific dummy users
            // TODO: Seed posts

            // NOTE: This relies on the order of users above
            var Posts = new Post[]
            {
                new Post { ApplicationUserID = dummyUserList[0].Id, PostedTime = DateTime.Now.AddDays(-2), Text = "I made a new monopoly board! Check it out at https://www.monopoly.com!" },
                new Post { ApplicationUserID = dummyUserList[1].Id, PostedTime = DateTime.Now.AddDays(-3), Text = "I'm ready to pump some iron!" },
                new Post { ApplicationUserID = dummyUserList[1].Id, PostedTime = DateTime.Now.AddDays(-2), Text = "I lifted 500lbs! Can you, girly man?" },
                new Post { ApplicationUserID = dummyUserList[1].Id, PostedTime = DateTime.Now.AddDays(-1), Text = "Tore a muscle. Ouch!" }
            };

            foreach (Post post in Posts)
            {
                context.Posts.Add(post);
            }



            context.SaveChanges();

        }
    }
}
