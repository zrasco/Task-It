using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskItSite.Models.MainViewModels
{
    public class AchievementWrapper
    {
        public GlobalAchievement Achievement { get; set; }
        public bool IsAchieved { get; set; }
    }

    public class AchievementsViewModel
    {
        public ApplicationUser CurrentUser { get; set; }
        public List<AchievementWrapper> AchievementWrapperList { get; set; }

        public string StatusMessage { get; set; }
    }
}
