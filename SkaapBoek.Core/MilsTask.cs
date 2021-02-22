using System;
using System.Collections.Generic;
using System.Text;

namespace SkaapBoek.Core
{
    public class MilsTask
    {
        public int Id { get; set; }

        public string Instructions { get; set; }

        public int MilsPhaseId { get; set; }
        public MilsPhase MilsPhase { get; set; }
    }
}
