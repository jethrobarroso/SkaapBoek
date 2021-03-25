using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SkaapBoek.Web.ViewModels.Partials
{
    public class MoveToPhaseModel
    {
        public int GroupId { get; set; }

        [Display(Name = "Phase")]
        public int PhaseId { get; set; }

        [Display(Name = "New Start Date")]
        public DateTime StartDate { get; set; } = DateTime.Today;
        public SelectList Phases { get; set; }
    }
}
