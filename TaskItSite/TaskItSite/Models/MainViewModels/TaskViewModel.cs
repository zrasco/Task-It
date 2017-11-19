using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskItSite.Models.MainViewModels
{
    //exists because other user tasks will be loaded later on
    public class TaskWrapper
    {
        public Task Task { get; set; }
        public bool isTask {get; set;}
    }
    public class TaskViewModel
    {
        public ApplicationUser CurrentUser { get; set; }
        public List<TaskWrapper> TaskWrapperList { get; set; }

        public string StatusMessage { get; set; }
    }
}
