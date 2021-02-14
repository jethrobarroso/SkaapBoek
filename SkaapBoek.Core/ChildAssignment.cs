namespace SkaapBoek.Core
{
    public class ChildAssignment
    {
        public int? ChildId { get; set; }
        public Child Sheep { get; set; }

        public int GroupId { get; set; }
        public Group Group { get; set; }
    }
}