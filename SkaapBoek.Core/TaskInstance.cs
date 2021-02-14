using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SkaapBoek.Core
{
    public class TaskInstance
    {
        public int Id { get; set; }

        [MaxLength(50)]
        [Required]
        public string Name { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        [Range(0, 1000)]
        [Required]
        [Display(Name = "Duration")]
        public int? DurationDays { get; set; }

        [Required]
        [Display(Name = "Start Date")]
        public DateTime? StartDate { get; set; }

        [Display(Name = "Priority")]
        public int PriorityId { get; set; }
        public Priority Priority { get; set; }

        [Display(Name = "Status")]
        public int StatusId { get; set; }
        public Status Status { get; set; }

        [Display(Name = "Template")]
        public int TaskTemplateId { get; set; }
        public TaskTemplate TaskTemplate { get; set; }

        [Display(Name = "Sheep")]
        public int? SheepId { get; set; }
        public HerdMember Sheep { get; set; }

        [Display(Name = "Child")]
        public int? ChildId { get; set; }
        public Child Child { get; set; }

        [Display(Name = "Group")]
        public int? GroupId { get; set; }
        public Group Group { get; set; }
    }
}
