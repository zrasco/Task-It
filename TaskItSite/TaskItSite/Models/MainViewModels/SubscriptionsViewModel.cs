using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskItSite.Models.MainViewModels
{
    public class SubscriptionWrapper
    {
        public ApplicationUser SubscribedUser { get; set; }
        public string SubscribedUserID { get; set; }
        public bool IsSubscribed { get; set; }
    }

    public class SubscriptionsViewModel
    {
        public ApplicationUser CurrentUser { get; set; }
        public List<SubscriptionWrapper> SubscriptionWrapperList { get; set; }

        public string StatusMessage { get; set; }
    }
}
