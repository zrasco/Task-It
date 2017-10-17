using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskItSite.Models
{
    public class Setting
    {
        public Setting() { }

        public string home_screen { get; set; }
        public int notification_settings { get; set; }
        public int id { get; set; }
    }
}
