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
        public Sheep Mother { get; set; }
        public Sheep Father { get; set; }
        public IEnumerable<Sheep> Children { get; set; }
        public IEnumerable<TaskInstance> Tasks { get; set; }
    }
}
