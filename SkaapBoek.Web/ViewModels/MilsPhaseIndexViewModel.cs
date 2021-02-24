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
        public DateTime StartDate { get; set; } = DateTime.Today;

        [Required]
        [Range(0, 5000)]
        public int Days { get; set; }

        public int OldSequence { get; set; }
        public int NewSequence { get; set; }
    }
}
