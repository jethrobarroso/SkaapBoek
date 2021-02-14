using SkaapBoek.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkaapBoek.Web.ViewModels
{
    public class ProjectEditViewModel
    {
        public List<HerdMember> AvailableSheep { get; set; }
        public List<HerdMember> SelectedSheep { get; set; }

        public List<Group> AvailableGroups { get; set; }
        public List<Group> SelectedGroups { get; set; }
    }
}
