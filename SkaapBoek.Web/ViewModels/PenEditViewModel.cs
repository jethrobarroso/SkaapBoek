using Microsoft.AspNetCore.Mvc.Rendering;
using SkaapBoek.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SkaapBoek.Web.ViewModels
{
    public class PenEditViewModel
    {
        [Required]
        [Range(0,10000)]
        public int Number { get; set; }

        [Display(Name ="Assigned Feed")]
        public int? FeedId { get; set; }
        public SelectList Feed { get; set; }

        public string Description { get; set; }

        public int[] GroupIds { get; set; }
        public int[] SheepIds { get; set; }

        public IEnumerable<Group> AvailableGroups { get; set; }
        public IEnumerable<Sheep> AvailableSheep { get; set; }

        public IList<Group> SelectedGroups { get; set; }
        public IList<Sheep> SelectedSheep { get; set; }
    }
}
