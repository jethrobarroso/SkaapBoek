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
        [MaxLength(50)]
        [Required]
        public string Name { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        [DataType(DataType.Date)]
        public DateTime? StartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }

        public int SelectedPriority { get; set; }
        public List<SelectListItem> Priorities { get; set; }

        public int SelectedStatus { get; set; }
        public List<SelectListItem> StatusList { get; set; }
    }
}
