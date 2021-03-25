using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SkaapBoek.Core;
using SkaapBoek.DAL;
using SkaapBoek.DAL.Services;
using SkaapBoek.Web.Utilities;
using SkaapBoek.Web.ViewModels;

namespace SkaapBoek.Web.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class GroupsController : Controller
    {
        private readonly ILogger<GroupsController> _logger;
        private readonly IGroupService _groupService;
        private readonly IPenService _penService;

        public GroupsController(ILogger<GroupsController> logger, IGroupService groupService,
            IPenService penService)
        {
            _logger = logger;
            _groupService = groupService;
            _penService = penService;
        }

        // GET: Groups
        public async Task<IActionResult> Index()
        {
            return View(await _groupService.GetAll()
                .Include(g => g.MilsPhase)
                .Include(g => g.Pen)
                .AsNoTracking()
                .ToListAsync());
        }

        // GET: Groups/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                ViewBag.ErrorMessage = "Bad request. No group ID specified.";
                return View(nameof(BadRequest));
            }

            var @group = await _groupService.GetByIdFull(id.Value, true);
            if (@group == null)
            {
                ViewBag.ErrorMessage = $"Group with ID = {id} not found.";
                return View(nameof(NotFound));
            }

            return View(@group);
        }

        // GET: Groups/Create
        public async Task<IActionResult> Create()
        {
            var vm = new GroupEditViewModel
            {
                Pens = new SelectList(await _penService.GetAllNoTrack(), "Id", "Name")
            };

            return View(vm);
        }

        // POST: Groups/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GroupEditViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var group = new Group();
            group.Name = model.Name;
            group.Description = model.Description;
            group.PenId = model.PenId;

            await _groupService.Add(group);
            TempData["Success"] = $"Successfully updated group.";
            return RedirectToAction(nameof(Index));
        }

        // GET: Groups/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null)
            {
                ViewBag.ErrorMessage = $"Group with ID = {id} not found.";
                return View("NotFound");
            }

            var @group = await _groupService.GetByIdFull(id.Value);

            if (@group is null)
            {
                ViewBag.ErrorMessage = $"Bad request. Group ID not specified.";
                return View(nameof(BadRequest));
            }

            var vm = new GroupEditViewModel
            {
                Name = group.Name,
                Description = group.Description,
                PenId = group.PenId,
                Pens = new SelectList(await _penService.GetAllNoTrack(), "Id", "Name"),
                AvailableSheep = await _groupService.GetAvailableSheep(id.Value),
                SelectedSheep = await _groupService.GetSelectedSheep(id.Value),
            };

            return View(vm);
        }

        // POST: Groups/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Edit(int id, GroupEditViewModel model)
        {
            var group = await _groupService.GetByIdFull(id, true);

            if (group is null)
            {
                ViewBag.ErrorMessage = $"Group with ID = {id} not found.";
                return View("NotFound");
            }

            if (!ModelState.IsValid)
                return View(group);

            group.Name = model.Name;
            group.Description = model.Description;
            group.PenId = model.PenId;
            group.GroupedSheep.Clear();
            group.GroupedSheep = new List<GroupedSheep>();

            if (model.SelectedSheepIds != null)
            {
                foreach (int i in model.SelectedSheepIds)
                {
                    group.GroupedSheep.Add(new GroupedSheep { GroupId = id, SheepId = i });
                }
            }

            try
            {
                await _groupService.Update(group);
            }
            catch (DbUpdateException e)
            {
                _logger.LogError($"Error updating group with ID = {id}\nException Message - {e.Message}");
                return View("Error");
            }

            TempData["Success"] = $"Successfully updated group.";
            return RedirectToAction(nameof(Index));
        }

        // POST: Groups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _groupService.Delete(id);
            }
            catch (DbUpdateException e)
            {
                _logger.LogError($"Error deleting group with ID = {id}\nException Message - {e.Message}");
                return View("Error");
            }

            TempData["Success"] = $"Successfully deleted group.";
            return RedirectToAction(nameof(Index));
        }
    }
}
