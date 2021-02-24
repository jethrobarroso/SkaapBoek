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
        Task<bool> ExistsWithPhaseOrder(int order);
        Task<IList<MilsPhase>> GetAllWithTasksSorted();
        Task<MilsPhase> GetById(int id, bool track = false);
        Task UpdatePhaseSequence(int oldSequence, int newSequence);
    }

    public class MilsService : BaseService<MilsPhase>, IMilsService
    {
        public MilsService(AppDbContext context) : base(context) { }

        public override async Task<MilsPhase> Add(MilsPhase newEntity)
        {
            if (Context.MilsPhaseSet.Any())
                newEntity.PhaseSequence = await Context.MilsPhaseSet.MaxAsync(p => p.PhaseSequence) + 1;
            else
                newEntity.PhaseSequence = 1;
            Context.MilsPhaseSet.Add(newEntity);
            Context.SaveChanges();
            return newEntity;
        }

        public async Task<MilsPhase> GetById(int id, bool track = false)
        {
            var result = Context.MilsPhaseSet
                .Include(m => m.Tasks);

            return track ? await result.FirstOrDefaultAsync(m => m.Id == id)
                : await result.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<IList<MilsPhase>> GetAllWithTasksSorted()
        {
            return await base.GetAll()
                .Include(m => m.Tasks)
                .OrderBy(m => m.PhaseSequence)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<bool> ExistsWithPhaseOrder(int order)
        {
            return await Context.MilsPhaseSet
                .AnyAsync(p => p.PhaseSequence == order);
        }

        public async Task UpdatePhaseSequence(int oldSequence, int newSequence)
        {
            if (oldSequence - newSequence > 0)
            {
                var shiftedElements = Context.MilsPhaseSet.Where(p =>
                    p.PhaseSequence >= newSequence &&
                    p.PhaseSequence <= oldSequence).OrderBy(p => p.PhaseSequence);

                foreach (var phase in shiftedElements)
                {
                    phase.PhaseSequence++;
                }

                var last = shiftedElements.Last();
                last.PhaseSequence = newSequence;
            }
            else
            {
                var shiftedElements = await Context.MilsPhaseSet.Where(p =>
                    p.PhaseSequence <= newSequence &&
                    p.PhaseSequence >= oldSequence).OrderBy(p => p.PhaseSequence).ToListAsync();

                foreach (var phase in shiftedElements)
                {
                    phase.PhaseSequence--;
                }

                var first = shiftedElements.First();
                first.PhaseSequence = newSequence;
            }

            await Context.SaveChangesAsync();
        }
    }
}
