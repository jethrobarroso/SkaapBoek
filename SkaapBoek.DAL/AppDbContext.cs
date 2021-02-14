using Microsoft.EntityFrameworkCore;
using SkaapBoek.Core;
using System.Linq;

namespace SkaapBoek.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<HerdMember> HerdMemberSet { get; set; }
        public DbSet<Gender> GenderSet { get; set; }
        public DbSet<SheepStatus> SheepStateSet { get; set; }
        public DbSet<Feed> FeedSet { get; set; }
        public DbSet<Relationship> RelationshipSet { get; set; }
        public DbSet<Child> ChildSet { get; set; }
        public DbSet<Priority> PrioritySet { get; set; }
        public DbSet<Status> StatusSet { get; set; }
        public DbSet<Group> GroupSet { get; set; }
        public DbSet<GroupedHerdMember> GroupedHerdMemberSet { get; set; }
        public DbSet<TaskInstance> TaskInstanceSet { get; set; }
        public DbSet<TaskTemplate> TaskTemplateSet { get; set; }
        public DbSet<Color> ColorSet { get; set; }
        public DbSet<GroupedChild> GroupedChildSet { get; set; }
        public DbSet<Cage> CageSet { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            builder.Entity<Feed>().ToTable("feed");
            builder.Entity<Gender>().ToTable("gender");
            builder.Entity<Relationship>().ToTable("relationship");
            builder.Entity<HerdMember>().ToTable("herd_member");
            builder.Entity<SheepStatus>().ToTable("state");
            builder.Entity<Child>().ToTable("child");
            builder.Entity<Group>().ToTable("group");
            builder.Entity<Priority>().ToTable("priority");
            builder.Entity<GroupedHerdMember>().ToTable("grouped_herd_member");
            builder.Entity<GroupedChild>().ToTable("grouped_child");
            builder.Entity<Status>().ToTable("status");
            builder.Entity<TaskInstance>().ToTable("task_instance");
            builder.Entity<TaskTemplate>().ToTable("task_tamplate");
            builder.Entity<Color>().ToTable("color");
            builder.Entity<Cage>().ToTable("cage");

            builder.Entity<Relationship>()
                .HasKey(r => new { r.SheepId, r.ChildId });

            builder.Entity<Relationship>()
                .HasOne(r => r.Parent)
                .WithMany(s => s.Relationships)
                .HasForeignKey(r => r.SheepId);

            builder.Entity<Relationship>()
                .HasOne(r => r.Child)
                .WithMany(c => c.Relationships)
                .HasForeignKey(r => r.ChildId);

            builder.Entity<GroupedHerdMember>()
                .HasKey(sg => new { sg.HerdMemberId, sg.GroupId });

            builder.Entity<GroupedHerdMember>()
                .HasOne(sg => sg.HerdMember)
                .WithMany(s => s.GroupedHerdMembers)
                .HasForeignKey(sg => sg.HerdMemberId);

            builder.Entity<GroupedHerdMember>()
                .HasOne(sg => sg.Group)
                .WithMany(p => p.GroupedHerdMembers)
                .HasForeignKey(sg => sg.GroupId);

            builder.Entity<GroupedChild>()
                .HasKey(cg => new { cg.ChildId, cg.GroupId });

            builder.Entity<GroupedChild>()
                .HasOne(cg => cg.Child)
                .WithMany(c => c.GroupedChildren)
                .HasForeignKey(cg => cg.ChildId);

            builder.Entity<GroupedChild>()
                .HasOne(cg => cg.Group)
                .WithMany(g => g.GroupedChildren)
                .HasForeignKey(cg => cg.GroupId);

            builder.Entity<GroupedHerdMember>()
                .HasKey(hg => new { hg.HerdMemberId, hg.GroupId });

            builder.Entity<GroupedHerdMember>()
                .HasOne(hg => hg.HerdMember)
                .WithMany(c => c.GroupedHerdMembers)
                .HasForeignKey(hg => hg.HerdMemberId);

            builder.Entity<GroupedHerdMember>()
                .HasOne(hg => hg.Group)
                .WithMany(g => g.GroupedHerdMembers)
                .HasForeignKey(hg => hg.GroupId);
        }
    }
}
