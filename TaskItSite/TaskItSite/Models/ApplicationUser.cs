using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskItSite.Models
{
    public enum HomeScreen { Overview, Feed, Journal, Subscriptions, Achievements }

    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            HomeScreen = HomeScreen.Overview;
            TextOnMessage = false;
            TextOnFeedUpdate = false;
            EmailOnMessage = false;
            EmailOnFeedUpdate = false;
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
                case HomeScreen.Overview:
                default:
                    return "/";
            }
        }

        public string Status { get; set; }
        public string FullName { get; set; }
        public string ProfileImageURL { get; set; }

        public AccessLevel AccessLevel { get; set; }

        public ICollection<ApplicationUser> IAmFollowing { get; set; }
        public ICollection<Task> Tasks { get; set; }
        public ICollection<Achievement> Achivements { get; set; }
        public ICollection<Reminder> Reminders { get; set; }

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

        [NotMapped]
        private int x;
    }
}
