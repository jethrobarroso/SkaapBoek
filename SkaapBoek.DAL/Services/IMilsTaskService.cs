using SkaapBoek.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkaapBoek.DAL.Services
{
    public interface IMilsTaskService : IDataService<MilsTask>
    {
        Task<MilsTask> GetById(int? id);
    }

    public class MilsTaskService : BaseService<MilsTask>, IMilsTaskService
    {
        public MilsTaskService(AppDbContext context) : base(context) { }

        public async Task<MilsTask> GetById(int? id)
        {
            return await Context.MilsTaskSet.FindAsync(id);
        }
    }
}
