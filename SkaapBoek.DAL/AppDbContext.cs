﻿using Microsoft.EntityFrameworkCore;
using SkaapBoek.Core;
using System.Linq;

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
        public DbSet<Priority> PrioritySet { get; set; }
        public DbSet<Status> StatusSet { get; set; }
        public DbSet<Group> GroupSet { get; set; }
        public DbSet<GroupedSheep> GroupedSheepSet { get; set; }
        public DbSet<TaskInstance> TaskInstanceSet { get; set; }
        public DbSet<TaskTemplate> TaskTemplateSet { get; set; }
        public DbSet<Color> ColorSet { get; set; }
        public DbSet<Enclosure> EnclosureSet { get; set; }
        public DbSet<EnclosureType> EnclosureTypeSet { get; set; }
        public DbSet<SheepCategory> SheepCategorySet { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            builder.Entity<Feed>().ToTable("feed");
            builder.Entity<Gender>().ToTable("gender");
            builder.Entity<Status>().ToTable("status");
            builder.Entity<Sheep>().ToTable("sheep");
            builder.Entity<SheepCategory>().ToTable("sheep_category");
            builder.Entity<SheepStatus>().ToTable("state");
            builder.Entity<Group>().ToTable("group");
            builder.Entity<GroupedSheep>().ToTable("grouped_herd_member");
            builder.Entity<Priority>().ToTable("priority");
            builder.Entity<TaskInstance>().ToTable("task_instance");
            builder.Entity<TaskTemplate>().ToTable("task_tamplate");
            builder.Entity<Color>().ToTable("color");
            builder.Entity<Enclosure>().ToTable("enclosure");
            builder.Entity<EnclosureType>().ToTable("enclosure_type");

            builder.Entity<GroupedSheep>()
                .HasKey(sg => new { sg.SheepId, sg.GroupId });

            builder.Entity<GroupedSheep>()
                .HasOne(sg => sg.Sheep)
                .WithMany(s => s.GroupedSheep)
                .HasForeignKey(sg => sg.SheepId);

            builder.Entity<GroupedSheep>()
                .HasOne(sg => sg.Group)
                .WithMany(p => p.GroupedSheep)
                .HasForeignKey(sg => sg.GroupId);

            builder.Entity<GroupedSheep>()
                .HasKey(hg => new { hg.SheepId, hg.GroupId });

            builder.Entity<GroupedSheep>()
                .HasOne(hg => hg.Sheep)
                .WithMany(c => c.GroupedSheep)
                .HasForeignKey(hg => hg.SheepId);

            builder.Entity<GroupedSheep>()
                .HasOne(hg => hg.Group)
                .WithMany(g => g.GroupedSheep)
                .HasForeignKey(hg => hg.GroupId);

            builder.Entity<Sheep>(builder =>
            {
                builder.HasOne(s => s.Enclosure)
                .WithMany(s => s.ContainedSheep)
                .OnDelete(DeleteBehavior.SetNull);

                builder.HasOne(s => s.CurrentFeed)
                .WithMany(f => f.Sheep)
                .OnDelete(DeleteBehavior.SetNull);
            });

            builder.Entity<Group>()
                .HasOne(g => g.Enclosure)
                .WithMany(e => e.ContainedGroups)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<Sheep>(builder =>
            {
                builder.HasOne(s => s.Father).WithOne().HasForeignKey<Sheep>(s => s.FatherId);
                builder.HasOne(s => s.Mother).WithOne().HasForeignKey<Sheep>(s => s.MotherId);
            });
        }
    }
}
