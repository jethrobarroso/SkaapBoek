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
        public int[] SelectedChildIds { get; set; }
        public List<Sheep> SelectedChildren { get; set; }
        public List<Sheep> AvailableChildren { get; set; }
    }
}
