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
        Task<List<Sheep>> GetAvailableChildren(Sheep currentSheep);
        Task<List<Sheep>> GetSelectedChildren(Sheep currentSheep);
        Task<IEnumerable<Sheep>> GetHerdMembersNoTrack();
        Task<IEnumerable<Sheep>> GetFeedLotMembersNoTrack();
        Task<List<Sheep>> GetAllChildrenNoTrack(Sheep currentSheep);
        Task<ICollection<Sheep>> GetSheepByIds(int[] ids);
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

        public Task<List<Sheep>> GetAvailableChildren(Sheep currentSheep)
        {
            return null;
        }

        public Task<List<Sheep>> GetSelectedChildren(Sheep currentSheep)
        {
            return null;
        }

        public Task<List<Sheep>> GetAllChildrenNoTrack(Sheep currentSheep)
        {
            return null;
        }
    }
}
