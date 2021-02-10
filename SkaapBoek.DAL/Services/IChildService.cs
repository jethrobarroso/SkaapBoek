using Microsoft.EntityFrameworkCore;
using SkaapBoek.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace SkaapBoek.DAL.Services
{
    public interface IChildService : IDataService<Child>
    {
        Task<IEnumerable<SheepStatus>> GetSheepStates();
        Task<Child> GetById(int id);
        Task<IEnumerable<Gender>> GetGenders();
        Task<IEnumerable<Child>> GetFullNoTrack();
        //Task<IEnumerable<Sheep>> GetAvailableSheep(Sheep currentSheep, Func<Sheep, IEnumerable<Relationship>> relationship);
        //Task<IEnumerable<Sheep>> GetSelectedSheep(Sheep currentSheep, Func<Relationship, Sheep> relationship);
    }

    public class ChildService : BaseService<Child>, IChildService
    {
        public ChildService(AppDbContext context) : base(context)
        {

        }

        public async Task<Child> GetById(int id)
        {
            var child = await Context.ChildSet
                .Include(s => s.Gender)
                .Include(s => s.SheepStatus)
                .Include(s => s.Color)
                .FirstOrDefaultAsync(s => s.Id == id);

            return child;
        }

        public async Task<IEnumerable<Gender>> GetGenders() => 
            await Context.GenderSet.
            OrderByDescending(g => g.Type).ToListAsync();

        public async Task<IEnumerable<SheepStatus>> GetSheepStates() =>
            await Context.SheepStateSet.ToListAsync();

        public async Task<IEnumerable<Child>> GetFullNoTrack()
        {
            return await base.GetAll()
                .Include(s => s.Gender)
                .Include(s => s.SheepStatus)
                .Include(s => s.Color)
                .AsNoTracking()
                .ToListAsync();
        }

        //public async Task<IEnumerable<Sheep>> GetAvailableSheep(Sheep currentSheep,
        //    Func<Sheep,IEnumerable<Relationship>> relationship)
        //{
        //    return await (from s in Context.SheepSet
        //                  join r in Context.RelationShipSet
        //                  on s.Id equals r.ChildId into jg
        //                  from g in jg.DefaultIfEmpty()
        //                  select s)
        //                  .Distinct()
        //                  .Where(s => relationship(currentSheep).Count() == 0 ||
        //                  !s.Children.Any(r => r.ChildId == currentSheep.Id))
        //                  .ToListAsync();
        //}

        //public async Task<IEnumerable<Sheep>> GetSelectedSheep(Sheep currentSheep,
        //    Func<Relationship,Sheep> relationship)
        //{
        //    return await Context.RelationShipSet
        //        .Where(r => relationship(r).Id == currentSheep.Id)
        //        .Select(r => relationship(r)).ToListAsync();
        //}
    }
}
