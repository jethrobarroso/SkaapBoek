using Microsoft.AspNetCore.Mvc.Rendering;
using SkaapBoek.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SkaapBoek.Web.ViewModels
{
    public class MilsPhaseIndexViewModel
    {
        public IList<MilsPhase> PhaseList { get; set; }

        /// <summary>
        /// New phase activity
        /// </summary>
        [Required]
        [MaxLength(250)]
        public string Activity { get; set; }

        /// <summary>
        /// New phase duration in days
        /// </summary>
        [Required]
        [Range(0, 5000)]
        [Display(Name ="Duration in days")]
        public int Days { get; set; }

        /// <summary>
        /// New phase task instructions
        /// </summary>
        [Required]
        [MaxLength(500)]
        public string Instructions { get; set; }

        /// <summary>
        /// Assigned group selected ID
        /// </summary>
        [Required]
        public int SelectedGroupId { get; set; }

        /// <summary>
        /// Phase ID that the group will be assigned to
        /// </summary>
        public int MilsPhaseId { get; set; }

        /// <summary>
        /// Assigned group start date
        /// </summary>
        public DateTime? PhaseStartDate { get; set; } = DateTime.Today;

        /// <summary>
        /// Available groups that can be assigned to phase
        /// </summary>
        public SelectList AvailableGroups { get; set; }
    }
}
