using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SkaapBoek.Core
{
    public class MilsPhase
    {
        public int Id { get; set; }

        [Required]
        [Range(1, 1000)]
        public int PhaseSequence { get; set; }

        [Required]
        [MaxLength(250)]
        public string Activity { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime StartDate { get; set; } = DateTime.Today;
        
        [Required]
        [Range(0,5000)]
        public int Days { get; set; }

        public ICollection<MilsTask> Tasks { get; set; }
        public ICollection<Group> Groups { get; set; }
    }
}
