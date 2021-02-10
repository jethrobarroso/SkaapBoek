using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SkaapBoek.Core;
using SkaapBoek.DAL;
using SkaapBoek.DAL.Services;
using SkaapBoek.Web.Utilities;
using SkaapBoek.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SkaapBoek.Web.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class SheepController : AppController
    {
        private readonly ILogger<SheepController> _logger;
        private readonly ISheepService _sheepService;
        private readonly IChildService _childService;

        public SheepController(ILogger<SheepController> logger, ISheepService service, IChildService childService)
        {
            _logger = logger;
            _sheepService = service;
            _childService = childService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = new SheepIndexViewModel
            {
                HerdSheep = await _sheepService.GetFullNoTrack(),
                FeedlotSheep = await _childService.GetFullNoTrack(),
            };
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new SheepCreateViewModel
            {
                Genders = (await _sheepService.GetGenders()).ToList(),
                Colors = new SelectList(await _sheepService.GetColors(), "Id", "Name"),
                StatusList = new SelectList(await _sheepService.GetSheepStates(), "Id", "Name")
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(SheepCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var sheep = new Sheep
                {
                    AcquireDate = model.AcquireDate,
                    BirthDate = model.BirthDate,
                    GenderId = model.GenderId,
                    TagNumber = model.TagNumber,
                    Weight = model.Weight,
                    CostPrice = model.CostPrice,
                    SheepStatusId = model.SheepStatusId,
                    SalePrice = model.SalePrice,
                    ColorId = model.ColorId,
                };

                try
                {
                    await _sheepService.Add(sheep);
                }
                catch (DbUpdateException ex)
                {
                    _logger.LogError(ex.Message);
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "please request assistance from support@crybit.co.za");
                }
                TempData["Success"] = $"Successfully created sheep with tag {sheep.TagNumber}.";
                return RedirectToAction(nameof(Index));

            }
            model.Genders = (await _sheepService.GetGenders()).ToList();
            model.Colors = new SelectList(await _sheepService.GetColors(), "Id", "Name");
            model.StatusList = new SelectList(await _sheepService.GetSheepStates(), "Id", "Name");
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var sheep = await _sheepService.GetById(id);

            if (SheepNullCheckWith404(sheep, id))
                return View("NotFound");

            var model = new SheepEditViewModel
            {
                AcquireDate = sheep.AcquireDate,
                BirthDate = sheep.BirthDate,
                Genders = (await _sheepService.GetGenders()).ToList(),
                GenderId = sheep.GenderId,
                CostPrice = sheep.CostPrice,
                SalePrice = sheep.SalePrice,
                TagNumber = sheep.TagNumber,
                Weight = sheep.Weight,
                ColorId = sheep.Color.Id,
                Colors = new SelectList(await _sheepService.GetColors(), "Id", "Name"),
                SheepStatusId = sheep.SheepStatusId,
                StatusList = new SelectList(await _sheepService.GetSheepStates(), "Id", "Name")
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SheepEditViewModel model, int id)
        {
            if (ModelState.IsValid)
            {
                var sheep = await _sheepService.GetById(id);
                sheep.AcquireDate = model.AcquireDate;
                sheep.BirthDate = model.BirthDate;
                sheep.GenderId = model.GenderId;
                sheep.CostPrice = model.CostPrice;
                sheep.SalePrice = model.SalePrice;
                sheep.SheepStatusId = model.SheepStatusId;
                sheep.TagNumber = model.TagNumber;
                sheep.Weight = model.Weight;
                sheep.ColorId = model.ColorId;

                await _sheepService.Update(sheep);
                TempData["Success"] = $"Successfully updated sheep with tag {sheep.TagNumber}";
                return RedirectToAction(nameof(Index));
            }

            model.Genders = (await _sheepService.GetGenders()).ToList();
            model.Colors = new SelectList(await _sheepService.GetColors(), "Id", "Name");
            model.StatusList = new SelectList(await _sheepService.GetSheepStates(), "Id", "Name");
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var sheep = await _sheepService.GetById(id);

            if (SheepNullCheckWith404(sheep, id))
                return View("NotFound");

            await _sheepService.Delete(id);
            TempData["Success"] = $"Successfully deleted {sheep.TagNumber}";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var sheep = await _sheepService.GetById(id);

            if (SheepNullCheckWith404(sheep, id))
                return View("NotFound");

            return View(sheep);
        }
    }
}
