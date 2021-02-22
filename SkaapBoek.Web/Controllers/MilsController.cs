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
        private readonly IMapper _mapper;

        public MilsController(IMilsService milsService, IMapper mapper)
        {
            _milsService = milsService;
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
            return View(_mapper.Map<MilsPhaseCreateViewModel>(phase));
        }

        [HttpPost]
        public async Task<IActionResult> CreatePhase(MilsPhaseCreateViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var phase = _mapper.Map<MilsPhase>(model);
            await _milsService.Add(phase);
            TempData["Success"] = $"Succesfully added phase";
            return RedirectToAction(nameof(Index));
        }
    }
}
