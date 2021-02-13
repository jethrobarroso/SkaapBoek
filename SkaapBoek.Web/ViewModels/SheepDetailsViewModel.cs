using SkaapBoek.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkaapBoek.Web.ViewModels
{
    public class SheepDetailsViewModel
    {
        public Sheep Sheep { get; set; }
        public IEnumerable<Child> Children { get; set; }
    }
}
