using Microsoft.AspNetCore.Mvc.Rendering;
using SkaapBoek.Core;
using SkaapBoek.Web.ViewModels.Partials;
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

        public AssignGroupToPhaseModel AssignGroupToPhaseModel { get; set; }

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

        [Required]
        public int PenId { get; set; }
        public SelectList Pens { get; set; }

        /// <summary>
        /// New phase task instructions
        /// </summary>
        [Required]
        [MaxLength(500)]
        public string Instructions { get; set; }
    }
}
