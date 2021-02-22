using Microsoft.EntityFrameworkCore;
using SkaapBoek.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkaapBoek.DAL.Services
{
    public interface IMilsService : IDataService<MilsPhase>
    {
        Task<IEnumerable<MilsPhase>> GetAllWithTasksSorted();
        Task<MilsPhase> GetById(int id, bool track = false);
    }

    public class MilsService : BaseService<MilsPhase>, IMilsService
    {
        public MilsService(AppDbContext context) : base(context) { }
        
        public async Task<MilsPhase> GetById(int id, bool track = false)
        {
            var result = Context.MilsPhaseSet
                .Include(m => m.Tasks);

            return track ? await result.FirstOrDefaultAsync(m => m.Id == id)
                : await result.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<IEnumerable<MilsPhase>> GetAllWithTasksSorted()
        {
            return await base.GetAll()
                .Include(m => m.Tasks)
                .OrderBy(m => m.PhaseOrder)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
