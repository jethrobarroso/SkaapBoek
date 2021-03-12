using SkaapBoek.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SkaapBoek.Web.ViewModels.Partials
{
    public class OverviewTaskTableModel
    {
        public string Name { get; set; }

        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Display(Name = "Priority")]
        public string PriorityName { get; set; }

        [Display(Name = "Status")]
        public string StatusName { get; set; }

        public IEnumerable<Sheep> Sheep { get; set; }
        public IEnumerable<Group> Groups { get; set; }
    }
}
