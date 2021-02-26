using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SkaapBoek.Core
{
    public class Group
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        public int? PenId { get; set; }
        public Pen Pen { get; set; }

        public DateTime? PhaseStartDate { get; set; }

        public int? MilsPhaseId { get; set; }
        public MilsPhase MilsPhase { get; set; }

        public ICollection<GroupedSheep> GroupedSheep { get; set; }
        public ICollection<TaskInstance> TaskInstances { get; set; }
    }
}
