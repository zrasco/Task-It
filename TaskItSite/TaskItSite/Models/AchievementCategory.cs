using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TaskItSite.Models
{
    public class AchievementCategory
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Number")]
        public int AchievementCategoryID { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<GlobalAchievement> Achievements { get; set; }
    }
}
