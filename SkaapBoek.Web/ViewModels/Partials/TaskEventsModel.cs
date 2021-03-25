using SkaapBoek.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkaapBoek.Web.ViewModels.Partials
{
    public class TaskEventsModel
    {
        public IEnumerable<TaskInstance> TodaysTasks { get; set; }
        public IEnumerable<TaskInstance> OverdueTasks { get; set; }
        public IEnumerable<TaskInstance> UpcomingTasks { get; set; }
    }
}
