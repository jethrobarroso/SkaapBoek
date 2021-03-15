using Microsoft.EntityFrameworkCore;
using SkaapBoek.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkaapBoek.DAL.Services
{
    public interface IEventService
    {
        Task<IEnumerable<TaskInstance>> GetOverdueTasks();
        Task<IEnumerable<TaskInstance>> GetTasksDueToday();
        Task<IEnumerable<TaskInstance>> GetUpcomingTasks(int days);
    }

    public class EventService : IEventService
    {
        private readonly AppDbContext _context;

        public EventService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TaskInstance>> GetTasksDueToday()
        {
            return await _context.TaskInstanceSet
                .Include(t => t.Status)
                .Include(t => t.Sheep)
                .Include(t => t.Group)
                .Include(t => t.Priority)
                .Where(t => t.Status.Name != "Completed" 
                        && ((t.StartDate.Date == DateTime.Today && t.StatusId != 2)
                            || (t.EndDate ?? DateTime.MinValue) == DateTime.Today))
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<TaskInstance>> GetOverdueTasks()
        {
            return await _context.TaskInstanceSet
                .Include(t => t.Status)
                .Include(t => t.Sheep)
                .Include(t => t.Group)
                .Include(t => t.Priority)
                .Where(t => (t.StartDate < DateTime.Today 
                        && t.EndDate < DateTime.Today
                        && t.StatusId != 3)
                    || (t.StartDate < DateTime.Today 
                        && t.EndDate > DateTime.Today
                        && t.StatusId == 1))
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<TaskInstance>> GetUpcomingTasks(int days)
        {
            var today = DateTime.Today;
            var toDate = DateTime.Today.AddDays(days);
            return await _context.TaskInstanceSet
                .Include(t => t.Status)
                .Include(t => t.Sheep)
                .Include(t => t.Group)
                .Include(t => t.Priority)
                .AsNoTracking()
                .Select(tp => new
                {
                    Task = tp,
                    CombinedDate = (tp.EndDate ?? tp.StartDate).AddDays(days),
                })
                .Where(tp => ((tp.CombinedDate > today && tp.CombinedDate <= toDate)
                    || (tp.Task.StartDate > today && tp.Task.StartDate <= toDate))
                    && tp.Task.StatusId != 3)
                .Select(tp => tp.Task)
                .ToListAsync();
        }
    }
}
