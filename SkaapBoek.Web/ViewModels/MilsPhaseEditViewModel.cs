using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SkaapBoek.Web.ViewModels
{
    public class MilsPhaseEditViewModel : MilsPhaseDto
    {
        [Required]
        [Range(1, 100)]
        public int TaskOrder { get; set; }

        [Required]
        [MaxLength(500)]
        public string Instructions { get; set; }

        public int MilsPhaseId { get; set; }
    }
}
