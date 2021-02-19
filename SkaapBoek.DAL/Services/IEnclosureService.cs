using Microsoft.EntityFrameworkCore;
using SkaapBoek.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkaapBoek.DAL.Services
{
    public interface IEnclosureService : IDataService<Enclosure>
    {
        Task<IEnumerable<Enclosure>> GetAllNoTrack();
        Task<IEnumerable<Group>> GetAvailableGroups();
        Task<IEnumerable<Sheep>> GetAvailableSheep();
        Task<Enclosure> GetById(int id);
        Task<Enclosure> GetByIdFullNoTrack(int id);
    }

    public class EnclosureService : BaseService<Enclosure>, IEnclosureService
    {
        public EnclosureService(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Enclosure>> GetAllNoTrack()
        {
            return await base.GetAll()
                .Include(c => c.Feed)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Enclosure> GetById(int id)
        {
            return await Context.EnclosureSet
                .Include(e => e.ContainedSheep)
                .Include(e => e.ContainedGroups)
                .Include(e => e.Feed)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<Enclosure> GetByIdFullNoTrack(int id)
        {
            return await Context.EnclosureSet
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
                .Where(g => g.EnclosureId == null)
                .AsNoTracking()
                .ToListAsync();

            return result;
        }

        public async Task<IEnumerable<Sheep>> GetAvailableSheep()
        {
            return await Context.SheepSet
                .Where(s => s.EnclosureId == null)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
