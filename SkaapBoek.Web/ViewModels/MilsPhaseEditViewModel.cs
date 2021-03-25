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
    public class MilsPhaseEditViewModel : MilsPhaseDto
    {
        [Required]
        [Range(1, 100)]
        public int TaskSequence { get; set; }

        public int MilsTaskId { get; set; }

        [Required]
        [MaxLength(500)]
        public string Instructions { get; set; }

        public int MilsPhaseId { get; set; }

        [Required]
        public int GroupId { get; set; }
        public SelectList GroupList { get; set; }

        [Required]
        public DateTime PhaseStartDate { get; set; }

        public AssignGroupToPhaseModel AssignGroupModel { get; set; }
    }
}
