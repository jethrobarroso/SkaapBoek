using System;
using System.Collections.Generic;
using System.Text;

namespace SkaapBoek.Core
{
    public class Cage
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public string Description { get; set; }

        public int ColorId { get; set; }
        public Color Color { get; set; }
        
        public int FeedId { get; set; }
        public Feed Feed { get; set; }

        public ICollection<Sheep> Groups { get; set; }
    }
}
