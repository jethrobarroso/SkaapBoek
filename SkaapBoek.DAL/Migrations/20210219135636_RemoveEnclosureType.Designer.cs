﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SkaapBoek.DAL;

namespace SkaapBoek.DAL.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20210219135636_RemoveEnclosureType")]
    partial class RemoveEnclosureType
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SkaapBoek.Core.Color", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("HexValue")
                        .IsRequired()
                        .HasColumnType("char(6)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("color");
                });

            modelBuilder.Entity("SkaapBoek.Core.Enclosure", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("FeedId")
                        .HasColumnType("int");

                    b.Property<int>("Number")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FeedId");

                    b.ToTable("enclosure");
                });

            modelBuilder.Entity("SkaapBoek.Core.Feed", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal?>("CostPrice")
                        .HasColumnType("decimal(10,2)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(500)")
                        .HasMaxLength(500);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("ProductCode")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Supplier")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("feed");
                });

            modelBuilder.Entity("SkaapBoek.Core.Gender", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("gender");
                });

            modelBuilder.Entity("SkaapBoek.Core.Group", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(500)")
                        .HasMaxLength(500);

                    b.Property<int?>("EnclosureId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("EnclosureId");

                    b.ToTable("group");
                });

            modelBuilder.Entity("SkaapBoek.Core.GroupedSheep", b =>
                {
                    b.Property<int>("SheepId")
                        .HasColumnType("int");

                    b.Property<int>("GroupId")
                        .HasColumnType("int");

                    b.HasKey("SheepId", "GroupId");

                    b.HasIndex("GroupId");

                    b.ToTable("grouped_herd_member");
                });

            modelBuilder.Entity("SkaapBoek.Core.Priority", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasColumnType("char(6)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("priority");
                });

            modelBuilder.Entity("SkaapBoek.Core.Sheep", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("AcquireDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("ColorId")
                        .HasColumnType("int");

                    b.Property<decimal?>("CostPrice")
                        .HasColumnType("decimal(10,2)");

                    b.Property<int?>("EnclosureId")
                        .HasColumnType("int");

                    b.Property<int?>("FatherId")
                        .HasColumnType("int");

                    b.Property<int?>("FeedId")
                        .HasColumnType("int");

                    b.Property<int>("GenderId")
                        .HasColumnType("int");

                    b.Property<int?>("MotherId")
                        .HasColumnType("int");

                    b.Property<decimal?>("SalePrice")
                        .HasColumnType("decimal(10,2)");

                    b.Property<int>("SheepCategoryId")
                        .HasColumnType("int");

                    b.Property<int>("SheepStatusId")
                        .HasColumnType("int");

                    b.Property<string>("TagNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<float>("Weight")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("ColorId");

                    b.HasIndex("EnclosureId");

                    b.HasIndex("FatherId");

                    b.HasIndex("FeedId");

                    b.HasIndex("GenderId");

                    b.HasIndex("MotherId");

                    b.HasIndex("SheepCategoryId");

                    b.HasIndex("SheepStatusId");

                    b.ToTable("sheep");
                });

            modelBuilder.Entity("SkaapBoek.Core.SheepCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("sheep_category");
                });

            modelBuilder.Entity("SkaapBoek.Core.SheepStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("state");
                });

            modelBuilder.Entity("SkaapBoek.Core.Status", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasColumnType("char(6)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("status");
                });

            modelBuilder.Entity("SkaapBoek.Core.TaskInstance", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(500)")
                        .HasMaxLength(500);

                    b.Property<int?>("DurationDays")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<int?>("GroupId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<int>("PriorityId")
                        .HasColumnType("int");

                    b.Property<int?>("SheepId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("StartDate")
                        .IsRequired()
                        .HasColumnType("datetime2");

                    b.Property<int>("StatusId")
                        .HasColumnType("int");

                    b.Property<int>("TaskTemplateId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.HasIndex("PriorityId");

                    b.HasIndex("SheepId");

                    b.HasIndex("StatusId");

                    b.HasIndex("TaskTemplateId");

                    b.ToTable("task_instance");
                });

            modelBuilder.Entity("SkaapBoek.Core.TaskTemplate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(500)")
                        .HasMaxLength(500);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("task_tamplate");
                });

            modelBuilder.Entity("SkaapBoek.Core.Enclosure", b =>
                {
                    b.HasOne("SkaapBoek.Core.Feed", "Feed")
                        .WithMany()
                        .HasForeignKey("FeedId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("SkaapBoek.Core.Group", b =>
                {
                    b.HasOne("SkaapBoek.Core.Enclosure", "Enclosure")
                        .WithMany("ContainedGroups")
                        .HasForeignKey("EnclosureId")
                        .OnDelete(DeleteBehavior.SetNull);
                });

            modelBuilder.Entity("SkaapBoek.Core.GroupedSheep", b =>
                {
                    b.HasOne("SkaapBoek.Core.Group", "Group")
                        .WithMany("GroupedSheep")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("SkaapBoek.Core.Sheep", "Sheep")
                        .WithMany("GroupedSheep")
                        .HasForeignKey("SheepId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("SkaapBoek.Core.Sheep", b =>
                {
                    b.HasOne("SkaapBoek.Core.Color", "Color")
                        .WithMany("Sheep")
                        .HasForeignKey("ColorId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("SkaapBoek.Core.Enclosure", "Enclosure")
                        .WithMany("ContainedSheep")
                        .HasForeignKey("EnclosureId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("SkaapBoek.Core.Sheep", "Father")
                        .WithMany("ChildrenOfFather")
                        .HasForeignKey("FatherId");

                    b.HasOne("SkaapBoek.Core.Feed", "CurrentFeed")
                        .WithMany("Sheep")
                        .HasForeignKey("FeedId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("SkaapBoek.Core.Gender", "Gender")
                        .WithMany("Sheep")
                        .HasForeignKey("GenderId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("SkaapBoek.Core.Sheep", "Mother")
                        .WithMany("ChildrenOfMother")
                        .HasForeignKey("MotherId");

                    b.HasOne("SkaapBoek.Core.SheepCategory", "Category")
                        .WithMany("Sheep")
                        .HasForeignKey("SheepCategoryId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("SkaapBoek.Core.SheepStatus", "SheepStatus")
                        .WithMany("Sheep")
                        .HasForeignKey("SheepStatusId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("SkaapBoek.Core.TaskInstance", b =>
                {
                    b.HasOne("SkaapBoek.Core.Group", "Group")
                        .WithMany("TaskInstances")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("SkaapBoek.Core.Priority", "Priority")
                        .WithMany("Tasks")
                        .HasForeignKey("PriorityId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("SkaapBoek.Core.Sheep", "Sheep")
                        .WithMany("TaskInstances")
                        .HasForeignKey("SheepId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("SkaapBoek.Core.Status", "Status")
                        .WithMany("Tasks")
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("SkaapBoek.Core.TaskTemplate", "TaskTemplate")
                        .WithMany("Tasks")
                        .HasForeignKey("TaskTemplateId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
