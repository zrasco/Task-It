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
        public int SubscriptionID { get; set; }

        public string SubscribingUserID { get; set; }
        public string SubscribingToUserID { get; set; }

        [ForeignKey("SubscribingUserID")]
        public virtual ApplicationUser SubscribingUser { get; set; }

        [ForeignKey("SubscribingToUserID")]
        public virtual ApplicationUser SubscribingTo { get; set; }

    }

}

