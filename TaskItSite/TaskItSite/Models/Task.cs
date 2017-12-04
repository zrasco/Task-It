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
        public System.DateTime CreatedDate { get; set; }
        public System.DateTime DueDate { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public bool IsPin { get; set; }
        public string ApplicationUserId { get; set; }
        public bool IsActive { get; set; }

        [Display(Name = "Private task")]
        public bool IsPrivate { get; set; }
    }
}
