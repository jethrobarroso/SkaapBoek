using Microsoft.AspNetCore.Mvc;
using SkaapBoek.Core;
using SkaapBoek.DAL.Services;
using SkaapBoek.Web.ViewModels;
using SkaapBoek.Web.ViewModels.Partials;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkaapBoek.Web.Controllers
{
    public class EventsController : Controller
    {
        private readonly IEventService _eventService;
        private readonly ITaskService _taskService;

        public EventsController(IEventService eventService, ITaskService taskService)
        {
            _eventService = eventService;
            _taskService = taskService;
        }

        public async Task<IActionResult> Overview()
        {
            var model = new EventsOverviewViewModel
            {
                MilsEventsModel = new MilsEventsModel(),
                TaskEventsModel = new TaskEventsModel()
            };

            model.TaskEventsModel.TodaysTasks = await _eventService.GetTasksDueToday();
            model.TaskEventsModel.OverdueTasks = await _eventService.GetOverdueTasks();
            model.TaskEventsModel.UpcomingTasks = await _eventService.GetUpcomingTasks(7);

            model.MilsEventsModel.TodaysMilsGroups = await _eventService.GetTodaysMilsEvents();
            model.MilsEventsModel.OverdueMilsGroups = await _eventService.GetOverdueMilsEvents();
            model.MilsEventsModel.UpcomingMilsGroups = await _eventService.GetUpcomingMilsEvents(7);
            //var todaysTasks = await _eventService.GetTasksDueToday();
            return View(model);
        }
    }
}
