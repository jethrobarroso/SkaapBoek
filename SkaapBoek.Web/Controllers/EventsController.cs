using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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
        private readonly IMilsService _milsService;
        private readonly IPenService _penService;

        public EventsController(IEventService eventService, 
            ITaskService taskService,
            IMilsService milsService,
            IPenService penService)
        {
            _eventService = eventService;
            _taskService = taskService;
            _milsService = milsService;
            _penService = penService;
        }

        public async Task<IActionResult> Overview()
        {
            var model = new EventsOverviewViewModel
            {
                MilsEventsModel = new MilsEventsModel(),
                TaskEventsModel = new TaskEventsModel(),
                MoveToPhaseModel = new MoveToPhaseModel(),
                RemoveMilsGroupModel = new RemoveMilsGroupModel()
            };

            model.RemoveMilsGroupModel.Pens = new SelectList(await _penService.GetPens(), "Id", "Name");

            model.TaskEventsModel.TodaysTasks = await _eventService.GetTasksDueToday();
            model.TaskEventsModel.OverdueTasks = await _eventService.GetOverdueTasks();
            model.TaskEventsModel.UpcomingTasks = await _eventService.GetUpcomingTasks(7);

            model.MilsEventsModel.TodaysMilsGroups = await _eventService.GetTodaysMilsEvents();
            model.MilsEventsModel.OverdueMilsGroups = await _eventService.GetOverdueMilsEvents();
            model.MilsEventsModel.UpcomingMilsGroups = await _eventService.GetUpcomingMilsEvents(7);

            model.MoveToPhaseModel.Phases = new SelectList(
                await _milsService.GetAll().AsNoTracking().ToListAsync(),
                "Id", "Activity");
            return View(model);
        }

        [HttpGet("[controller]/[action]/{groupId}")]
        public async Task<IActionResult> RenderMilsEventInfo(int groupId)
        {
            var model = await _eventService.GetMilsEventByGroupId(groupId);
            return PartialView("_MilsAdditionalEventInfo", model);
        }
    }
}
