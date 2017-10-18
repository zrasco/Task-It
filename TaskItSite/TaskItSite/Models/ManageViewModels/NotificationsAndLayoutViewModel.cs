using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TaskItSite.Models.ManageViewModels
{
    public class NotificationsAndLayoutViewModel
    {
        [Required]
        [Display(Name = "Home screen")]
        public HomeScreen HomeScreen
        {
            get;
            set;
        }

        [Display(Name = "Text when message recieved")]
        public bool TextOnMessage { get; set; }

        [Display(Name = "Text when news feed updated")]
        public bool TextOnFeedUpdate { get; set; }

        [Display(Name = "Email when messgae recieved")]
        public bool EmailOnMessage { get; set; }

        [Display(Name = "Email when news feed updated")]
        public bool EmailOnFeedUpdate {
            get;
            set;
        }

        public string StatusMessage { get; set; }
    }
}
