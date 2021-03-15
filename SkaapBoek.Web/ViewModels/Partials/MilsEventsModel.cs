using SkaapBoek.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkaapBoek.Web.ViewModels.Partials
{
    public class MilsEventsModel
    {
        public IEnumerable<Group> TodaysMilsGroups { get; set; }
        public IEnumerable<Group> OverdueMilsGroups { get; set; }
        public IEnumerable<Group> UpcomingMilsGroups { get; set; }
    }
}
