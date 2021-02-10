using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SkaapBoek.Core
{
    [Display(Name = "Template")]
    public class TaskTemplate
    {
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }

        [Required]
        [MaxLength(500)]
        public string Description { get; set; }

        public ICollection<TaskInstance> Tasks { get; set; }
    }
}
