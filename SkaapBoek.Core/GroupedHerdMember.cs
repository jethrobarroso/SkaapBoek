namespace SkaapBoek.Core
{
    public class GroupedHerdMember
    {
        public int HerdMemberId { get; set; }
        public HerdMember HerdMember { get; set; }

        public int GroupId { get; set; }
        public Group Group { get; set; }
    }
}