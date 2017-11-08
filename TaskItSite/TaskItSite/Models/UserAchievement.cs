using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TaskItSite.Models
{
    // This is the achievement the user will have
    public class UserAchievement
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Number")]
        [Key]
        public int UserAchievementID { get; set; }

        public int GlobalAchievementID { get; set; }

        [ForeignKey("GlobalAchievementID")]
        public virtual GlobalAchievement GlobalAchievement { get; set; }

        public DateTime? AchievedTime { get; set; }

        public string ApplicationUserID;

        [ForeignKey("ApplicationUserID")]
        public virtual ApplicationUser ApplicationUser { get; set; }
        
    }
}
