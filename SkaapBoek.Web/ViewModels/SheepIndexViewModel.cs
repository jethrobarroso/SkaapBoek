using SkaapBoek.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkaapBoek.Web.ViewModels
{
    public class SheepIndexViewModel
    {
        public IEnumerable<Sheep> HerdSheep { get; set; }
        public IEnumerable<Child> FeedlotSheep { get; set; }
    }
}
