using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SkaapBoek.Core;
using SkaapBoek.DAL;
using SkaapBoek.DAL.Services;
using SkaapBoek.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkaapBoek.Web.Controllers
{
    public class MilsController : Controller
    {
        private readonly IMilsService _milsService;
        private readonly IMilsTaskService _milsTaskService;
        private readonly IGroupService _groupService;
        private readonly IMapper _mapper;
        private readonly ILogger<MilsController> _logger;

        public MilsController(IMilsService milsService, 
            IMilsTaskService milsTaskService,
            IGroupService groupService,
            IMapper mapper, 
            ILogger<MilsController> logger)
        {
            _milsService = milsService;
            _milsTaskService = milsTaskService;
            _groupService = groupService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = new MilsPhaseIndexViewModel
            {
                PhaseList = await _milsService.GetAllWithTasksSorted(),
                AvailableGroups = new SelectList(await _milsService.GetAvailableGroups(), "Id", "Name")
            };
            
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> CreatePhase()
        {
            var allPhases = await _milsService.GetAll()
                    .AsNoTracking()
                    .Select(p => new SelectListItem
                    {
                        Value = p.PhaseSequence.ToString(),
                        Text = p.Activity
                    }).ToListAsync();

            ViewBag.Phases = allPhases;
            var model = new MilsPhaseDto();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePhase(MilsPhaseDto model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var phase = _mapper.Map<MilsPhase>(model);
            await _milsService.Add(phase);
            TempData["Success"] = $"Succesfully added phase";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> EditPhase(int? id)
        {
            if (id is null)
            {
                ViewBag.ErrorMessage = "Bad request. No phase ID specified.";
                return View("BadRequest");
            }

            var phase = await _milsService.GetById(id.Value);

            if (phase is null)
            {
                ViewBag.ErrorMessage = $"Mils phase with ID = {id} not found";
                return View("NotFound");
            }

            var model = _mapper.Map<MilsPhaseEditViewModel>(phase);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditPhase(MilsPhaseEditViewModel model, int? id)
        {
            if (id is null)
            {
                ViewBag.ErrorMessage = "Bad request. No phase ID specified.";
                return View("BadRequest");
            }
            var phase = await _milsService.GetById(id.Value, true);
            if (phase is null)
            {
                ViewBag.ErrorMessage = $"Mils phase with ID = {id} not found";
                return View("NotFound");
            }

            phase.Activity = model.Activity;
            phase.Days = model.Days;
            await _milsService.Update(phase);
            TempData["Success"] = "Successfully updated phase.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask(MilsPhaseEditViewModel model)
        {
            var phase = await _milsService.GetById(model.MilsPhaseId, true);

            if (phase is null)
            {
                ViewBag.ErrorMessage = $"M.I.L.S phase with ID = {model.MilsPhaseId} not found.";
                return View("NotFound");
            }

            var task = _mapper.Map<MilsTask>(model);
            phase.Tasks.Add(task);
            await _milsService.Update(phase);
            TempData["Success"] = "Successfully updated phase.";
            return RedirectToAction(nameof(EditPhase), new { id = model.MilsPhaseId });
        }

        [HttpPost]
        public async Task<IActionResult> EditTask(int? id, int phaseId, string instructions)
        {
            if (id is null)
            {
                ViewBag.ErrorMessage = "Bad request. No task ID specified.";
                return View("BadRequest");
            }
            var task = await _milsTaskService.GetById(id);
            if (task is null)
            {
                ViewBag.ErrorMessage = $"Phase task with ID = {id} not found";
                return View("NotFound");
            }

            task.Instructions = instructions;
            await _milsTaskService.Update(task);
            TempData["Success"] = "Successfully updated phase task.";
            return RedirectToAction(nameof(EditPhase), new { id = phaseId });
        }

        [HttpPost]
        public async Task<IActionResult> AssignGroup(MilsPhaseIndexViewModel model)
        {
            var group = await _groupService.GetByIdLite(model.SelectedGroupId, true);
            _mapper.Map(model, group);
            await _groupService.Update(group);
            TempData["Success"] = $"Successfully queued group to phase.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> DeletePhase(int? id)
        {
            if (id is null)
            {
                ViewBag.ErrorMessage = "Bad request. No phase ID specified.";
                return View("BadRequest");
            }
            await _milsService.Delete(id.Value);
            TempData["Success"] = "Successfully deleted phase.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteTask(int phaseId, int? id)
        {
            if (id is null)
            {
                ViewBag.ErrorMessage = "Bad request. No phase ID specified.";
                return View("BadRequest");
            }
            await _milsTaskService.Delete(id.Value);
            TempData["Success"] = "Successfully deleted task.";
            return RedirectToAction(nameof(EditPhase), new { id = phaseId });
        }
    }
}
