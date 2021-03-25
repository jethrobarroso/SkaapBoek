using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkaapBoek.Core
{
    public class MilsEventModel
    {
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public string PenName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int CurrentPhaseId { get; set; }
        public int CurrentPhaseSequence { get; set; }
        public string CurrentPhaseActivity { get; set; }
        public int? NextPhaseId { get; set; }
        public string NextPhaseActivity { get; set; }
        public IEnumerable<string> Instructions { get; set; }
    }
}
