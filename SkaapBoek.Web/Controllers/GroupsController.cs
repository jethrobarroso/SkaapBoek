using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SkaapBoek.Core;
using SkaapBoek.DAL;
using SkaapBoek.Web.Utilities;
using SkaapBoek.Web.ViewModels;

namespace SkaapBoek.Web.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class GroupsController : Controller
    {
        private readonly ILogger<GroupsController> _logger;
        private readonly AppDbContext _context;

        public GroupsController(ILogger<GroupsController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        // GET: Groups
        public async Task<IActionResult> Index()
        {
            return View(await _context.GroupSet.ToListAsync());
        }

        // GET: Groups/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @group = await _context.GroupSet
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@group == null)
            {
                return NotFound();
            }

            return View(@group);
        }

        // GET: Groups/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Groups/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description")] Group @group)
        {
            if (ModelState.IsValid)
            {
                _context.Add(@group);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(@group);
        }

        // GET: Groups/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @group = await _context.GroupSet
                .SingleOrDefaultAsync(g => g.Id == id);

            var vm = new GroupEditViewModel
            {
                Name = group.Name,
                Description = group.Description,
            };

            vm.AvailableSheep = await (from s in _context.SheepSet
                                       join sg in _context.GroupedHerdMemberSet
                                       on s.Id equals sg.SheepId into jg
                                       from g in jg.DefaultIfEmpty()
                                       select s).Distinct()
                                       .Where(s => s.GroupedSheep.Count() == 0 ||
                                       !s.GroupedSheep.Any(sg => sg.GroupId == group.Id))
                                       .ToListAsync();

            vm.SelectedSheep = await _context.GroupedHerdMemberSet
                .Where(sg => sg.GroupId == id)
                .Select(s => s.Sheep).ToListAsync();

            if (@group == null)
            {
                return NotFound();
            }
            return View(vm);
        }

        // POST: Groups/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Edit(int id, GroupEditViewModel model, int[] selectedSheepIds)
        {
            var group = await _context.GroupSet
                .Include(g => g.GroupedHerdMembers)
                    .ThenInclude(gs => gs.Group)
                .FirstOrDefaultAsync(g => g.Id == id);

            if (group == null)
                return NotFound();

            if (ModelState.IsValid)
            {
                group.GroupedHerdMembers.Clear();
                group.GroupedHerdMembers = new List<GroupedSheep>();
                foreach (int i in selectedSheepIds)
                {
                    group.GroupedHerdMembers.Add(new GroupedSheep { GroupId = id, SheepId = i });
                }

                try
                {
                    _context.Update(group);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GroupExists(@group.Id))
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
            return View(@group);
        }

        //[HttpPost]
        //public async Task<IActionResult> 

        // POST: Groups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var @group = await _context.GroupSet.FindAsync(id);
            _context.GroupSet.Remove(@group);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GroupExists(int id)
        {
            return _context.GroupSet.Any(e => e.Id == id);
        }
    }
}
