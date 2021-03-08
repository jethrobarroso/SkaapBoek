using SkaapBoek.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkaapBoek.Web.ViewModels.Partials
{
    public class GroupTableModel
    {
        public bool HideMilsData { get; set; }
        public bool HideDeleteAction { get; set; }
        public IEnumerable<Group> Groups { get; set; }
    }
}
