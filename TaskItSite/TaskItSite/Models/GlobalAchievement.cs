using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TaskItSite.Models
{
    // This is the achievement the user will have
    public class GlobalAchievement
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Number")]
        [Key]
        public int GlobalAchievementID { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public string EmojiString { get; set; }

        public int AchievementCategoryID { get; set; }

        [ForeignKey("AchievementCategoryID")]
        public virtual AchievementCategory AchievementCategory { get; set; }

        public virtual ICollection<UserAchievement> UserAchievements { get; set; }
    }
}
