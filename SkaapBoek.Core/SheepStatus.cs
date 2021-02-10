using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SkaapBoek.Core
{
    public class SheepStatus
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<Sheep> Sheep { get; set; }
    }
}
