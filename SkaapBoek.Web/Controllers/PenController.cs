using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using SkaapBoek.DAL.Services;
using SkaapBoek.Web.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SkaapBoek.Core;

namespace SkaapBoek.Web.Controllers
{
    public class PenController : Controller
    {
        private readonly ILogger<PenController> _logger;
        private readonly IPenService _penService;
        private readonly IGroupService _groupService;
        private readonly ISheepService _sheepService;

        public PenController(ILogger<PenController> logger,
            IPenService penService, IGroupService groupService,
            ISheepService sheepService)
        {
            _logger = logger;
            _penService = penService;
            _groupService = groupService;
            _sheepService = sheepService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = await _penService.GetAllNoTrack();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new PenEditViewModel
            {
                Feed = new SelectList(await _penService.GetAllFeed(), "Id", "Name"),
                SelectedGroups = new List<Group>(),
                SelectedSheep = new List<Sheep>(),
                AvailableGroups = await _penService.GetAvailableGroups(),
                AvailableSheep = await _penService.GetAvailableSheep()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(PenEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var enc = new Pen
                {
                    Number = model.Number,
                    Description = model.Description,
                    FeedId = model.FeedId,
                    ContainedGroups = await _groupService.GetGroupsByIds(model.GroupIds),
                    ContainedSheep = await _sheepService.GetSheepByIds(model.SheepIds),
                };

                await _penService.Add(enc);
                TempData["Success"] = $"Successfully added pen.";
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null)
            {
                ViewBag.ErrorMessage = $"Bad request. No pen ID specified.";
                return View(nameof(BadRequest));
            }

            var enc = await _penService.GetById(id.Value);

            if (enc is null)
            {
                ViewBag.ErrorMessage = $"Pen with ID = {id} not found.";
                return View(nameof(NotFound));
            }

            var model = new PenEditViewModel
            {
                Number = enc.Number,
                Description = enc.Description,
                FeedId = enc.FeedId,
                Feed = new SelectList(await _penService.GetAllFeed(), "Id", "Name"),
                AvailableGroups = await _penService.GetAvailableGroups(),
                AvailableSheep = await _penService.GetAvailableSheep(),
                SelectedGroups = enc.ContainedGroups.ToList(),
                SelectedSheep = enc.ContainedSheep.ToList()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int? id, PenEditViewModel model)
        {
            if(id is null)
            {
                ViewBag.ErrorMessage = $"Bad request. No pen ID specified.";
                return View(nameof(BadRequest));
            }

            if (ModelState.IsValid)
            {
                var enc = await _penService.GetById(id.Value);

                if (enc is null)
                {
                    ViewBag.ErrorMessage = $"Pen with ID = {id} not found.";
                    return View("NotFound");
                }

                enc.Number = model.Number;
                enc.Description = model.Description;
                enc.FeedId = model.FeedId;
                enc.ContainedGroups = await _groupService.GetGroupsByIds(model.GroupIds);
                enc.ContainedSheep = await _sheepService.GetSheepByIds(model.SheepIds);

                await _penService.Update(enc);
                TempData["Success"] = $"Successfully updated pen";
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id is null)
            {
                ViewBag.ErrorMessage = $"Bad request. No pen ID specified.";
                return View(nameof(BadRequest));
            }

            var enc = await _penService.GetByIdFullNoTrack(id.Value);

            if (enc is null)
            {
                ViewBag.ErrorMessage = $"Pen with ID = {id} not found.";
                return View("NotFound");
            }

            return View(enc);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null)
            {
                ViewBag.ErrorMessage = $"Bad request. No pen ID specified.";
                return View(nameof(BadRequest));
            }

            await _penService.Delete(id.Value);
            TempData["Success"] = $"Successfully deleted pen.";
            return RedirectToAction(nameof(Index));
        }
    }
}
