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

        [Required]
        [MaxLength(250)]
        public string Activity { get; set; }

        [Required]
        [Range(0, 5000)]
        [Display(Name ="Duration in days")]
        public int Days { get; set; }
    }
}
