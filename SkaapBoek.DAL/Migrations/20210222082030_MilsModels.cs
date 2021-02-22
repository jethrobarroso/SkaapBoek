using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SkaapBoek.DAL.Migrations
{
    public partial class MilsModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MilsPhaseId",
                table: "group",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "mils_phase",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhaseOrder = table.Column<int>(nullable: false),
                    Activity = table.Column<string>(maxLength: 250, nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    Days = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mils_phase", x => x.Id);
                    table.UniqueConstraint("AK_mils_phase_PhaseOrder", x => x.PhaseOrder);
                });

            migrationBuilder.CreateTable(
                name: "mils_task",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Instructions = table.Column<string>(nullable: true),
                    MilsPhaseId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mils_task", x => x.Id);
                    table.ForeignKey(
                        name: "FK_mils_task_mils_phase_MilsPhaseId",
                        column: x => x.MilsPhaseId,
                        principalTable: "mils_phase",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_group_MilsPhaseId",
                table: "group",
                column: "MilsPhaseId");

            migrationBuilder.CreateIndex(
                name: "IX_mils_task_MilsPhaseId",
                table: "mils_task",
                column: "MilsPhaseId");

            migrationBuilder.AddForeignKey(
                name: "FK_group_mils_phase_MilsPhaseId",
                table: "group",
                column: "MilsPhaseId",
                principalTable: "mils_phase",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_group_mils_phase_MilsPhaseId",
                table: "group");

            migrationBuilder.DropTable(
                name: "mils_task");

            migrationBuilder.DropTable(
                name: "mils_phase");

            migrationBuilder.DropIndex(
                name: "IX_group_MilsPhaseId",
                table: "group");

            migrationBuilder.DropColumn(
                name: "MilsPhaseId",
                table: "group");
        }
    }
}
