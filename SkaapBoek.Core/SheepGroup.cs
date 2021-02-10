namespace SkaapBoek.Core
{
    public class SheepGroup
    {
        public int SheepId { get; set; }
        public Sheep Sheep { get; set; }

        public int GroupId { get; set; }
        public Group Group { get; set; }
    }
}