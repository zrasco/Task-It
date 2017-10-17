using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace TaskItSite.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public string Status { get; set; }
        public AccessLevel AccessLevel { get; set; }

        public ICollection<ApplicationUser> IAmFollowing { get; set; }
        public ICollection<Task> Tasks { get; set; }
        public ICollection<Achievement> Achivements { get; set; }
        public ICollection<Reminder> Reminders { get; set; }
        public Setting Setting { get; set; }
    }
}
