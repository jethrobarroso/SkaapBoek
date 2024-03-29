﻿using System;
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

        [Required]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        [Display(Name = "Priority")]
        public int PriorityId { get; set; }
        public Priority Priority { get; set; }

        [Display(Name = "Status")]
        public int StatusId { get; set; }
        public Status Status { get; set; }

        [Display(Name = "Sheep")]
        public int? SheepId { get; set; }
        public Sheep Sheep { get; set; }

        [Display(Name = "Group")]
        public int? GroupId { get; set; }
        public Group Group { get; set; }
    }
}
