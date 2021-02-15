using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SkaapBoek.Core
{
    public class Sheep
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string TagNumber { get; set; }

        public int ColorId { get; set; }
        public Color Color { get; set; }

        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime AcquireDate { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal? CostPrice { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal? SalePrice { get; set; }

        public int SheepStatusId { get; set; } = 3;
        public SheepStatus SheepStatus { get; set; }

        public int SheepCategoryId { get; set; }
        public SheepCategory Category { get; set; }

        [Required]
        public int GenderId { get; set; }
        public Gender Gender { get; set; }

        [Required]
        public float Weight { get; set; }

        public int? FeedId { get; set; }
        public Feed CurrentFeed { get; set; }

        public int? CageId { get; set; }
        public Cage Cage { get; set; }

        public ICollection<GroupedSheep> GroupedSheep { get; set; }
        public ICollection<Relationship> AsParentTo { get; set; }
        public ICollection<Relationship> AsChildTo { get; set; }
        public ICollection<TaskInstance> TaskInstances { get; set; }
    }
}
