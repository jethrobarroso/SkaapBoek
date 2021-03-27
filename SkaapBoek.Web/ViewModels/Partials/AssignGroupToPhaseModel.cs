using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SkaapBoek.Web.ViewModels.Partials
{
    public class AssignGroupToPhaseModel
    {
        public int MilsPhaseId { get; set; }

        public int PenId { get; set; }

        [Required]
        [Display(Name = "Group")]
        public int GroupId { get; set; }

        public SelectList AvailableGroups { get; set; }

        [Display(Name = "Start Date")]
        public DateTime? PhaseStartDate { get; set; } = DateTime.Today;
    }
}
