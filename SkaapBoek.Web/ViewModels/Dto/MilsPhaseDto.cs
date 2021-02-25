using Microsoft.AspNetCore.Mvc.Rendering;
using SkaapBoek.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SkaapBoek.Web.ViewModels
{
    public class MilsPhaseDto
    {
        [Required]
        [MaxLength(250)]
        public string Activity { get; set; }

        [Required]
        [Range(0, 5000)]
        public int Days { get; set; }

        public IList<MilsTask> Tasks { get; set; }
    }
}
