using SkaapBoek.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkaapBoek.Web.ViewModels.Partials
{
    public class MilsEventsModel
    {
        public IEnumerable<MilsEventModel> TodaysMilsGroups { get; set; }
        public IEnumerable<MilsEventModel> OverdueMilsGroups { get; set; }
        public IEnumerable<MilsEventModel> UpcomingMilsGroups { get; set; }
    }
}
