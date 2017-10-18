using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskItSite.Models
{
    public class Reminder
    {
        public Reminder() { }

        public int ID { get; set; }
        public string Description { get; set; }
        public string StartDatetime { get; set; }
        public string EndDatetime { get; set; }
        public string ThresholdMinutes { get; set; }
        public bool IsEvent { get; set; }
        public string Summary { get; set; }
        public int UserID { get; set; }
    }
}
