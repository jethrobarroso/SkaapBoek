using Microsoft.AspNetCore.Mvc.Rendering;
using SkaapBoek.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SkaapBoek.Web.ViewModels
{
    public class GroupEditViewModel
    {
        [Required]
        public string Name { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        [Display(Name = "Pen number")]
        public int? EnclosureId { get; set; }
        public SelectList Enclosures { get; set; }

        public int[] SelectedSheepIds { get; set; }
        public IList<Sheep> AvailableSheep { get; set; }
        public IList<Sheep> SelectedSheep { get; set; }
    }
}
