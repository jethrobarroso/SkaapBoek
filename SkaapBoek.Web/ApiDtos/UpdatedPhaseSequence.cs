using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkaapBoek.Web.ApiDtos
{
    public class UpdatedPhaseSequence
    {
        public int OldSequence { get; set; }
        public int NewSequence { get; set; }
    }
}
