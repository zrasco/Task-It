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
                new GlobalAchievement {AchievementCategoryID = 1, Name = "Created 1 task!", Description = "Earned after creating 1 task." , EmojiString = "em em-ballot_box_with_check" },
                new GlobalAchievement {AchievementCategoryID = 2, Name = "Subscribed to 1 person!", Description = "Earned after subscribing to 1 person." , EmojiString = "em-man-getting-massage" },
                new GlobalAchievement {AchievementCategoryID = 3, Name = "Cloned 1 task!", Description = "Earned after cloning 1 task." , EmojiString = "em-arrow_double_down"},
                new GlobalAchievement {AchievementCategoryID = 4, Name = "Created a private task!", Description = "Earned after creating 1 private task.", EmojiString = "em-european_castle" },
                new GlobalAchievement {AchievementCategoryID = 5, Name = "Subscribed to 5 people!", Description = "Earned after subscribing to 5 people." , EmojiString = "em-man-boy"},
                new GlobalAchievement {AchievementCategoryID = 6, Name = "Added 2 factor authentication!", Description = "Earned after setting up two factor authentication." , EmojiString = "em-hammer_and_wrench"},
                new GlobalAchievement {AchievementCategoryID = 1, Name = "Created 5 tasks!", Description = "Earned after creating 5 tasks." , EmojiString = "em-hand" },
                new GlobalAchievement {AchievementCategoryID = 1, Name = "Subscribed to 10 people!", Description = "Earned after subscribing to 10 people.", EmojiString = "em-man-girl-boy" },
                new GlobalAchievement {AchievementCategoryID = 1, Name = "Subscribed to 20 people!", Description = "Earned after subscribing to 20 people.", EmojiString = "em-woman-woman-girl-boy" },
                new GlobalAchievement {AchievementCategoryID = 2, Name = "Created 10 tasks!", Description = "Earned after creating 10 tasks." , EmojiString = "em-open_hands" },
                new GlobalAchievement {AchievementCategoryID = 2, Name = "Created 20 tasks!", Description = "Earned after creating 20 tasks." , EmojiString = "em-woman_climbing"},
                new GlobalAchievement {AchievementCategoryID = 2, Name = "Completed 1 task!", Description = "Earned after completing 1 task." ,EmojiString = "em-ok_hand"},
                new GlobalAchievement {AchievementCategoryID = 2, Name = "Completed 5 tasks!", Description = "Earned after completing 5 tasks." ,EmojiString = "em-juggling" },
                new GlobalAchievement {AchievementCategoryID = 2, Name = "Completed 10 tasks!", Description = "Earned after completing 10 tasks." ,EmojiString ="em-grinning_face_with_star_eyes" },
                new GlobalAchievement {AchievementCategoryID = 2, Name = "Completed 20 tasks!", Description = "Earned after completing 20 tasks." , EmojiString = "em-grinning_face_with_star_eyes"}
                
            };

            foreach (GlobalAchievement achievement in globalAchievementList)
            {
                context.GlobalAchievements.Add(achievement);
            }
            context.SaveChanges();

            // Add users here
            // All dummy users have a password of 'test'
            var userStore = new UserStore<ApplicationUser>(context);

            // Don't change the order! Or you'll have milton bradley pumping iron ;)
            var dummyUserList = new ApplicationUser[]
            {
                new ApplicationUser { Email = "milton.bradley@monopoly.com", FullName = "Milton Bradley", ProfileImageURL = "http://www.iconninja.com/files/968/519/663/monopoly-icon.png" },
                new ApplicationUser { Email = "arnie@gmail.com", FullName = "Arnold Schwarzenegger", ProfileImageURL = "http://news.greatblogabout.org/wp-content/uploads/2007/03/a1-small1.jpg" },
                new ApplicationUser { Email = "taylor-swift@gmail.com", FullName = "Taylor Swift", ProfileImageURL = "https://www.billboard.com/files/styles/480x270/public/media/taylor-swift-1989-tour-red-lipstick-2015-billboard-650.jpg" },
                new ApplicationUser { Email = "jennan@gmail.com", FullName = "Jennifer Aniston", ProfileImageURL = "https://upload.wikimedia.org/wikipedia/commons/thumb/a/ac/JenniferAniston08TIFF.jpg/170px-JenniferAniston08TIFF.jpg" },
                new ApplicationUser { Email = "clooneyg@gmail.com", FullName = "George Clooney", ProfileImageURL = "https://upload.wikimedia.org/wikipedia/commons/8/8d/George_Clooney_2016.jpg" },
                new ApplicationUser { Email = "willsmith@gmail.com", FullName = "Will Smith", ProfileImageURL = "https://www.biography.com/.image/t_share/MTE4MDAzNDEwNzQzMTY2NDc4/will-smith-9542165-1-402.jpg" },
                new ApplicationUser { Email = "theking@gmail.com", FullName = "Elvis Presley", ProfileImageURL = "https://upload.wikimedia.org/wikipedia/commons/thumb/9/99/Elvis_Presley_promoting_Jailhouse_Rock.jpg/1200px-Elvis_Presley_promoting_Jailhouse_Rock.jpg" },
                new ApplicationUser { Email = "robdown@ironman.com", FullName = "Robert Downey Jr", ProfileImageURL = "https://upload.wikimedia.org/wikipedia/commons/thumb/9/94/Robert_Downey_Jr_2014_Comic_Con_%28cropped%29.jpg/1200px-Robert_Downey_Jr_2014_Comic_Con_%28cropped%29.jpg" },
                new ApplicationUser { Email = "ryan_gos@gmail.com", FullName = "Ryan Gosling", ProfileImageURL = "https://upload.wikimedia.org/wikipedia/commons/thumb/e/e6/Ryan_Gosling_by_Gage_Skidmore.jpg/220px-Ryan_Gosling_by_Gage_Skidmore.jpg" },
                new ApplicationUser { Email = "Oprah@gmail.com", FullName = "Oprah Winfery", ProfileImageURL = "http://static.tvgcdn.net/mediabin/showcards/celebs/m-o/thumbs/oprah-winfrey-168611_828x1104.png" },
                new ApplicationUser { Email = "lvfd@gmail.com", FullName = "Las Vegas Fire Department", ProfileImageURL = "https://upload.wikimedia.org/wikipedia/commons/e/e3/Las_Vegas_Fire_Department.jpg" },
                new ApplicationUser { Email = "thekillers@music.com", FullName = "The Killers", ProfileImageURL = "https://media.pitchfork.com/photos/59299367c0084474cd0bead4/1:1/w_300/90179474.jpg" },
                new ApplicationUser { Email = "CrissAngel@magic.com", FullName = "Criss Angel", ProfileImageURL = "https://lasvegas.showtickets.com/cdn/site/v1_16_3100_cds_branding_digital_mfl.jpg" },
                new ApplicationUser { Email = "KrisBryant@cubs.com", FullName = "Kris Bryant", ProfileImageURL = "http://mlb.mlb.com/mlb/images/players/head_shot/592178.jpg" },
                new ApplicationUser { Email = "SnoopDogg@gmail.com", FullName = "Snoop Dogg", ProfileImageURL = "https://www.biography.com/.image/t_share/MTQ3NjM5ODIyNjU0MTIxMDM0/snoop_dogg_photo_by_estevan_oriol_archive_photos_getty_455616412.jpg" }
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
            /*
                new ApplicationUser { Email = "milton.bradley@monopoly.com", FullName = "Milton Bradley", ProfileImageURL = "http://www.iconninja.com/files/968/519/663/monopoly-icon.png" },
                new ApplicationUser { Email = "arnie@gmail.com", FullName = "Arnold Schwarzenegger", ProfileImageURL = "http://news.greatblogabout.org/wp-content/uploads/2007/03/a1-small1.jpg" },
                new ApplicationUser { Email = "taylor-swift@gmail.com", FullName = "Taylor Swift", ProfileImageURL = "https://www.billboard.com/files/styles/480x270/public/media/taylor-swift-1989-tour-red-lipstick-2015-billboard-650.jpg" },
                new ApplicationUser { Email = "jennan@gmail.com", FullName = "Jennifer Aniston", ProfileImageURL = "https://upload.wikimedia.org/wikipedia/commons/thumb/a/ac/JenniferAniston08TIFF.jpg/170px-JenniferAniston08TIFF.jpg" },
                new ApplicationUser { Email = "clooneyg@gmail.com", FullName = "George Clooney", ProfileImageURL = "https://upload.wikimedia.org/wikipedia/commons/8/8d/George_Clooney_2016.jpg" },
                new ApplicationUser { Email = "willsmith@gmail.com", FullName = "Will Smith", ProfileImageURL = "https://www.biography.com/.image/t_share/MTE4MDAzNDEwNzQzMTY2NDc4/will-smith-9542165-1-402.jpg" },
                new ApplicationUser { Email = "theking@gmail.com", FullName = "Elvis Presley", ProfileImageURL = "https://upload.wikimedia.org/wikipedia/commons/thumb/9/99/Elvis_Presley_promoting_Jailhouse_Rock.jpg/1200px-Elvis_Presley_promoting_Jailhouse_Rock.jpg" },
                new ApplicationUser { Email = "robdown@ironman.com", FullName = "Robert Downey Jr", ProfileImageURL = "https://upload.wikimedia.org/wikipedia/commons/thumb/9/94/Robert_Downey_Jr_2014_Comic_Con_%28cropped%29.jpg/1200px-Robert_Downey_Jr_2014_Comic_Con_%28cropped%29.jpg" },
                new ApplicationUser { Email = "ryan_gos@gmail.com", FullName = "Ryan Gosling", ProfileImageURL = "https://upload.wikimedia.org/wikipedia/commons/thumb/e/e6/Ryan_Gosling_by_Gage_Skidmore.jpg/220px-Ryan_Gosling_by_Gage_Skidmore.jpg" },
                new ApplicationUser { Email = "Oprah@gmail.com", FullName = "Oprah Winfery", ProfileImageURL = "http://static.tvgcdn.net/mediabin/showcards/celebs/m-o/thumbs/oprah-winfrey-168611_828x1104.png" },
                new ApplicationUser { Email = "lvfd@gmail.com", FullName = "Las Vegas Fire Department", ProfileImageURL = "https://upload.wikimedia.org/wikipedia/commons/e/e3/Las_Vegas_Fire_Department.jpg" },
                new ApplicationUser { Email = "thekillers@music.com", FullName = "The Killers", ProfileImageURL = "https://media.pitchfork.com/photos/59299367c0084474cd0bead4/1:1/w_300/90179474.jpg" },
                new ApplicationUser { Email = "CrissAngel@magic.com", FullName = "Criss Angel", ProfileImageURL = "https://lasvegas.showtickets.com/cdn/site/v1_16_3100_cds_branding_digital_mfl.jpg" },
                new ApplicationUser { Email = "KrisBryant@cubs.com", FullName = "Kris Bryant", ProfileImageURL = "http://mlb.mlb.com/mlb/images/players/head_shot/592178.jpg" },
                new ApplicationUser { Email = "SnoopDogg@gmail.com", FullName = "Snoop Dogg", ProfileImageURL = "https://www.biography.com/.image/t_share/MTQ3NjM5ODIyNjU0MTIxMDM0/snoop_dogg_photo_by_estevan_oriol_archive_photos_getty_455616412.jpg" }
                */
            var Posts = new Post[]
            {
                new Post { ApplicationUserID = dummyUserList[0].Id, PostedTime = DateTime.Now.AddDays(-3), Text = "I made a new monopoly board! Check it out at https://www.monopoly.com!" },
                new Post { ApplicationUserID = dummyUserList[0].Id, PostedTime = DateTime.Now.AddDays(-2), Text = "New scabble board out!" },
                new Post { ApplicationUserID = dummyUserList[0].Id, PostedTime = DateTime.Now.AddDays(-1), Text = "Made a new version of battleship!" },

                new Post { ApplicationUserID = dummyUserList[1].Id, PostedTime = DateTime.Now.AddDays(-3), Text = "I'm ready to pump some iron!" },
                new Post { ApplicationUserID = dummyUserList[1].Id, PostedTime = DateTime.Now.AddDays(-2), Text = "I lifted 500lbs! Can you, girly man?" },
                new Post { ApplicationUserID = dummyUserList[1].Id, PostedTime = DateTime.Now.AddDays(-1), Text = "Tore a muscle. Ouch!" },

                new Post { ApplicationUserID = dummyUserList[2].Id, PostedTime = DateTime.Now.AddDays(-3), Text = "Broke up with boyfriend" },
                new Post { ApplicationUserID = dummyUserList[2].Id, PostedTime = DateTime.Now.AddDays(-2), Text = "Thinking of material for my new album" },
                new Post { ApplicationUserID = dummyUserList[2].Id, PostedTime = DateTime.Now.AddDays(-1), Text = "New album out!" },

                new Post { ApplicationUserID = dummyUserList[3].Id, PostedTime = DateTime.Now.AddDays(-3), Text = "Friends reunion today" },
                new Post { ApplicationUserID = dummyUserList[3].Id, PostedTime = DateTime.Now.AddDays(-2), Text = "I saw Brad Pitt today and omg sooooooo awkward!" },
                new Post { ApplicationUserID = dummyUserList[3].Id, PostedTime = DateTime.Now.AddDays(-1), Text = "Office space reunion today" },

                new Post { ApplicationUserID = dummyUserList[4].Id, PostedTime = DateTime.Now.AddDays(-3), Text = "Thinking of Batman Forever....ugh!" },
                new Post { ApplicationUserID = dummyUserList[4].Id, PostedTime = DateTime.Now.AddDays(-2), Text = "My tequila company is doing great!" },
                new Post { ApplicationUserID = dummyUserList[4].Id, PostedTime = DateTime.Now.AddDays(-5), Text = "My favorite winter Margarita . 1oz reposado, 1oz mezcal, 1oz lime juice, 0.75oz simple syrup, 1 egg white" },

                new Post { ApplicationUserID = dummyUserList[5].Id, PostedTime = DateTime.Now.AddDays(-3), Text = "I love being the Fresh Prince of Bel-Air!" },
                new Post { ApplicationUserID = dummyUserList[5].Id, PostedTime = DateTime.Now.AddDays(-2), Text = "I loved that movie where I was in the end of the world. What was it called?" },
                new Post { ApplicationUserID = dummyUserList[5].Id, PostedTime = DateTime.Now.AddDays(-1), Text = "Thinking of making a new hip-hop album after all these years!" },

                new Post { ApplicationUserID = dummyUserList[6].Id, PostedTime = DateTime.Now.AddDays(-3), Text = "I love being the King!" },
                new Post { ApplicationUserID = dummyUserList[6].Id, PostedTime = DateTime.Now.AddDays(-2), Text = "Got a street named after me in Las Vegas. Thank you very much!" },
                new Post { ApplicationUserID = dummyUserList[6].Id, PostedTime = DateTime.Now.AddDays(-1), Text = "Elvis has left the building" },

                new Post { ApplicationUserID = dummyUserList[7].Id, PostedTime = DateTime.Now.AddDays(-3), Text = "Iron man 5 out!" },
                new Post { ApplicationUserID = dummyUserList[7].Id, PostedTime = DateTime.Now.AddDays(-2), Text = "I can't keep these people away from me!" },
                new Post { ApplicationUserID = dummyUserList[7].Id, PostedTime = DateTime.Now.AddDays(-1), Text = "Going on vacation" },

                new Post { ApplicationUserID = dummyUserList[8].Id, PostedTime = DateTime.Now.AddDays(-3), Text = "Love being nominated for best actor!" },
                new Post { ApplicationUserID = dummyUserList[8].Id, PostedTime = DateTime.Now.AddDays(-2), Text = "Reminiscing on my days in The Notebook..." },
                new Post { ApplicationUserID = dummyUserList[8].Id, PostedTime = DateTime.Now.AddDays(-1), Text = "In my new movie, First Man!" },

                new Post { ApplicationUserID = dummyUserList[9].Id, PostedTime = DateTime.Now.AddDays(-3), Text = "Look under your chairs! Just kidding, haha" },
                new Post { ApplicationUserID = dummyUserList[9].Id, PostedTime = DateTime.Now.AddDays(-2), Text = "Loving my new mansion!" },
                new Post { ApplicationUserID = dummyUserList[9].Id, PostedTime = DateTime.Now.AddDays(-6), Text = "Thanks again Jaon Barthel for helping me write Oprah: An Autobiography!" },

                new Post { ApplicationUserID = dummyUserList[10].Id, PostedTime = DateTime.Now.AddDays(-3), Text = "Put out a 5-alarm fire over at Mandalay bay today" },
                new Post { ApplicationUserID = dummyUserList[10].Id, PostedTime = DateTime.Now.AddDays(-2), Text = "We'll be at the grocery store buying food today, ladies!" },
                new Post { ApplicationUserID = dummyUserList[10].Id, PostedTime = DateTime.Now.AddDays(-1), Text = "Rescued a kitten today!" },

                new Post { ApplicationUserID = dummyUserList[11].Id, PostedTime = DateTime.Now.AddDays(-3), Text = "Picking new cover art for our album" },
                new Post { ApplicationUserID = dummyUserList[11].Id, PostedTime = DateTime.Now.AddDays(-2), Text = "Just released our new album!" },
                new Post { ApplicationUserID = dummyUserList[11].Id, PostedTime = DateTime.Now.AddDays(-1), Text = "Loved performing at #VegasStrong concert, thanks Las Vegas!" },

                new Post { ApplicationUserID = dummyUserList[12].Id, PostedTime = DateTime.Now.AddDays(-3), Text = "I made my lunch dissapear! Mwahaha" },
                new Post { ApplicationUserID = dummyUserList[12].Id, PostedTime = DateTime.Now.AddDays(-2), Text = "Performing random magic tricks on the street today. Watch for me on the strip around Flamingo!" },
                new Post { ApplicationUserID = dummyUserList[12].Id, PostedTime = DateTime.Now.AddDays(-1), Text = "I made my breakfast dissapear! Yum!" },

                new Post { ApplicationUserID = dummyUserList[13].Id, PostedTime = DateTime.Now.AddDays(-3), Text = "Broke the MLB pitching record today!" },
                new Post { ApplicationUserID = dummyUserList[13].Id, PostedTime = DateTime.Now.AddDays(-2), Text = "No game today, booooooooring" },
                new Post { ApplicationUserID = dummyUserList[13].Id, PostedTime = DateTime.Now.AddDays(-1), Text = "Game got rained out!" },

                new Post { ApplicationUserID = dummyUserList[1].Id, PostedTime = DateTime.Now.AddDays(-3), Text = "New album with Dr. Dre out today, The Chronic 2017!" },
                new Post { ApplicationUserID = dummyUserList[1].Id, PostedTime = DateTime.Now.AddDays(-2), Text = "Man I'm hungry!" },
                new Post { ApplicationUserID = dummyUserList[1].Id, PostedTime = DateTime.Now.AddDays(-1), Text = "I love crusing around the strip!" },

                new Post { ApplicationUserID = dummyUserList[3].Id, PostedTime = DateTime.Now.AddDays(-1), Text = "This meal plan has been working perfectly for me at https://www.pritikin.com/your-health/healthy-living/eating-right/1720-healthy-meal-plan-for-weight-loss.html!" }
            };

            foreach (Post post in Posts)
            {
                context.Posts.Add(post);
            }

            var Tasks = new Models.Task[]
            {
                new Models.Task { ApplicationUserId = dummyUserList[2].Id, CreatedDate = DateTime.Now.AddDays(-6), DueDate = DateTime.Now.AddDays(2),   Summary = "Come to my event at CBC 237" },
                new Models.Task { ApplicationUserId = dummyUserList[5].Id, CreatedDate = DateTime.Now.AddDays(-2), DueDate = DateTime.Now.AddDays(1),   Summary = "Workout plan to prepare for next film: https://www.muscleandfitness.com/workouts/workout-routines/complete-mf-beginners-training-guide-0" },
                new Models.Task { ApplicationUserId = dummyUserList[7].Id, CreatedDate = DateTime.Now.AddDays(-4), DueDate = DateTime.Now.AddDays(1),   Summary = "Filming just finished for Avengers: Infinity War, watch the trailer here https://www.youtube.com/watch?v=6ZfuNTqbHE8" },
                new Models.Task { ApplicationUserId = dummyUserList[8].Id, CreatedDate = DateTime.Now.AddDays(-6), DueDate = DateTime.Now.AddDays(3),   Summary = "Read The Hammer: an American Hero By Harold D. Edmunds by Due Date" },
                new Models.Task { ApplicationUserId = dummyUserList[11].Id, CreatedDate = DateTime.Now.AddDays(-3), DueDate = DateTime.Now.AddDays(1),   Summary = "Crunch time: Finish New Single" },
                new Models.Task { ApplicationUserId = dummyUserList[10].Id, CreatedDate = DateTime.Now.AddDays(-2), DueDate = DateTime.Now.AddDays(10),   Summary = "City wide outlet checks this month, Contact your local electrician or call (702) 455-7311 for more details" },
                new Models.Task { ApplicationUserId = dummyUserList[12].Id, CreatedDate = DateTime.Now.AddDays(-2), DueDate = DateTime.Now.AddDays(1),   Summary = "Weekend special to see Criss Angel Mindfreak at the Luxor Hotel & Casino" },
                new Models.Task { ApplicationUserId = dummyUserList[14].Id, CreatedDate = DateTime.Now.AddDays(-3), DueDate = DateTime.Now.AddDays(1),   Summary = "We've got special deals at Jardin Cannabis Dispensary, come check it out" }
            };

            foreach (Models.Task task in Tasks)
            {
                task.StartDate = task.CreatedDate;
                context.Tasks.Add(task);
            }


            context.SaveChanges();

        }
    }
}
