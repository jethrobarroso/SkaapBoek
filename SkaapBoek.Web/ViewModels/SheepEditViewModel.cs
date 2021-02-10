using SkaapBoek.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SkaapBoek.Web.ViewModels
{
    public class SheepEditViewModel : SheepCreateViewModel
    {
        public int[] SelectedChildSheepIds { get; set; }
        public List<Sheep> AvailableChildSheep { get; set; }

        public int[] SelectedParentSheepIds { get; set; }
        public List<Sheep> SelectedParentSheep { get; set; }
    }
}
