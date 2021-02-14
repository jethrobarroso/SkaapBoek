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

        #region Herd
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = new SheepIndexViewModel
            {
                HerdSheep = await _sheepService.GetFullNoTrack(),
                Children = await _childService.GetFullNoTrack(),
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
                var sheep = new HerdMember
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

            model.AvailableChildren = await _sheepService.GetAvailableChildren(sheep);
            model.SelectedChildren = await _sheepService.GetSelectedChildren(sheep);

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

                sheep.Relationships.Clear();
                sheep.Relationships = new List<Relationship>();

                if (model.SelectedChildIds != null)
                {
                    foreach (var i in model.SelectedChildIds)
                    {
                        sheep.Relationships.Add(new Relationship
                        {
                            SheepId = sheep.Id,
                            ChildId = i
                        });
                    } 
                }

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
        public async Task<IActionResult> Details(int? id)
        {
            if (id is null)
            {
                ViewBag.ErrorMessage = "No sheep ID specified";
                return View(nameof(BadRequest));
            }

            var sheep = await _sheepService.GetParentWithChildrenNoTrack(id.Value);

            if (SheepNullCheckWith404(sheep, id.Value))
                return View(nameof(NotFound));

            return View(sheep);
        }
        #endregion

        #region Child
        [HttpPost]
        public async Task<IActionResult> DeleteChild(int? id)
        {
            if (id is null)
            {
                ViewBag.ErrorMessage = "No sheep ID specified";
                return View(nameof(BadRequest));
            }

            var child = await _childService.GetById(id.Value);

            if (SheepNullCheckWith404(child, id.Value))
                return View("NotFound");

            await _childService.Delete(id.Value);
            TempData["Success"] = $"Successfully deleted {child.TagNumber}";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> EditChild(int? id)
        {
            if(id is null)
            {
                ViewBag.ErrorMessage = "No child sheep ID specified";
                return View(nameof(BadRequest));
            }

            var child = await _childService.GetById(id.Value);

            var parents = await _childService.GetParentsFromChild(child.Id);

            if (SheepNullCheckWith404(child, id.Value))
                return View(nameof(NotFound));

            var model = new ChildEditViewModel
            {
                BirthDate = child.BirthDate,
                Genders = (await _sheepService.GetGenders()).ToList(),
                GenderId = child.GenderId,
                SalePrice = child.SalePrice,
                TagNumber = child.TagNumber,
                Weight = child.Weight,
                ColorId = child.Color.Id,
                Colors = new SelectList(await _sheepService.GetColors(), "Id", "Name"),
                SheepStatusId = child.SheepStatusId,
                StatusList = new SelectList(await _sheepService.GetSheepStates(), "Id", "Name"),
                FatherId = parents.father?.Id,
                MotherId = parents.mother?.Id,
                Males = new SelectList(await _sheepService.GetSheepPerGenderNoTrack("Male"), "Id", "TagNumber", parents.father?.Id),
                Females = new SelectList(await _sheepService.GetSheepPerGenderNoTrack("Female"), "Id", "TagNumber", parents.mother?.Id)
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditChild(ChildEditViewModel model, int id)
        {
            if (ModelState.IsValid)
            {
                var child = await _childService.GetById(id);
                child.BirthDate = model.BirthDate;
                child.GenderId = model.GenderId;
                child.SalePrice = model.SalePrice;
                child.SheepStatusId = model.SheepStatusId;
                child.TagNumber = model.TagNumber;
                child.Weight = model.Weight;
                child.ColorId = model.ColorId;

                child.Relationships.Clear();
                child.Relationships = new List<Relationship>();
                
                if(model.FatherId != null && 
                    await _sheepService.ContainsSheep(model.FatherId.Value) == true)
                {
                    child.Relationships.Add(new Relationship 
                    { 
                        ChildId = child.Id, SheepId = model.FatherId.Value 
                    });
                }

                if (model.MotherId != null &&
                    await _sheepService.ContainsSheep(model.MotherId.Value) == true)
                {
                    child.Relationships.Add(new Relationship
                    {
                        ChildId = child.Id,
                        SheepId = model.MotherId.Value
                    });
                }

                await _childService.Update(child);
                TempData["Success"] = $"Successfully updated sheep with tag {child.TagNumber}";
                return RedirectToAction(nameof(Index));
            }

            model.Genders = (await _sheepService.GetGenders()).ToList();
            model.Colors = new SelectList(await _sheepService.GetColors(), "Id", "Name");
            model.StatusList = new SelectList(await _sheepService.GetSheepStates(), "Id", "Name");
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> ChildDetails(int? id)
        {
            if (id is null)
            {
                ViewBag.ErrorMessage = "No child sheep ID specified";
                return View(nameof(BadRequest));
            }

            var child = await _childService.GetById(id.Value);

            if (SheepNullCheckWith404(child, id.Value))
                return View(nameof(NotFound));

            var parents = await _childService.GetParentsFromChild(child.Id);

            var model = new ChildDetailsViewModel()
            {
                Child = child,
                Father = parents.father,
                Mother = parents.mother,
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> RemoveParentFromChild(int? childId, int? parentId)
        {
            if(childId is null || parentId is null)
            {
                ViewBag.ErrorMessage = "Not all identifiers were specified";
                return View(nameof(BadRequest));
            }

            var child = await _childService.GetById(childId.Value);
            var parent = child.Relationships.SingleOrDefault(r => r.SheepId == parentId);
            
            if(child == null || parent == null)
            {
                ViewBag.ErrorMessage = $"Id parameters [childId={childId}] and [parentId={parentId} did not match related entries";
                return View(nameof(NotFound));
            }

            child.Relationships.Remove(parent);

            try
            {
                await _childService.Update(child);
            }
            catch (DbUpdateException)
            {
                TempData["Failure"] = "Something went wrong when trying to unlink the parent. Refresh the page and try again";
            }

            TempData["Success"] = "Successfully unlinked parent sheep.";
            return RedirectToAction(nameof(ChildDetails), new { id = childId });
        }
        #endregion
    }
}
