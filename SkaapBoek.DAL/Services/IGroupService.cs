using Microsoft.EntityFrameworkCore;
using SkaapBoek.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkaapBoek.DAL.Services
{
    public interface IGroupService : IDataService<Group>
    {
        Task<IEnumerable<Group>> GetAllNoTrack();
        Task<IList<Sheep>> GetAvailableSheep(int? groupId = null);
        Task<Group> GetByIdFull(int id, bool track = false);
        Task<ICollection<Group>> GetGroupsByIds(int[] ids);
        Task<IList<Sheep>> GetSelectedSheep(int groupId);
    }

    public class GroupService : BaseService<Group>, IGroupService
    {
        public GroupService(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Group>> GetAllNoTrack()
        {
            return await base.GetAll().AsNoTracking().ToListAsync();
        }

        public async Task<ICollection<Group>> GetGroupsByIds(int[] ids)
        {
            if (ids is null)
                return new List<Group>();

            return await Context.GroupSet
                .Where(g => ids.Contains(g.Id))
                .ToListAsync();
        }

        public async Task<IEnumerable<Sheep>> GetSheepByGroupIdNoTrack(int groupId)
        {
            return await Context.GroupedSheepSet
                .Where(g => g.GroupId == groupId)
                .Select(g => g.Sheep)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Group> GetByIdFull(int id, bool track = false)
        {
            var query = Context.GroupSet
                .Include(g => g.Pen)
                .Include(g => g.GroupedSheep)
                    .ThenInclude(gs => gs.Sheep.Gender)
                .Include(g => g.GroupedSheep)
                    .ThenInclude(gs => gs.Sheep.Color)
                .Include(g => g.GroupedSheep)
                    .ThenInclude(gs => gs.Sheep.Category)
                .Include(g => g.GroupedSheep)
                    .ThenInclude(gs => gs.Sheep.SheepStatus);

            if (!track)
            {
                return await query.AsNoTracking().FirstOrDefaultAsync(g => g.Id == id);
            }
            return await query.FirstOrDefaultAsync(g => g.Id == id);
        }

        public async Task<IList<Sheep>> GetAvailableSheep(int? groupId = null)
        {
            return await (from s in Context.SheepSet
                          join sg in Context.GroupedSheepSet
                          on s.Id equals sg.SheepId into jg
                          from g in jg.DefaultIfEmpty()
                          select s).Distinct()
                          .Where(s => s.GroupedSheep.Count() == 0 ||
                          !s.GroupedSheep.Any(sg => 
                            groupId == null ? true : sg.GroupId == groupId.Value))
                          .ToListAsync();
        }

        public async Task<IList<Sheep>> GetSelectedSheep(int groupId)
        {
            return await Context.GroupedSheepSet
                .Where(sg => sg.GroupId == groupId)
                .AsNoTracking()
                .Select(sg => sg.Sheep).ToListAsync();
        }
    }
}
