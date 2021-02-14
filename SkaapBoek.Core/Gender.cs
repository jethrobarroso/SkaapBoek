using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SkaapBoek.Core
{
    public class Gender
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Type { get; set; }

        public List<HerdMember> Sheep { get; set; }
    }
}