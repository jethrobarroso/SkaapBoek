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
        private readonly IPenService _penService;

        public SheepController(ILogger<SheepController> logger, ISheepService service, IPenService penService)
        {
            _logger = logger;
            _sheepService = service;
            _penService = penService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = new SheepIndexViewModel
            {
                HerdSheep = await _sheepService.GetAllSheepWithRelated()
            };
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            
            var model = new SheepCreateViewModel();
            await PopulateSheepLists(model);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(SheepCreateViewModel model)
        {
            var httpContext = HttpContext;
            if (!ModelState.IsValid)
            {
                await PopulateSheepLists(model);
                return View(model);
            }
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
                SheepCategoryId = model.SheepCategoryId,
                PenId = model.PenId,
                FeedId = model.FeedId,
                FatherId = model.FatherId,
                MotherId = model.MotherId
            };

            try
            {
                await _sheepService.Add(sheep);
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists, " +
                    "please request assistance from support@crybit.co.za");

                await PopulateSheepLists(model);
                return View(model);
            }
            TempData["Success"] = $"Successfully created sheep with tag {sheep.TagNumber}.";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null)
            {
                ViewBag.ErrorMessage = "No sheep ID specified";
                return View(nameof(BadRequest));
            }

            var sheep = await _sheepService.GetById(id.Value);

            if (sheep is null)
            {
                ViewBag.ErrorMessage = $"Sheep with ID = {id} not found";
                return View(nameof(NotFound));
            }

            var model = await PopulateSheepEditViewModel(sheep);

            return View(model);
        }

        private async Task PopulateSheepLists(SheepCreateViewModel vm)
        {
            vm.Genders = (await _sheepService.GetGenders()).ToList();
            vm.Colors = new SelectList(await _sheepService.GetColors(), "Id", "Name");
            vm.StatusList = new SelectList(await _sheepService.GetSheepStates(), "Id", "Name");
            vm.Pens = new SelectList(await _penService.GetAllNoTrack(), "Id", "Number");
            vm.Categories = new SelectList(await _sheepService.GetCategories(), "Id", "Name");
            vm.FeedList = new SelectList(await _sheepService.GetAllFeed(), "Id", "Name");
            vm.Rams = new SelectList(await _sheepService.GetSheepPerGenderNoTrack("Male"), "Id", "TagNumber");
            vm.Ewes = new SelectList(await _sheepService.GetSheepPerGenderNoTrack("Female"), "Id", "TagNumber");
        }

        private async Task<SheepCreateViewModel> PopulateSheepEditViewModel(Sheep sheep)
        {
            var model = new SheepCreateViewModel
            {
                Id = sheep.Id,
                AcquireDate = sheep.AcquireDate,
                BirthDate = sheep.BirthDate,
                GenderId = sheep.GenderId,
                CostPrice = sheep.CostPrice,
                SalePrice = sheep.SalePrice,
                TagNumber = sheep.TagNumber,
                Weight = sheep.Weight,
                ColorId = sheep.Color.Id,
                SheepStatusId = sheep.SheepStatusId,
                PenId = sheep.PenId,
                SheepCategoryId = sheep.SheepCategoryId,
                FeedId = sheep.FeedId,
                FatherId = sheep.FatherId,
                MotherId = sheep.MotherId,
            };

            await PopulateSheepLists(model);
            return model;
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SheepCreateViewModel model, int id)
        {
            if (!ModelState.IsValid)
            {
                await PopulateSheepLists(model);
                return View(model);
            }

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
            sheep.SheepCategoryId = model.SheepCategoryId;
            sheep.FeedId = model.FeedId;
            sheep.MotherId = model.MotherId;
            sheep.FatherId = model.FatherId;

            await _sheepService.Update(sheep);
            TempData["Success"] = $"Successfully updated sheep with tag {sheep.TagNumber}";
            return RedirectToAction(nameof(Index));
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
        public async Task<IActionResult> Details(int? id)
        {
            if (id is null)
            {
                ViewBag.ErrorMessage = "No sheep ID specified";
                return View(nameof(BadRequest));
            }

            var sheep = await _sheepService.GetById(id.Value);

            if (sheep is null)
            {
                ViewBag.ErrorMessage = $"Sheep with ID = {id} not found";
                return View(nameof(NotFound));
            }

            var model = new SheepDetailsViewModel
            {
                Sheep = sheep,
                Mother = await _sheepService.GetById(sheep.FatherId),
                Father = await _sheepService.GetById(sheep.MotherId),
                Children = await _sheepService.GetParentChildren(sheep.Id)
            };

            return View(model);
        }
    }
}
