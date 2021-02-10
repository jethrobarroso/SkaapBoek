﻿using System;
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

        public ICollection<SheepGroup> SheepGroups { get; set; }
        public ICollection<TaskInstance> TaskInstances { get; set; }
        public ICollection<ChildGroup> ChildGroups { get; set; }
    }
}
