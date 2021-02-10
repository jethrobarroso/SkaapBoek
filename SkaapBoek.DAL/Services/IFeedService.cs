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
        Task<Feed> GetById(int id);
    }

    public class FeedService : BaseService<Feed>, IFeedService
    {
        public FeedService(AppDbContext context) : base(context)
        {
        }

        public async Task<Feed> GetById(int id)
        {
            return await Context.FeedSet.FindAsync(id);
        }

        public async Task<IEnumerable<Feed>> GetAllNoTrack()
        {
            return await base.GetAll().AsNoTracking().ToListAsync();
        }
    }
}
