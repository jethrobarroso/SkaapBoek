using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SkaapBoek.Core;
using SkaapBoek.DAL;
using SkaapBoek.DAL.Services;
using SkaapBoek.Web.ViewModels;

namespace SkaapBoek.Web.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class TasksController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly ITaskService _taskService;

        public TasksController(AppDbContext context, IMapper mapper, ITaskService taskService)
        {
            _context = context;
            _mapper = mapper;
            _taskService = taskService;
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
        public async Task<IActionResult> Create()
        {
            await PopulateLists();
            return View(new TaskEditViewModel());
        }

        private async Task PopulateLists()
        {
            var priorities = await _context.PrioritySet
                .OrderByDescending(p => p.Id)
                .ToListAsync();
            ViewData["GroupId"] = new SelectList(_context.GroupSet, "Id", "Name");
            ViewData["PriorityId"] = new SelectList(priorities, "Id", "Name");
            ViewData["SheepId"] = new SelectList(_context.SheepSet, "Id", "TagNumber");
            ViewData["StatusList"] = new SelectList(_context.StatusSet, "Id", "Name");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TaskEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                await PopulateLists();
                return View(model);
            }

            var task = _mapper.Map<TaskInstance>(model);
            var now = DateTime.Now;
            var startDate = model.StartDate;
            var duration = model.DurationDays ?? 0;

            switch (model.AssignOption)
            {
                case "group":
                    task.SheepId = null;
                    break;
                case "sheep":
                    task.GroupId = null;
                    break;
                default:
                    task.GroupId = null;
                    task.SheepId = null;
                    break;
            }

            if ((model.DurationDays ?? 0) != 0)
                task.EndDate = model.StartDate.AddDays(model.DurationDays.Value);

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
            var model = _mapper.Map<TaskEditViewModel>(taskInstance);

            if (taskInstance == null)
            {
                ViewBag.ErrorMessage = $"Task with ID = {id} not found";
                return View("NotFound");
            }

            await PopulateLists();

            return View(model);
        }

        // POST: Tasks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TaskEditViewModel model)
        {
            var task = await _context.TaskInstanceSet.FindAsync(id);
            if (task == null)
            {
                ViewBag.ErrorMessage = $"Task with ID = {id} not found";
                return View("NotFound");
            }

            if (!ModelState.IsValid)
            {
                await PopulateLists();
                return View(task);
            }

            _mapper.Map(model, task);

            switch (model.AssignOption)
            {
                case "group":
                    task.SheepId = null;
                    break;
                case "sheep":
                    task.GroupId = null;
                    break;
                default:
                    task.GroupId = null;
                    task.SheepId = null;
                    break;
            }

            if ((model.DurationDays ?? 0) != 0)
                task.EndDate = model.StartDate.AddDays(model.DurationDays.Value);

            try
            {
                _context.Update(task);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskInstanceExists(task.Id))
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

        [HttpPost]
        public async Task<IActionResult> Complete(int id)
        {
            var task = await _taskService.GetByIdLite(id, true);

            if (task is null)
                return NotFound();

            //task.StatusId = 3;
            //await _taskService.Update(task);
            return NoContent();
        }

        private bool TaskInstanceExists(int id)
        {
            return _context.TaskInstanceSet.Any(e => e.Id == id);
        }
    }
}
