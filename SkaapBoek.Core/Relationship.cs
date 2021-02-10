using System;
using System.Collections.Generic;
using System.Text;

namespace SkaapBoek.Core
{
    public class Relationship
    {
        public int ParentId { get; set; }
        public Sheep Parent { get; set; }

        public int ChildId { get; set; }
        public Sheep Child { get; set; }
    }
}
