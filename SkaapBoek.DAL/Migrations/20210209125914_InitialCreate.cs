﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SkaapBoek.DAL.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "color",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    HexValue = table.Column<string>(type: "char(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_color", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "feed",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Description = table.Column<string>(maxLength: 500, nullable: true),
                    ProductCode = table.Column<string>(maxLength: 50, nullable: true),
                    Supplier = table.Column<string>(maxLength: 50, nullable: true),
                    CostPrice = table.Column<decimal>(type: "decimal(10,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_feed", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "gender",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_gender", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "group",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_group", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "priority",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Color = table.Column<string>(type: "char(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_priority", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "state",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_state", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "status",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Color = table.Column<string>(type: "char(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_status", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "task_tamplate",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_task_tamplate", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "sheep",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TagNumber = table.Column<string>(maxLength: 100, nullable: false),
                    ColorId = table.Column<int>(nullable: false),
                    BirthDate = table.Column<DateTime>(nullable: false),
                    AcquireDate = table.Column<DateTime>(nullable: false),
                    CostPrice = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    SalePrice = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    SheepStatusId = table.Column<int>(nullable: false),
                    GenderId = table.Column<int>(nullable: false),
                    Weight = table.Column<float>(nullable: false),
                    FeedId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sheep", x => x.Id);
                    table.ForeignKey(
                        name: "FK_sheep_color_ColorId",
                        column: x => x.ColorId,
                        principalTable: "color",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_sheep_feed_FeedId",
                        column: x => x.FeedId,
                        principalTable: "feed",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_sheep_gender_GenderId",
                        column: x => x.GenderId,
                        principalTable: "gender",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_sheep_state_SheepStatusId",
                        column: x => x.SheepStatusId,
                        principalTable: "state",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "child",
                columns: table => new
                {
                    SheepId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SheepId1 = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_child", x => x.SheepId);
                    table.ForeignKey(
                        name: "FK_child_sheep_SheepId1",
                        column: x => x.SheepId1,
                        principalTable: "sheep",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "parent",
                columns: table => new
                {
                    SheepId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SheepId1 = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_parent", x => x.SheepId);
                    table.ForeignKey(
                        name: "FK_parent_sheep_SheepId1",
                        column: x => x.SheepId1,
                        principalTable: "sheep",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "sheep_group",
                columns: table => new
                {
                    SheepId = table.Column<int>(nullable: false),
                    GroupId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sheep_group", x => new { x.SheepId, x.GroupId });
                    table.ForeignKey(
                        name: "FK_sheep_group_group_GroupId",
                        column: x => x.GroupId,
                        principalTable: "group",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_sheep_group_sheep_SheepId",
                        column: x => x.SheepId,
                        principalTable: "sheep",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "task_instance",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Description = table.Column<string>(maxLength: 500, nullable: true),
                    DurationDays = table.Column<int>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    PriorityId = table.Column<int>(nullable: false),
                    StatusId = table.Column<int>(nullable: false),
                    TaskTemplateId = table.Column<int>(nullable: false),
                    SheepId = table.Column<int>(nullable: true),
                    GroupId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_task_instance", x => x.Id);
                    table.ForeignKey(
                        name: "FK_task_instance_group_GroupId",
                        column: x => x.GroupId,
                        principalTable: "group",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_task_instance_priority_PriorityId",
                        column: x => x.PriorityId,
                        principalTable: "priority",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_task_instance_sheep_SheepId",
                        column: x => x.SheepId,
                        principalTable: "sheep",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_task_instance_status_StatusId",
                        column: x => x.StatusId,
                        principalTable: "status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_task_instance_task_tamplate_TaskTemplateId",
                        column: x => x.TaskTemplateId,
                        principalTable: "task_tamplate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "relationship",
                columns: table => new
                {
                    ParentId = table.Column<int>(nullable: false),
                    ChildId = table.Column<int>(nullable: false),
                    ChildSheepId = table.Column<int>(nullable: true),
                    ChildSheepId1 = table.Column<int>(nullable: false),
                    ParentSheepId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_relationship", x => new { x.ChildId, x.ParentId });
                    table.ForeignKey(
                        name: "FK_relationship_sheep_ChildId",
                        column: x => x.ChildId,
                        principalTable: "sheep",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_relationship_child_ChildSheepId",
                        column: x => x.ChildSheepId,
                        principalTable: "child",
                        principalColumn: "SheepId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_relationship_child_ChildSheepId1",
                        column: x => x.ChildSheepId1,
                        principalTable: "child",
                        principalColumn: "SheepId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_relationship_sheep_ParentId",
                        column: x => x.ParentId,
                        principalTable: "sheep",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_relationship_parent_ParentSheepId",
                        column: x => x.ParentSheepId,
                        principalTable: "parent",
                        principalColumn: "SheepId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_child_SheepId1",
                table: "child",
                column: "SheepId1");

            migrationBuilder.CreateIndex(
                name: "IX_parent_SheepId1",
                table: "parent",
                column: "SheepId1");

            migrationBuilder.CreateIndex(
                name: "IX_relationship_ChildSheepId",
                table: "relationship",
                column: "ChildSheepId");

            migrationBuilder.CreateIndex(
                name: "IX_relationship_ChildSheepId1",
                table: "relationship",
                column: "ChildSheepId1");

            migrationBuilder.CreateIndex(
                name: "IX_relationship_ParentId",
                table: "relationship",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_relationship_ParentSheepId",
                table: "relationship",
                column: "ParentSheepId");

            migrationBuilder.CreateIndex(
                name: "IX_sheep_ColorId",
                table: "sheep",
                column: "ColorId");

            migrationBuilder.CreateIndex(
                name: "IX_sheep_FeedId",
                table: "sheep",
                column: "FeedId");

            migrationBuilder.CreateIndex(
                name: "IX_sheep_GenderId",
                table: "sheep",
                column: "GenderId");

            migrationBuilder.CreateIndex(
                name: "IX_sheep_SheepStatusId",
                table: "sheep",
                column: "SheepStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_sheep_group_GroupId",
                table: "sheep_group",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_task_instance_GroupId",
                table: "task_instance",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_task_instance_PriorityId",
                table: "task_instance",
                column: "PriorityId");

            migrationBuilder.CreateIndex(
                name: "IX_task_instance_SheepId",
                table: "task_instance",
                column: "SheepId");

            migrationBuilder.CreateIndex(
                name: "IX_task_instance_StatusId",
                table: "task_instance",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_task_instance_TaskTemplateId",
                table: "task_instance",
                column: "TaskTemplateId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "relationship");

            migrationBuilder.DropTable(
                name: "sheep_group");

            migrationBuilder.DropTable(
                name: "task_instance");

            migrationBuilder.DropTable(
                name: "child");

            migrationBuilder.DropTable(
                name: "parent");

            migrationBuilder.DropTable(
                name: "group");

            migrationBuilder.DropTable(
                name: "priority");

            migrationBuilder.DropTable(
                name: "status");

            migrationBuilder.DropTable(
                name: "task_tamplate");

            migrationBuilder.DropTable(
                name: "sheep");

            migrationBuilder.DropTable(
                name: "color");

            migrationBuilder.DropTable(
                name: "feed");

            migrationBuilder.DropTable(
                name: "gender");

            migrationBuilder.DropTable(
                name: "state");
        }
    }
}
