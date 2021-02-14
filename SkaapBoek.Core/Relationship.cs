using System;
using System.Collections.Generic;
using System.Text;

namespace SkaapBoek.Core
{
    public class Relationship
    {
        public int SheepId { get; set; }
        public HerdMember Parent { get; set; }

        public int ChildId { get; set; }
        public Child Child { get; set; }
    }
}
