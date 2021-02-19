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
    public class EnclosuresController : Controller
    {
        private readonly ILogger<EnclosuresController> _logger;
        private readonly IEnclosureService _enclosureService;
        private readonly IGroupService _groupService;
        private readonly ISheepService _sheepService;

        public EnclosuresController(ILogger<EnclosuresController> logger,
            IEnclosureService enclosureService, IGroupService groupService,
            ISheepService sheepService)
        {
            _logger = logger;
            _enclosureService = enclosureService;
            _groupService = groupService;
            _sheepService = sheepService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = await _enclosureService.GetAllNoTrack();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new EnclosureEditViewModel
            {
                Feed = new SelectList(await _enclosureService.GetAllFeed(), "Id", "Name"),
                SelectedGroups = new List<Group>(),
                SelectedSheep = new List<Sheep>(),
                AvailableGroups = await _enclosureService.GetAvailableGroups(),
                AvailableSheep = await _enclosureService.GetAvailableSheep()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(EnclosureEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var enc = new Enclosure
                {
                    Number = model.Number,
                    Description = model.Description,
                    FeedId = model.FeedId,
                    ContainedGroups = await _groupService.GetGroupsByIds(model.GroupIds),
                    ContainedSheep = await _sheepService.GetSheepByIds(model.SheepIds),
                };

                await _enclosureService.Add(enc);
                TempData["Success"] = $"Successfully added enclosure";
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null)
            {
                ViewBag.ErrorMessage = $"Bad request. No enclosure ID specified.";
                return View(nameof(BadRequest));
            }

            var enc = await _enclosureService.GetById(id.Value);

            if (enc is null)
            {
                ViewBag.ErrorMessage = $"Enclosure with ID = {id} not found.";
                return View(nameof(NotFound));
            }

            var model = new EnclosureEditViewModel
            {
                Number = enc.Number,
                Description = enc.Description,
                FeedId = enc.FeedId,
                Feed = new SelectList(await _enclosureService.GetAllFeed(), "Id", "Name"),
                AvailableGroups = await _enclosureService.GetAvailableGroups(),
                AvailableSheep = await _enclosureService.GetAvailableSheep(),
                SelectedGroups = enc.ContainedGroups.ToList(),
                SelectedSheep = enc.ContainedSheep.ToList()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int? id, EnclosureEditViewModel model)
        {
            if(id is null)
            {
                ViewBag.ErrorMessage = $"Bad request. No enclosure ID specified.";
                return View(nameof(BadRequest));
            }

            if (ModelState.IsValid)
            {
                var enc = await _enclosureService.GetById(id.Value);

                if (enc is null)
                {
                    ViewBag.ErrorMessage = $"Enclosure with ID = {id} not found.";
                    return View("NotFound");
                }

                enc.Number = model.Number;
                enc.Description = model.Description;
                enc.FeedId = model.FeedId;
                enc.ContainedGroups = await _groupService.GetGroupsByIds(model.GroupIds);
                enc.ContainedSheep = await _sheepService.GetSheepByIds(model.SheepIds);

                await _enclosureService.Update(enc);
                TempData["Success"] = $"Successfully updated enclosure";
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id is null)
            {
                ViewBag.ErrorMessage = $"Bad request. No enclosure ID specified.";
                return View(nameof(BadRequest));
            }

            var enc = await _enclosureService.GetByIdFullNoTrack(id.Value);

            if (enc is null)
            {
                ViewBag.ErrorMessage = $"Enclosure with ID = {id} not found.";
                return View("NotFound");
            }

            return View(enc);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null)
            {
                ViewBag.ErrorMessage = $"Bad request. No enclosure ID specified.";
                return View(nameof(BadRequest));
            }

            await _enclosureService.Delete(id.Value);
            TempData["Success"] = $"Successfully deleted enclosure";
            return RedirectToAction(nameof(Index));
        }
    }
}
