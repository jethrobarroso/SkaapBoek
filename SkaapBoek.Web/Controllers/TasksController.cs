using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SkaapBoek.Core;
using SkaapBoek.DAL;
using SkaapBoek.Web.ViewModels;

namespace SkaapBoek.Web.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class TasksController : Controller
    {
        private readonly AppDbContext _context;

        public TasksController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Tasks
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.TaskInstanceSet
                .Include(t => t.Group)
                .Include(t => t.Priority)
                .Include(t => t.Sheep)
                .Include(t => t.Status)
                .AsNoTracking();
            return View(await appDbContext.ToListAsync());
        }

        // GET: Tasks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                ViewBag.ErrorMessage = "Bad request. Task ID not specified.";
                return View("BadRequest");
            }

            var taskInstance = await _context.TaskInstanceSet
                .Include(t => t.Group)
                .Include(t => t.Priority)
                .Include(t => t.Sheep)
                .Include(t => t.Status)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (taskInstance == null)
            {
                ViewBag.ErrorMessage = $"Task with ID = {id} not found";
                return View("NotFound");
            }

            return View(taskInstance);
        }

        // GET: Tasks/Create
        public IActionResult Create()
        {
            PopulateLists();
            return View();
        }

        private void PopulateLists()
        {
            ViewData["GroupId"] = new SelectList(_context.GroupSet, "Id", "Name");
            ViewData["PriorityId"] = new SelectList(_context.PrioritySet, "Id", "Name");
            ViewData["SheepId"] = new SelectList(_context.SheepSet, "Id", "TagNumber");
            ViewData["StatusList"] = new SelectList(_context.StatusSet, "Id", "Name");
        }

        // POST: Tasks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TaskEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                PopulateLists();
                return View(model);
            }

            var task = new TaskInstance
            {
                Name = model.Name,
                Description = model.Description,
                DurationDays = model.Duration,
                GroupId = model.GroupId,
                PriorityId = model.PriorityId,
                SheepId = model.SheepId,
                StatusId = model.StatusId
            };

            _context.Add(task);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Successfully created task.";
            return RedirectToAction(nameof(Index));

        }

        // GET: Tasks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                ViewBag.ErrorMessage = "Bad request. Task ID not specified.";
                return View("BadRequest");
            }

            var taskInstance = await _context.TaskInstanceSet.FindAsync(id);

            if (taskInstance == null)
            {
                ViewBag.ErrorMessage = $"Task with ID = {id} not found";
                return View("NotFound");
            }

            PopulateLists();

            return View(taskInstance);
        }

        // POST: Tasks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TaskEditViewModel model)
        {
            var taskInstance = await _context.TaskInstanceSet.FindAsync(id);
            if (taskInstance == null)
            {
                ViewBag.ErrorMessage = $"Task with ID = {id} not found";
                return View("NotFound");
            }

            if (!ModelState.IsValid)
            {
                PopulateLists();
                return View(taskInstance);
            }

            taskInstance.Name = model.Name;
            taskInstance.Description = model.Description;
            taskInstance.DurationDays = model.Duration;
            taskInstance.GroupId = model.GroupId;
            taskInstance.PriorityId = model.PriorityId;
            taskInstance.SheepId = model.SheepId;
            taskInstance.StatusId = model.StatusId;

            try
            {
                
                _context.Update(taskInstance);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskInstanceExists(taskInstance.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            TempData["Success"] = "Successfully updated task.";
            return RedirectToAction(nameof(Index));
        }

        // POST: Tasks/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var taskInstance = await _context.TaskInstanceSet.FindAsync(id);
            _context.TaskInstanceSet.Remove(taskInstance);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TaskInstanceExists(int id)
        {
            return _context.TaskInstanceSet.Any(e => e.Id == id);
        }
    }
}
