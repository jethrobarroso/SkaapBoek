using Microsoft.AspNetCore.Mvc.Rendering;
using SkaapBoek.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SkaapBoek.Web.ViewModels
{
    public class TaskEditViewModel
    {
        public int TaskId { get; set; }

        [MaxLength(50)]
        [Required]
        public string Name { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Display(Name = "Duration (days)")]
        public int? DurationDays { get; set; }

        [Display(Name = "Priority")]
        public int PriorityId { get; set; }

        [Display(Name = "Status")]
        public int StatusId { get; set; }

        [Display(Name = "Sheep")]
        public int? SheepId { get; set; }

        [Display(Name = "Group")]
        public int? GroupId { get; set; }
        
        [Display(Name = "Assign to: ")]
        public string AssignOption { get; set; }
    }
}
