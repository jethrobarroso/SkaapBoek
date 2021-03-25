using Microsoft.AspNetCore.Mvc.Rendering;
using SkaapBoek.Web.ViewModels.Partials;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkaapBoek.Web.ViewModels
{
    public class EventsOverviewViewModel
    {
        public MilsEventsModel MilsEventsModel { get; set; }
        public TaskEventsModel TaskEventsModel { get; set; }
        public MoveToPhaseModel MoveToPhaseModel { get; set; }
    }
}
