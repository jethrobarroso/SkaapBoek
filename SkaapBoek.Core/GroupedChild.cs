using System;
using System.Collections.Generic;
using System.Text;

namespace SkaapBoek.Core
{
    public class GroupedChild
    {
        public int ChildId { get; set; }
        public Child Child { get; set; }

        public int GroupId { get; set; }
        public Group Group { get; set; }
    }
}
