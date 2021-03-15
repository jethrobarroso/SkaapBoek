using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SkaapBoek.DAL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkaapBoek.Web.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;
        private readonly ILogger<TasksController> _logger;

        public TasksController(ITaskService taskService, ILogger<TasksController> logger)
        {
            _taskService = taskService;
            _logger = logger;
        }

        [HttpPut("{id}/Complete")]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> Complete(int id)
        {
            var task = await _taskService.GetByIdLite(id, true);

            if (task is null)
                return NotFound();

            task.StatusId = 3;
            await _taskService.Update(task);
            return NoContent();
        }

        [HttpPut("{id}/MarkAsInProgress")]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> MarkAsInProgress(int id)
        {
            var task = await _taskService.GetByIdLite(id, true);

            if (task is null)
                return NotFound();

            task.StatusId = 2;
            await _taskService.Update(task);
            return NoContent();
        }
    }
}
