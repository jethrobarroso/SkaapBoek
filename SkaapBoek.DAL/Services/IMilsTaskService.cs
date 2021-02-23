using SkaapBoek.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SkaapBoek.DAL.Services
{
    public interface IMilsTaskService : IDataService<MilsTask>
    {
    }

    public class MilsTaskService : BaseService<MilsTask>, IMilsTaskService
    {
        public MilsTaskService(AppDbContext context) : base(context) { }
    }
}
