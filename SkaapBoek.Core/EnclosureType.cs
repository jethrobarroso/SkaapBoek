using System;
using System.Collections.Generic;
using System.Text;

namespace SkaapBoek.Core
{
    public class EnclosureType
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Enclosure> Enclosures { get; set; }
    }
}
