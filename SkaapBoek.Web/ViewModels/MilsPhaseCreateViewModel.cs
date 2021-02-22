using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SkaapBoek.Web.ViewModels
{
    public class MilsPhaseCreateViewModel
    {
        [Required]
        [Range(1, 1000)]
        public int PhaseOrder { get; set; }

        [Required]
        [MaxLength(250)]
        public string Activity { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        [Range(0, 5000)]
        public int Days { get; set; }
    }
}
