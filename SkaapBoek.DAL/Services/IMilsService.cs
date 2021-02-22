using SkaapBoek.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace SkaapBoek.DAL.Services
{
    public interface IMilsService : IDataService<MilsPhase>
    {

    }

    public class MilsService : BaseService<MilsPhase>, IMilsService
    {
        public MilsService(AppDbContext context) : base(context) { }
    }
}
