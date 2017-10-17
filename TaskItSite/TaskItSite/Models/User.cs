using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskItSite.Models
{
    public class User : IdentityUser
    {
        public int id { get; set; }
        public string username { get; set; }
        public string google_id { get; set; }
        public string email { get; set; }
        public string status { get; set; }
        public AccessLevel access_level { get; set; }
        public string phone_number { get; set; }

        public ICollection<User> IAmFollowing { get; set; }
        public ICollection<Task> Tasks { get; set; }
        public ICollection<Achievement> Achivements { get; set; }
        public ICollection<Reminder> Reminders { get; set; }
        public Setting Setting { get; set; }

    }
}
