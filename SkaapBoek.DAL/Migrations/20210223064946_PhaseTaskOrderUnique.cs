using Microsoft.EntityFrameworkCore.Migrations;

namespace SkaapBoek.DAL.Migrations
{
    public partial class PhaseTaskOrderUnique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddUniqueConstraint(
                name: "AK_mils_task_TaskOrder",
                table: "mils_task",
                column: "TaskOrder");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_mils_task_TaskOrder",
                table: "mils_task");
        }
    }
}
