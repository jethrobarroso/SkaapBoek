using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SkaapBoek.Core
{
    public class Child
    {
        [Key]
        public int SheepId { get; set; }
        public Sheep Sheep { get; set; }

        public ICollection<Relationship> ChildRelationships { get; set; }
        public ICollection<Relationship> ParentRelationships { get; set; }
    }
}
