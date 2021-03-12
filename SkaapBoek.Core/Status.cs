using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SkaapBoek.Core
{
    public class Status
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }

        [Required]
        [Column(TypeName = "char(6)")]
        public string Color { get; set; }

        public ICollection<TaskInstance> Tasks { get; set; }
    }
}
