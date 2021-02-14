using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SkaapBoek.DAL.Migrations
{
    public partial class Init : Migration
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
                name: "cage",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    ColorId = table.Column<int>(nullable: false),
                    FeedId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_cage_color_ColorId",
                        column: x => x.ColorId,
                        principalTable: "color",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_cage_feed_FeedId",
                        column: x => x.FeedId,
                        principalTable: "feed",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "child",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TagNumber = table.Column<string>(maxLength: 100, nullable: false),
                    ColorId = table.Column<int>(nullable: false),
                    BirthDate = table.Column<DateTime>(nullable: false),
                    FeedlotAssigned = table.Column<bool>(nullable: false),
                    SalePrice = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    SheepStatusId = table.Column<int>(nullable: false),
                    GenderId = table.Column<int>(nullable: false),
                    Weight = table.Column<float>(nullable: false),
                    FeedId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_child", x => x.Id);
                    table.ForeignKey(
                        name: "FK_child_color_ColorId",
                        column: x => x.ColorId,
                        principalTable: "color",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_child_feed_FeedId",
                        column: x => x.FeedId,
                        principalTable: "feed",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_child_gender_GenderId",
                        column: x => x.GenderId,
                        principalTable: "gender",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_child_state_SheepStatusId",
                        column: x => x.SheepStatusId,
                        principalTable: "state",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "group",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(maxLength: 500, nullable: true),
                    CageId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_group", x => x.Id);
                    table.ForeignKey(
                        name: "FK_group_cage_CageId",
                        column: x => x.CageId,
                        principalTable: "cage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "herd_member",
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
                    FeedId = table.Column<int>(nullable: true),
                    CageId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_herd_member", x => x.Id);
                    table.ForeignKey(
                        name: "FK_herd_member_cage_CageId",
                        column: x => x.CageId,
                        principalTable: "cage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_herd_member_color_ColorId",
                        column: x => x.ColorId,
                        principalTable: "color",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_herd_member_feed_FeedId",
                        column: x => x.FeedId,
                        principalTable: "feed",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_herd_member_gender_GenderId",
                        column: x => x.GenderId,
                        principalTable: "gender",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_herd_member_state_SheepStatusId",
                        column: x => x.SheepStatusId,
                        principalTable: "state",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "grouped_child",
                columns: table => new
                {
                    ChildId = table.Column<int>(nullable: false),
                    GroupId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_grouped_child", x => new { x.ChildId, x.GroupId });
                    table.ForeignKey(
                        name: "FK_grouped_child_child_ChildId",
                        column: x => x.ChildId,
                        principalTable: "child",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_grouped_child_group_GroupId",
                        column: x => x.GroupId,
                        principalTable: "group",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "grouped_herd_member",
                columns: table => new
                {
                    HerdMemberId = table.Column<int>(nullable: false),
                    GroupId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_grouped_herd_member", x => new { x.HerdMemberId, x.GroupId });
                    table.ForeignKey(
                        name: "FK_grouped_herd_member_group_GroupId",
                        column: x => x.GroupId,
                        principalTable: "group",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_grouped_herd_member_herd_member_HerdMemberId",
                        column: x => x.HerdMemberId,
                        principalTable: "herd_member",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "relationship",
                columns: table => new
                {
                    SheepId = table.Column<int>(nullable: false),
                    ChildId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_relationship", x => new { x.SheepId, x.ChildId });
                    table.ForeignKey(
                        name: "FK_relationship_child_ChildId",
                        column: x => x.ChildId,
                        principalTable: "child",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_relationship_herd_member_SheepId",
                        column: x => x.SheepId,
                        principalTable: "herd_member",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    ChildId = table.Column<int>(nullable: true),
                    GroupId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_task_instance", x => x.Id);
                    table.ForeignKey(
                        name: "FK_task_instance_child_ChildId",
                        column: x => x.ChildId,
                        principalTable: "child",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_task_instance_herd_member_SheepId",
                        column: x => x.SheepId,
                        principalTable: "herd_member",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_task_instance_status_StatusId",
                        column: x => x.StatusId,
                        principalTable: "status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_task_instance_task_tamplate_TaskTemplateId",
                        column: x => x.TaskTemplateId,
                        principalTable: "task_tamplate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_cage_ColorId",
                table: "cage",
                column: "ColorId");

            migrationBuilder.CreateIndex(
                name: "IX_cage_FeedId",
                table: "cage",
                column: "FeedId");

            migrationBuilder.CreateIndex(
                name: "IX_child_ColorId",
                table: "child",
                column: "ColorId");

            migrationBuilder.CreateIndex(
                name: "IX_child_FeedId",
                table: "child",
                column: "FeedId");

            migrationBuilder.CreateIndex(
                name: "IX_child_GenderId",
                table: "child",
                column: "GenderId");

            migrationBuilder.CreateIndex(
                name: "IX_child_SheepStatusId",
                table: "child",
                column: "SheepStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_group_CageId",
                table: "group",
                column: "CageId");

            migrationBuilder.CreateIndex(
                name: "IX_grouped_child_GroupId",
                table: "grouped_child",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_grouped_herd_member_GroupId",
                table: "grouped_herd_member",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_herd_member_CageId",
                table: "herd_member",
                column: "CageId");

            migrationBuilder.CreateIndex(
                name: "IX_herd_member_ColorId",
                table: "herd_member",
                column: "ColorId");

            migrationBuilder.CreateIndex(
                name: "IX_herd_member_FeedId",
                table: "herd_member",
                column: "FeedId");

            migrationBuilder.CreateIndex(
                name: "IX_herd_member_GenderId",
                table: "herd_member",
                column: "GenderId");

            migrationBuilder.CreateIndex(
                name: "IX_herd_member_SheepStatusId",
                table: "herd_member",
                column: "SheepStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_relationship_ChildId",
                table: "relationship",
                column: "ChildId");

            migrationBuilder.CreateIndex(
                name: "IX_task_instance_ChildId",
                table: "task_instance",
                column: "ChildId");

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
                name: "grouped_child");

            migrationBuilder.DropTable(
                name: "grouped_herd_member");

            migrationBuilder.DropTable(
                name: "relationship");

            migrationBuilder.DropTable(
                name: "task_instance");

            migrationBuilder.DropTable(
                name: "child");

            migrationBuilder.DropTable(
                name: "group");

            migrationBuilder.DropTable(
                name: "priority");

            migrationBuilder.DropTable(
                name: "herd_member");

            migrationBuilder.DropTable(
                name: "status");

            migrationBuilder.DropTable(
                name: "task_tamplate");

            migrationBuilder.DropTable(
                name: "cage");

            migrationBuilder.DropTable(
                name: "gender");

            migrationBuilder.DropTable(
                name: "state");

            migrationBuilder.DropTable(
                name: "color");

            migrationBuilder.DropTable(
                name: "feed");
        }
    }
}
