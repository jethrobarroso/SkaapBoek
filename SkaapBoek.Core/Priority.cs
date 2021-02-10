using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SkaapBoek.Core
{
    public class Priority
    {
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }

        [Required]
        [Column(TypeName = "char(6)")]
        public string Color { get; set; }

        public ICollection<TaskInstance> Tasks { get; set; }
    }
}