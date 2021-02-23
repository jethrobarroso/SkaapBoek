using Microsoft.EntityFrameworkCore.Migrations;

namespace SkaapBoek.DAL.Migrations
{
    public partial class PhaseTaskOrderAndConstraints : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Instructions",
                table: "mils_task",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TaskOrder",
                table: "mils_task",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TaskOrder",
                table: "mils_task");

            migrationBuilder.AlterColumn<string>(
                name: "Instructions",
                table: "mils_task",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 500);
        }
    }
}
