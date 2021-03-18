using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkaapBoek.DAL;
using SkaapBoek.DAL.Services;
using SkaapBoek.Web.ApiDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkaapBoek.Web.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class MilsController : ControllerBase
    {
        private readonly IMilsService _milsService;

        public MilsController(IMilsService milsService)
        {
            _milsService = milsService;
        }

        [HttpPut("UpdatePhaseSequence")]
        [IgnoreAntiforgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> UpdatePhaseSequence(UpdatedPhaseSequence model)
        {
            if (User.Identity.IsAuthenticated)
            {
                await _milsService.UpdatePhaseSequence(model.OldSequence, model.NewSequence);
                return NotFound();
            }

            return StatusCode(401);
        }

        //[HttpPut("{id}/Complete")]
        //[IgnoreAntiforgeryToken]
        //public async Task<IActionResult> Complete(int id)
        //{
        //    var task = await _taskService.GetByIdLite(id, true);

        //    if (task is null)
        //        return NotFound();

        //    task.StatusId = 3;
        //    await _taskService.Update(task);
        //    return NoContent();
        //}

        //[HttpPut("{id}/Complete")]
        //[IgnoreAntiforgeryToken]
        //public async Task<IActionResult> MoveToNextPhase(int id)
        //{
        //    var task = await _taskService.GetByIdLite(id, true);

        //    if (task is null)
        //        return NotFound();

        //    task.StatusId = 3;
        //    await _taskService.Update(task);
        //    return NoContent();
        //}
    }
}
