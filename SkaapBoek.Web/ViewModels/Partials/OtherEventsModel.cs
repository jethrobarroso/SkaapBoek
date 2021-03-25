using SkaapBoek.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkaapBoek.Web.ViewModels.Partials
{
    public class OtherEventsModel
    {
        public IEnumerable<SheepEventModel> SheepEvents { get; set; }
    }
}
