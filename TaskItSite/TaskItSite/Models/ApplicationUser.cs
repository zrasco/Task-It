using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskItSite.Models
{
    public enum HomeScreen { Feed, Journal, Subscriptions, Achievements }

    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        
        public ApplicationUser()
        {
            HomeScreen = HomeScreen.Feed;
            TextOnMessage = false;
            TextOnFeedUpdate = false;
            EmailOnMessage = false;
            EmailOnFeedUpdate = false;
            IsPublic = false;

            // Initialize lists

            if (Tasks == null)
                Tasks = new List<Task>();

            if (Achivements == null)
                Achivements = new List<UserAchievement>();

            if (Reminders == null)
                Reminders = new List<Reminder>();

            if (Subs == null)
            {
                Subs = new List<Subscription>();

                // Every user is always subscribed to themselves by default
                Subscription subMe = new Subscription
                {
                    SubscribingTo = this,
                    SubscribingToUserID = Id,
                    SubscribingUser = this,
                    SubscribingUserID = Id
                };

                Subs.Add(subMe);
            }

            if (Posts == null)
                Posts = new List<Post>();
        }

        public string GetHomeScreenURL()
        {
            switch (HomeScreen)
            {
                case HomeScreen.Feed:
                    return "/Home/Feed";
                case HomeScreen.Journal:
                    return "/Home/Journal";
                case HomeScreen.Subscriptions:
                    return "/Home/Subscriptions";
                case HomeScreen.Achievements:
                    return "/Home/Achievements";
                default:
                    return "/Home/Feed";
            }
        }

        public bool AddUserAchievement(List<GlobalAchievement> globalAchievementlList, string achievementName)
        // Returns true if added, false if already there
        {
            // Assumes global achievements are added already
            GlobalAchievement ga = globalAchievementlList.Where(x => x.Name == achievementName).SingleOrDefault();
            UserAchievement ua = this.Achivements.Where(x => x.GlobalAchievementID == ga.GlobalAchievementID).SingleOrDefault();

            if (ua == null)
            {
                UserAchievement toAdd = new UserAchievement
                {
                    GlobalAchievementID = ga.GlobalAchievementID,
                    AchievedTime = DateTime.Now,
                    ApplicationUserID = this.Id
                };

                this.Achivements.Add(toAdd);
                return true;
            }
            else
                return false;
        }

        public string Status { get; set; }
        public string FullName { get; set; }
        public string ProfileImageURL { get; set; }

        public AccessLevel AccessLevel { get; set; }

        public virtual ICollection<Task> Tasks { get; set; }
        public virtual ICollection<UserAchievement> Achivements { get; set; }
        public virtual ICollection<Reminder> Reminders { get; set; }
        public virtual ICollection<Subscription> Subs { get; set; }
        public virtual ICollection<Post> Posts { get; set; }

        // zrasco - Using a single settings container would require building a respository to save the sub-objects.
        //          Since we only have a handful of settings, make them individual fields instead
        //
        //          A repo + sub-object hierarchy could be written later to help this up.

        // Settings
        public HomeScreen HomeScreen { get; set; }
        public bool TextOnMessage { get; set; }
        public bool TextOnFeedUpdate { get; set; }
        public bool EmailOnMessage { get; set; }
        public bool EmailOnFeedUpdate { get; set; }
        public bool IsPublic { get; set; }
        
    }
}
