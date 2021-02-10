using Microsoft.EntityFrameworkCore;
using SkaapBoek.Core;

namespace SkaapBoek.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Sheep> SheepSet { get; set; }
        public DbSet<Gender> GenderSet { get; set; }
        public DbSet<SheepStatus> SheepStateSet { get; set; }
        public DbSet<Feed> FeedSet { get; set; }
        public DbSet<Relationship> RelationShipSet { get; set; }
        public DbSet<Child> ChildSet { get; set; }
        public DbSet<Parent> ParentSet { get; set; }
        public DbSet<Priority> PrioritySet { get; set; }
        public DbSet<Status> StatusSet { get; set; }
        public DbSet<Group> GroupSet { get; set; }
        public DbSet<SheepGroup> SheepGroupSet { get; set; }
        public DbSet<TaskInstance> TaskInstanceSet { get; set; }
        public DbSet<TaskTemplate> TaskTemplateSet { get; set; }
        public DbSet<Color> ColorSet { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Feed>().ToTable("feed");
            builder.Entity<Gender>().ToTable("gender");
            builder.Entity<Relationship>().ToTable("relationship");
            builder.Entity<Sheep>().ToTable("sheep");
            builder.Entity<SheepStatus>().ToTable("state");
            builder.Entity<Child>().ToTable("child");
            builder.Entity<Group>().ToTable("group");
            builder.Entity<Parent>().ToTable("parent");
            builder.Entity<Priority>().ToTable("priority");
            builder.Entity<SheepGroup>().ToTable("sheep_group");
            builder.Entity<Status>().ToTable("status");
            builder.Entity<TaskInstance>().ToTable("task_instance");
            builder.Entity<TaskTemplate>().ToTable("task_tamplate");
            builder.Entity<Color>().ToTable("color");

            builder.Entity<Relationship>()
                .HasKey(r => new { r.ChildId, r.ParentId });

            builder.Entity<Relationship>()
                .HasOne(r => r.Parent)
                .WithMany(s => s.Children)
                .HasForeignKey(r => r.ParentId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Relationship>()
                .HasOne(r => r.Child)
                .WithMany(s => s.Parents)
                .HasForeignKey(r => r.ChildId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<SheepGroup>()
                .HasKey(sg => new { sg.SheepId, sg.GroupId });

            builder.Entity<SheepGroup>()
                .HasOne(sg => sg.Sheep)
                .WithMany(s => s.SheepGroups)
                .HasForeignKey(sg => sg.SheepId);

            builder.Entity<SheepGroup>()
                .HasOne(sg => sg.Group)
                .WithMany(p => p.SheepGroups)
                .HasForeignKey(sg => sg.GroupId);
        }
    }
}
