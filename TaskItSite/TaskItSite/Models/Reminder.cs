using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskItSite.Models
{
    public class Reminder
    {
        public Reminder() { }

        public int id { get; set; }
        public string description { get; set; }
        public string start_datetime { get; set; }
        public string end_datetime { get; set; }
        public string threshold_minutes { get; set; }
        public string is_event { get; set; }
        public string summary { get; set; }
        public int UserID { get; set; }
    }
}
