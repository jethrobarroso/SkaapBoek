using Microsoft.EntityFrameworkCore;
using SkaapBoek.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace SkaapBoek.DAL.Services
{
    public interface ISheepService : IDataService<Sheep>
    {
        Task<IEnumerable<SheepStatus>> GetSheepStates();
        Task<Sheep> GetById(int id);
        Task<IEnumerable<Gender>> GetGenders();
        Task<IEnumerable<Sheep>> GetFullNoTrack();
        Task<IEnumerable<Sheep>> GetSheepPerGenderNoTrack(string genderName);
        Task<bool> ContainsSheep(int id);
        Task<Sheep> GetParentWithChildrenNoTrack(int id);
        Task<List<Child>> GetAvailableChildren(Sheep currentSheep);
        Task<List<Child>> GetSelectedChildren(Sheep currentSheep);
    }

    public class SheepService : BaseService<Sheep>, ISheepService
    {
        public SheepService(AppDbContext context) : base(context)
        {

        }

        public async Task<Sheep> GetById(int id)
        {
            var sheep = await Context.SheepSet
                .Include(s => s.Gender)
                .Include(s => s.SheepStatus)
                .Include(s => s.Color)
                .Include(s => s.Relationships)
                .FirstOrDefaultAsync(s => s.Id == id);

            return sheep;
        }

        public async Task<IEnumerable<Gender>> GetGenders() => 
            await Context.GenderSet.
            OrderByDescending(g => g.Type).ToListAsync();

        public async Task<IEnumerable<SheepStatus>> GetSheepStates() =>
            await Context.SheepStateSet.ToListAsync();

        public async Task<IEnumerable<Sheep>> GetFullNoTrack()
        {
            return await base.GetAll()
                .Include(s => s.Gender)
                .Include(s => s.SheepStatus)
                .Include(s => s.Color)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Sheep>> GetSheepPerGenderNoTrack(string genderName)
        {
            return await Context.SheepSet
                .Include(s => s.Gender)
                .Where(s => s.Gender.Type == genderName)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<bool> ContainsSheep(int id)
        {
            var sheep = await Context.SheepSet
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == id);
            return sheep != null;
        }

        public async Task<Sheep> GetParentWithChildrenNoTrack(int id)
        {
            return await Context.SheepSet
                .Include(s => s.Gender)
                .Include(s => s.SheepStatus)
                .Include(s => s.Color)
                .Include(s => s.Relationships)
                    .ThenInclude(r => r.Child.Gender)
                .Include(s => s.Relationships)
                    .ThenInclude(r => r.Child.Color)
                .Include(s => s.Relationships)
                    .ThenInclude(r => r.Child.SheepStatus)
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<List<Child>> GetAvailableChildren(Sheep currentSheep)
        {
            var parentGender = currentSheep.Gender.Type;
            return await (from c in Context.ChildSet
                          where !c.Relationships.Any(r => r.Parent.Gender.Type == parentGender)
                          select c).ToListAsync();

        }

        public async Task<List<Child>> GetSelectedChildren(Sheep currentSheep)
        {
            return await Context.RelationshipSet
                .Where(r => r.SheepId == currentSheep.Id)
                .Select(r => r.Child).ToListAsync();
        }
    }
}
