using Microsoft.EntityFrameworkCore;
using SkaapBoek.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkaapBoek.DAL.Services
{
    public interface IPenService : IDataService<Pen>
    {
        Task<IEnumerable<Pen>> GetAllNoTrack();
        Task<IEnumerable<Group>> GetAvailableGroups();
        Task<IEnumerable<Sheep>> GetAvailableSheep();
        Task<Pen> GetById(int id);
        Task<Pen> GetByIdFullNoTrack(int id);
        Task<IEnumerable<Pen>> GetPens(int? phaseId = null);
    }

    public class PenService : BaseService<Pen>, IPenService
    {
        public PenService(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Pen>> GetAllNoTrack()
        {
            return await base.GetAll()
                .Include(c => c.Feed)
                .Include(c => c.MilsPhase)
                .AsNoTracking()
                .ToListAsync();
        }



        public async Task<Pen> GetById(int id)
        {
            return await Context.PenSet
                .Include(e => e.ContainedSheep)
                .Include(e => e.ContainedGroups)
                .Include(e => e.Feed)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<IEnumerable<Pen>> GetPens(int? phaseId = null)
        {
            return await Context.PenSet
                .Include(p => p.MilsPhase)
                .Where(p => p.MilsPhase == null || p.MilsPhase.Id == phaseId)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Pen> GetByIdFullNoTrack(int id)
        {
            return await Context.PenSet
                .Include(e => e.ContainedSheep)
                    .ThenInclude(s => s.Color)
                .Include(e => e.ContainedSheep)
                    .ThenInclude(s => s.Gender)
                .Include(e => e.ContainedSheep)
                    .ThenInclude(s => s.SheepStatus)
                .Include(e => e.ContainedSheep)
                    .ThenInclude(s => s.Category)
                .Include(e => e.ContainedGroups)
                .Include(e => e.Feed)
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<IEnumerable<Group>> GetAvailableGroups()
        {
            var result = await Context.GroupSet
                .Where(g => g.PenId == null)
                .AsNoTracking()
                .ToListAsync();

            return result;
        }

        public async Task<IEnumerable<Sheep>> GetAvailableSheep()
        {
            return await Context.SheepSet
                .Where(s => s.PenId == null)
                .AsNoTracking()
                .ToListAsync();
        }

        public override async Task<Pen> Delete(int id)
        {
            var pen = Context.PenSet
                .Include(p => p.MilsPhase)
                .SingleOrDefault(p => p.Id == id);

            if (pen != null)
            {
                if(pen.MilsPhase is null)
                {
                    Context.PenSet.Remove(pen);
                    await Context.SaveChangesAsync();
                }
                else
                {
                    var message = "Cannot remove pen when MILS phase is bound to it.\n" +
                        "Assign phase to another pen before attempting to remove the pen.";
                    throw new DbUpdateException(message);
                }
            }

            return pen;
        }
    }
}
