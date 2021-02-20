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
        Task<Sheep> GetById(int? id);
        Task<IEnumerable<Gender>> GetGenders();
        Task<IEnumerable<Sheep>> GetSheepPerGenderNoTrack(string genderName);
        Task<bool> ContainsSheep(int id);
        Task<IEnumerable<Sheep>> GetHerdMembersNoTrack();
        Task<IEnumerable<Sheep>> GetFeedLotMembersNoTrack();
        Task<List<Sheep>> GetParentChildren(int parentId, bool track = false);
        Task<ICollection<Sheep>> GetSheepByIds(int[] ids);
        Task<IEnumerable<Sheep>> GetAllSheepWithRelated(bool track = false);
        Task<Sheep> UpdateChildren(Sheep parent, int[] childIds);
    }

    public class SheepService : BaseService<Sheep>, ISheepService
    {
        public SheepService(AppDbContext context) : base(context) { }
        public async Task<IEnumerable<Sheep>> GetAllSheepWithRelated(bool track =
 false)
        {
            var result = Context.SheepSet
                .Include(s => s.Gender)
                .Include(s => s.SheepStatus)
                .Include(s => s.Color)
                .Include(s => s.Category)
                .Include(s => s.Mother)
                .Include(s => s.Father);

            if (track)
                return await result.ToListAsync();

            return await result.AsNoTracking().ToListAsync();
        }

        public async Task<Sheep> GetById(int? id)
        {
            var sheep = await Context.SheepSet
                .Include(s => s.Gender)
                .Include(s => s.SheepStatus)
                .Include(s => s.Color)
                .FirstOrDefaultAsync(s => s.Id == id);

            return sheep;
        }

        public async Task<IEnumerable<Gender>> GetGenders() =>
            await Context.GenderSet.
            OrderByDescending(g => g.Type).ToListAsync();

        public async Task<IEnumerable<SheepStatus>> GetSheepStates() =>
            await Context.SheepStateSet.ToListAsync();

        public async Task<IEnumerable<Sheep>> GetHerdMembersNoTrack()
        {
            return await Context.SheepSet
                .Include(s => s.Category)
                .Where(s => s.Category.Name == "Herd")
                .Include(s => s.Gender)
                .Include(s => s.SheepStatus)
                .Include(s => s.Color)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Sheep>> GetFeedLotMembersNoTrack()
        {
            return await Context.SheepSet
                .Include(s => s.Category)
                .Where(s => s.Category.Name == "Feedlot")
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

        public async Task<ICollection<Sheep>> GetSheepByIds(int[] ids)
        {
            if (ids is null)
                return new List<Sheep>();

            return await Context.SheepSet
                .Where(s => ids.Contains(s.Id))
                .ToListAsync();
        }

        public async Task<List<Sheep>> GetParentChildren(int parentId, bool track = false)
        {
            var result = Context.SheepSet
                .Where(s => s.MotherId == parentId || s.FatherId == parentId)
                .Include(s => s.Gender)
                .Include(s => s.SheepStatus)
                .Include(s => s.Color)
                .Include(s => s.Category);

            if (track)
                return await result.ToListAsync();

            return await result.AsNoTracking().ToListAsync();
        }

        public async Task<Sheep> UpdateChildren(Sheep parent, int[] childIds)
        {
            if (parent?.Gender is null)
                return parent;

            if (childIds is null)
                childIds = new int[0];

            var parentId = parent.Id;
            List<Sheep> sheep;

            switch (parent.Gender.Type.ToLower())
            {
                case "male":
                    sheep = await Context.SheepSet
                        .Where(s => s.FatherId == parentId || childIds.Contains(s.Id))
                        .ToListAsync();

                    if (childIds.Length == 0)
                        sheep.ForEach(a => a.FatherId = null);
                    else
                        sheep.ForEach(a => a.FatherId = childIds.Contains(a.Id) ? (int?)parentId : null);
                    break;
                case "female":
                    sheep = await Context.SheepSet
                        .Where(s => s.FatherId == parentId || childIds.Contains(s.Id))
                        .ToListAsync();

                    if (childIds.Length == 0)
                        sheep.ForEach(a => a.MotherId = null);
                    else
                        sheep.ForEach(a => a.MotherId = childIds.Contains(a.Id) ? (int?)parentId : null);
                    break;
                default:
                    return parent;
            }

            await Context.SaveChangesAsync();
            return parent;
        }
    }
}
