using SkaapBoek.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkaapBoek.Web.ViewModels
{
    public class GroupEditViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public List<HerdMember> AvailableSheep { get; set; }
        public List<HerdMember> SelectedSheep { get; set; }
    }
}
