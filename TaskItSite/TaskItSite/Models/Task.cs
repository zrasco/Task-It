using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TaskItSite.Models
{
    public class Task
    {
        public Task() { }

        public int ID { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime DueDate { get; set; }

        [Required]
        public string Summary { get; set; }

        public string Description { get; set; }
        public string ApplicationUserId { get; set; }

        [Display(Name = "Active task")]
        public bool IsActive { get; set; }

        [Display(Name = "Private task")]
        public bool IsPrivate { get; set; }
    }
}
