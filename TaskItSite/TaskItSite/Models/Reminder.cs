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
        public DateTime StartDatetime { get; set; }
        public DateTime EndDatetime { get; set; }
        public long ThresholdMinutes { get; set; }
        public bool IsEvent { get; set; }
        public string Summary { get; set; }
        public Guid UserID { get; set; }
    }
}
