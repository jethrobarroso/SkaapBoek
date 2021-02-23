﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SkaapBoek.Core
{
    public class MilsTask
    {
        public int Id { get; set; }

        [Required]
        [Range(1,100)]
        public int TaskOrder { get; set; }

        [Required]
        [MaxLength(500)]
        public string Instructions { get; set; }

        public int MilsPhaseId { get; set; }
        public MilsPhase MilsPhase { get; set; }
    }
}
