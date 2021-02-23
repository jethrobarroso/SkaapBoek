using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkaapBoek.Core;
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
        private readonly IMapper _mapper;

        public MilsController(IMilsService milsService, IMilsTaskService milsTaskService,
            IMapper mapper)
        {
            _milsService = milsService;
            _milsTaskService = milsTaskService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var phases = await _milsService.GetAllWithTasksSorted();
            return View(phases);
        }

        [HttpGet]
        public IActionResult CreatePhase()
        {
            var phase = new MilsPhase();
            return View(_mapper.Map<MilsPhaseDto>(phase));
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
        public async Task<IActionResult> CreateTask(MilsPhaseEditViewModel model)
        {
            var phase = await _milsService.GetById(model.MilsPhaseId, true);

            if(phase is null)
            {
                ViewBag.ErrorMessage = $"M.I.L.S phase with ID = {model.MilsPhaseId} not found.";
                return View("NotFound");
            }

            if(phase.Tasks.Any(p => p.TaskOrder == model.PhaseOrder))
            {
                ModelState.AddModelError("", "Another task already has this order");
                return View(nameof(EditPhase), new { id = model.MilsPhaseId });
            }

            var task = _mapper.Map<MilsTask>(model);
            phase.Tasks.Add(task);
            await _milsService.Update(phase);

            return RedirectToAction(nameof(EditPhase), new { id = model.MilsPhaseId });
        }
    }
}
