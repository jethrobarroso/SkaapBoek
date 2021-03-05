using SkaapBoek.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkaapBoek.Web.ViewModels.Partials
{
    public class GroupTableModel
    {
        public bool DisplayMilsData { get; set; }
        public IEnumerable<Group> Groups { get; set; }
    }
}
