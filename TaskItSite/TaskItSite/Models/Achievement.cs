using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskItSite.Models
{
    public class Achievement
    {
        public Achievement() { }

        public int ID { get; set; }
        public int AchievementCategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
