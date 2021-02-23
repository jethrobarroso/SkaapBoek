using Microsoft.EntityFrameworkCore.Migrations;

namespace SkaapBoek.DAL.Migrations
{
    public partial class RemoveOrderUniqueness : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_mils_task_TaskOrder",
                table: "mils_task");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_mils_phase_PhaseOrder",
                table: "mils_phase");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddUniqueConstraint(
                name: "AK_mils_task_TaskOrder",
                table: "mils_task",
                column: "TaskOrder");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_mils_phase_PhaseOrder",
                table: "mils_phase",
                column: "PhaseOrder");
        }
    }
}
