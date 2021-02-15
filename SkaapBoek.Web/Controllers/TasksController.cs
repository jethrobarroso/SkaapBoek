using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SkaapBoek.Core;
using SkaapBoek.DAL;

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
                .Include(t => t.TaskTemplate)
                .Include(t => t.Status)
                .AsNoTracking();
            return View(await appDbContext.ToListAsync());
        }

        // GET: Tasks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taskInstance = await _context.TaskInstanceSet
                .Include(t => t.Group)
                .Include(t => t.Priority)
                .Include(t => t.Sheep)
                .Include(t => t.TaskTemplate)
                .Include(t => t.Status)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);
            if (taskInstance == null)
            {
                return NotFound();
            }

            return View(taskInstance);
        }

        // GET: Tasks/Create
        public IActionResult Create()
        {
            ViewData["GroupId"] = new SelectList(_context.GroupSet, "Id", "Name");
            ViewData["PriorityId"] = new SelectList(_context.PrioritySet, "Id", "Name");
            ViewData["SheepId"] = new SelectList(_context.SheepSet, "Id", "TagNumber");
            ViewData["TaskTemplateId"] = new SelectList(_context.TaskTemplateSet, "Id", "Name");
            ViewData["StatusList"] = new SelectList(_context.StatusSet, "Id", "Name");
            return View();
        }

        // POST: Tasks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,DurationDays,StartDate,PriorityId,StatusId,TaskTemplateId,ProjectId,SheepId,GroupId")] TaskInstance taskInstance)
        {
            if (ModelState.IsValid)
            {
                _context.Add(taskInstance);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GroupId"] = new SelectList(_context.GroupSet, "Id", "Name", taskInstance.GroupId);
            ViewData["PriorityId"] = new SelectList(_context.PrioritySet, "Id", "Name", taskInstance.PriorityId);
            ViewData["SheepId"] = new SelectList(_context.SheepSet, "Id", "TagNumber", taskInstance.SheepId);
            ViewData["TaskTemplateId"] = new SelectList(_context.TaskTemplateSet, "Id", "Name", taskInstance.TaskTemplateId);
            ViewData["StatusList"] = new SelectList(_context.StatusSet, "Id", "Name", taskInstance.StatusId);
            return View(taskInstance);
        }

        // GET: Tasks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taskInstance = await _context.TaskInstanceSet.FindAsync(id);
            if (taskInstance == null)
            {
                return NotFound();
            }
            ViewData["GroupId"] = new SelectList(_context.GroupSet, "Id", "Name", taskInstance.GroupId);
            ViewData["PriorityId"] = new SelectList(_context.PrioritySet, "Id", "Name", taskInstance.PriorityId);
            ViewData["SheepId"] = new SelectList(_context.SheepSet, "Id", "TagNumber", taskInstance.SheepId);
            ViewData["TaskTemplateId"] = new SelectList(_context.TaskTemplateSet, "Id", "Name", taskInstance.TaskTemplateId);
            ViewData["StatusList"] = new SelectList(_context.StatusSet, "Id", "Name", taskInstance.StatusId);
            return View(taskInstance);
        }

        // POST: Tasks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,DurationDays,StartDate,PriorityId,StatusId,TaskTemplateId,ProjectId,SheepId,GroupId")] TaskInstance taskInstance)
        {
            if (id != taskInstance.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
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
                return RedirectToAction(nameof(Index));
            }
            ViewData["GroupId"] = new SelectList(_context.GroupSet, "Id", "Name", taskInstance.GroupId);
            ViewData["PriorityId"] = new SelectList(_context.PrioritySet, "Id", "Name", taskInstance.PriorityId);
            ViewData["SheepId"] = new SelectList(_context.SheepSet, "Id", "TagNumber", taskInstance.SheepId);
            ViewData["TaskTemplateId"] = new SelectList(_context.TaskTemplateSet, "Id", "Name", taskInstance.TaskTemplateId);
            ViewData["StatusList"] = new SelectList(_context.StatusSet, "Id", "Name", taskInstance.StatusId);
            return View(taskInstance);
        }

        // GET: Tasks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taskInstance = await _context.TaskInstanceSet
                .Include(t => t.Group)
                .Include(t => t.Priority)
                .Include(t => t.Sheep)
                .Include(t => t.TaskTemplate)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (taskInstance == null)
            {
                return NotFound();
            }

            return View(taskInstance);
        }

        // POST: Tasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
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
