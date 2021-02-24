using Microsoft.EntityFrameworkCore.Migrations;

namespace SkaapBoek.DAL.Migrations
{
    public partial class RenameOrderToSequence : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TaskOrder",
                table: "mils_task");

            migrationBuilder.DropColumn(
                name: "PhaseOrder",
                table: "mils_phase");

            migrationBuilder.AddColumn<int>(
                name: "TaskSequence",
                table: "mils_task",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PhaseSequence",
                table: "mils_phase",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TaskSequence",
                table: "mils_task");

            migrationBuilder.DropColumn(
                name: "PhaseSequence",
                table: "mils_phase");

            migrationBuilder.AddColumn<int>(
                name: "TaskOrder",
                table: "mils_task",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PhaseOrder",
                table: "mils_phase",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
