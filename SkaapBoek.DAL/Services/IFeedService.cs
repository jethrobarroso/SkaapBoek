using Microsoft.EntityFrameworkCore;
using SkaapBoek.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SkaapBoek.DAL.Services
{
    public interface IFeedService : IDataService<Feed>
    {
        Task<IEnumerable<Feed>> GetAllNoTrack();
        Task<Feed> GetById(int id, bool track = false);
    }

    public class FeedService : BaseService<Feed>, IFeedService
    {
        public FeedService(AppDbContext context) : base(context)
        {
        }

        public async Task<Feed> GetById(int id, bool track = false)
        {
            var result = Context.FeedSet
                .Include(f => f.Pens)
                .Include(f => f.Sheep)
                    .ThenInclude(s => s.Gender)
                .Include(f => f.Sheep)
                    .ThenInclude(s => s.Category)
                .Include(f => f.Sheep)
                    .ThenInclude(s => s.SheepStatus)
                .Include(f => f.Sheep)
                    .ThenInclude(s => s.Color)
                .Include(f => f.Sheep)
                    .ThenInclude(s => s.Father)
                .Include(f => f.Sheep)
                    .ThenInclude(s => s.Mother)
                .Include(f => f.Sheep)
                    .ThenInclude(s => s.Pen);

            if (track)
                return await result.SingleOrDefaultAsync(f => f.Id == id);

            return await result.AsNoTracking().SingleOrDefaultAsync(f => f.Id == id);
        }

        public async Task<IEnumerable<Feed>> GetAllNoTrack()
        {
            return await base.GetAll().AsNoTracking().ToListAsync();
        }
    }
}
