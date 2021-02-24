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
        public async Task<IActionResult> UpdatePhaseSequence(PhaseSequenceChange model)
        {
            await _milsService.UpdatePhaseSequence(model.OldSequence, model.NewSequence);

            return NoContent();
        }
    }
}
