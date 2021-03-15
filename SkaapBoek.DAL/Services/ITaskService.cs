using Microsoft.EntityFrameworkCore;
using SkaapBoek.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SkaapBoek.DAL.Services
{
    public interface ITaskService : IDataService<TaskInstance>
    {
        Task<TaskInstance> GetByIdLite(int id, bool track = false);
        Task<IEnumerable<TaskInstance>> GetFullListNoTrack();
        Task<IEnumerable<Priority>> GetPriorities();
        Task<IEnumerable<Priority>> GetStatusList();
    }

    public class TaskService : BaseService<TaskInstance>, ITaskService
    {
        public TaskService(AppDbContext context) : base(context) { }
        
        public async Task<IEnumerable<TaskInstance>> GetFullListNoTrack()
        {
            return await base.GetAll()
                .Include(t => t.Priority)
                .Include(t => t.Status)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Priority>> GetPriorities()
        {
            return await Context.PrioritySet.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Priority>> GetStatusList()
        {
            return await Context.PrioritySet.AsNoTracking().ToListAsync();
        }

        public async Task<TaskInstance> GetByIdLite(int id, bool track = false)
        {
            var query = track 
                ? Context.TaskInstanceSet 
                : Context.TaskInstanceSet.AsNoTracking();

            return await query.SingleOrDefaultAsync(t => t.Id == id);
        }
    }
}
