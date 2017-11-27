using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TaskItSite.Models
{
    // This is the achievement the user will have
    public class GlobalSubscription
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Number")]
        [Key]
        public int GlobalSubscriptionID { get; set; }

        public string Name { get; set; }
        public string SubscribingToUserID { get; set; }

        [ForeignKey("SubscribingToUserID")]
        public virtual ApplicationUser SubscribingTo { get; set; }

        public virtual ICollection<Subscription> Subscriptions { get; set; }
    }
}