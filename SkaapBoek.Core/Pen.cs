using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SkaapBoek.Core
{
    public class Pen
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }
        
        public int? FeedId { get; set; }
        public Feed Feed { get; set; }

        public MilsPhase MilsPhase { get; set; }

        public ICollection<Sheep> ContainedSheep { get; set; }
        public ICollection<Group> ContainedGroups { get; set; }
    }
}
