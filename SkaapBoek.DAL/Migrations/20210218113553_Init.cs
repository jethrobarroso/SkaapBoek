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
                name: "enclosure_type",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_enclosure_type", x => x.Id);
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
                name: "sheep_category",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sheep_category", x => x.Id);
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
                name: "enclosure",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    EnclosureTypeId = table.Column<int>(nullable: false),
                    FeedId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_enclosure", x => x.Id);
                    table.ForeignKey(
                        name: "FK_enclosure_enclosure_type_EnclosureTypeId",
                        column: x => x.EnclosureTypeId,
                        principalTable: "enclosure_type",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_enclosure_feed_FeedId",
                        column: x => x.FeedId,
                        principalTable: "feed",
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
                    EnclosureId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_group", x => x.Id);
                    table.ForeignKey(
                        name: "FK_group_enclosure_EnclosureId",
                        column: x => x.EnclosureId,
                        principalTable: "enclosure",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
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
                    CostPrice = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    SalePrice = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    SheepStatusId = table.Column<int>(nullable: false),
                    SheepCategoryId = table.Column<int>(nullable: false),
                    GenderId = table.Column<int>(nullable: false),
                    Weight = table.Column<float>(nullable: false),
                    FeedId = table.Column<int>(nullable: true),
                    EnclosureId = table.Column<int>(nullable: true),
                    MotherId = table.Column<int>(nullable: true),
                    FatherId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sheep", x => x.Id);
                    table.ForeignKey(
                        name: "FK_sheep_color_ColorId",
                        column: x => x.ColorId,
                        principalTable: "color",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_sheep_enclosure_EnclosureId",
                        column: x => x.EnclosureId,
                        principalTable: "enclosure",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_sheep_sheep_FatherId",
                        column: x => x.FatherId,
                        principalTable: "sheep",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_sheep_feed_FeedId",
                        column: x => x.FeedId,
                        principalTable: "feed",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_sheep_gender_GenderId",
                        column: x => x.GenderId,
                        principalTable: "gender",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_sheep_sheep_MotherId",
                        column: x => x.MotherId,
                        principalTable: "sheep",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_sheep_sheep_category_SheepCategoryId",
                        column: x => x.SheepCategoryId,
                        principalTable: "sheep_category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_sheep_state_SheepStatusId",
                        column: x => x.SheepStatusId,
                        principalTable: "state",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "grouped_herd_member",
                columns: table => new
                {
                    SheepId = table.Column<int>(nullable: false),
                    GroupId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_grouped_herd_member", x => new { x.SheepId, x.GroupId });
                    table.ForeignKey(
                        name: "FK_grouped_herd_member_group_GroupId",
                        column: x => x.GroupId,
                        principalTable: "group",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_grouped_herd_member_sheep_SheepId",
                        column: x => x.SheepId,
                        principalTable: "sheep",
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
                        onDelete: ReferentialAction.Restrict);
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
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_task_instance_task_tamplate_TaskTemplateId",
                        column: x => x.TaskTemplateId,
                        principalTable: "task_tamplate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_enclosure_EnclosureTypeId",
                table: "enclosure",
                column: "EnclosureTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_enclosure_FeedId",
                table: "enclosure",
                column: "FeedId");

            migrationBuilder.CreateIndex(
                name: "IX_group_EnclosureId",
                table: "group",
                column: "EnclosureId");

            migrationBuilder.CreateIndex(
                name: "IX_grouped_herd_member_GroupId",
                table: "grouped_herd_member",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_sheep_ColorId",
                table: "sheep",
                column: "ColorId");

            migrationBuilder.CreateIndex(
                name: "IX_sheep_EnclosureId",
                table: "sheep",
                column: "EnclosureId");

            migrationBuilder.CreateIndex(
                name: "IX_sheep_FatherId",
                table: "sheep",
                column: "FatherId",
                unique: true,
                filter: "[FatherId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_sheep_FeedId",
                table: "sheep",
                column: "FeedId");

            migrationBuilder.CreateIndex(
                name: "IX_sheep_GenderId",
                table: "sheep",
                column: "GenderId");

            migrationBuilder.CreateIndex(
                name: "IX_sheep_MotherId",
                table: "sheep",
                column: "MotherId",
                unique: true,
                filter: "[MotherId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_sheep_SheepCategoryId",
                table: "sheep",
                column: "SheepCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_sheep_SheepStatusId",
                table: "sheep",
                column: "SheepStatusId");

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
                name: "grouped_herd_member");

            migrationBuilder.DropTable(
                name: "task_instance");

            migrationBuilder.DropTable(
                name: "group");

            migrationBuilder.DropTable(
                name: "priority");

            migrationBuilder.DropTable(
                name: "sheep");

            migrationBuilder.DropTable(
                name: "status");

            migrationBuilder.DropTable(
                name: "task_tamplate");

            migrationBuilder.DropTable(
                name: "color");

            migrationBuilder.DropTable(
                name: "enclosure");

            migrationBuilder.DropTable(
                name: "gender");

            migrationBuilder.DropTable(
                name: "sheep_category");

            migrationBuilder.DropTable(
                name: "state");

            migrationBuilder.DropTable(
                name: "enclosure_type");

            migrationBuilder.DropTable(
                name: "feed");
        }
    }
}
