using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SkaapBoek.Core
{
    public class Feed
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        [MaxLength(50)]
        public string ProductCode { get; set; }

        [MaxLength(50)]
        public string Supplier { get; set; }

        [Column(TypeName ="decimal(10,2)")]
        public decimal? CostPrice { get; set; }

        public List<HerdMember> Sheep { get; set; }
    }
}
