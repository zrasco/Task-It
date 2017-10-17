using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskItSite.Models
{
    public class Achievement
    {
        public int id { get; set; }
        public int achievementCategoryId { get; set; }
        public string name { get; set; }
        public string description { get; set; }
    }
}
