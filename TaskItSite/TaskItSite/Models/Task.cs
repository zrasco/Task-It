using System;
using System.Collections.Generic;
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
        public int UserId { get; set; }
    }
}
