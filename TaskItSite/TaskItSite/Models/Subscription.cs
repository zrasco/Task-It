using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TaskItSite.Models
{
    // This is the achievement the user will have
    public class Subscription
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Number")]
        [Key]
        public int UserSubscriptionID { get; set; }

        public int GlobalSubscriptionID { get; set; }

        [ForeignKey("GlobalSubscriptionID")]
        public virtual GlobalSubscription GlobalSubscription { get; set; }

        public DateTime? SubscribedTime { get; set; }

        public string ApplicationUserID;

        [ForeignKey("ApplicationUserID")]
        public virtual ApplicationUser ApplicationUser { get; set; }
        
    }
}
