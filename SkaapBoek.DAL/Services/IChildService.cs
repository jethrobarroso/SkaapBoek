using Microsoft.EntityFrameworkCore;
using SkaapBoek.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace SkaapBoek.DAL.Services
{
    public interface IChildService : IDataService<Sheep>
    {
        Task<IEnumerable<SheepStatus>> GetSheepStates();
        Task<Sheep> GetById(int id);
        Task<IEnumerable<Gender>> GetGenders();
        Task<IEnumerable<Sheep>> GetFullNoTrack();
        Task<Sheep> GetByIdNoTrack(int id);
        Task<(Sheep mother, Sheep father)> GetParentsFromChild(int childId);
    }

    public class ChildService : BaseService<Sheep>, IChildService
    {
        public ChildService(AppDbContext context) : base(context)
        {

        }

        public async Task<Sheep> GetById(int id)
        {
            var child = await Context.SheepSet
                .Include(c => c.Gender)
                .Include(c => c.SheepStatus)
                .Include(c => c.Color)
                .Include(c => c.AsParentTo)
                .FirstOrDefaultAsync(s => s.Id == id);

            return child;
        }

        public async Task<Sheep> GetByIdNoTrack(int id)
        {
            var child = await Context.SheepSet
                .Include(c => c.Gender)
                .Include(c => c.SheepStatus)
                .Include(c => c.Color)
                .Include(c => c.AsParentTo)
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == id);

            return child;
        }

        public async Task<IEnumerable<Gender>> GetGenders() => 
            await Context.GenderSet.
            OrderByDescending(g => g.Type).ToListAsync();

        public async Task<IEnumerable<SheepStatus>> GetSheepStates() =>
            await Context.SheepStateSet.ToListAsync();

        public async Task<IEnumerable<Sheep>> GetFullNoTrack()
        {
            return await base.GetAll()
                .Include(c => c.Gender)
                .Include(c => c.SheepStatus)
                .Include(c => c.Color)
                .Include(c => c.AsChildTo)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<(Sheep mother, Sheep father)> GetParentsFromChild(int childId)
        {
            var result = await Context.RelationshipSet
                .Include(r => r.Parent).ThenInclude(p => p.Gender)
                .Include(r => r.Parent).ThenInclude(p => p.SheepStatus)
                .Include(r => r.Parent).ThenInclude(p => p.Color)
                .Where(r => r.ChildId == childId)
                .Select(r => r.Parent).OrderBy(r => r.Gender.Type)
                .AsNoTracking()
                .ToListAsync();

            (Sheep mother, Sheep father) parents;
            parents.mother = result.SingleOrDefault(s => s.Gender.Type == "Female");
            parents.father = result.SingleOrDefault(s => s.Gender.Type == "Male");

            return parents;
        }
    }
}
