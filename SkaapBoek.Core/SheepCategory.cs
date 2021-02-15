using System;
using System.Collections.Generic;
using System.Text;

namespace SkaapBoek.Core
{
    public class SheepCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Sheep> Sheep { get; set; }
    }
}
