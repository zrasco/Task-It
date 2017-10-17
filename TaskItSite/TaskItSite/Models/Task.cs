using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskItSite.Models
{
    public class Task
    {
        public Task() { }

        public int id { get; set; }
        public System.DateTime created_date { get; set; }
        public System.DateTime due_date { get; set; }
        public string summary { get; set; }
        public string description { get; set; }
        public string is_pin { get; set; }
        public int UserId { get; set; }
    }
}
