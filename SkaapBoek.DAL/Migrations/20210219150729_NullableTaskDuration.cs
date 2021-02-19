using Microsoft.EntityFrameworkCore.Migrations;

namespace SkaapBoek.DAL.Migrations
{
    public partial class NullableTaskDuration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_task_instance_task_tamplate_TaskTemplateId",
                table: "task_instance");

            migrationBuilder.DropTable(
                name: "task_tamplate");

            migrationBuilder.DropIndex(
                name: "IX_task_instance_TaskTemplateId",
                table: "task_instance");

            migrationBuilder.DropColumn(
                name: "TaskTemplateId",
                table: "task_instance");

            migrationBuilder.AlterColumn<int>(
                name: "DurationDays",
                table: "task_instance",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "DurationDays",
                table: "task_instance",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TaskTemplateId",
                table: "task_instance",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "task_tamplate",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_task_tamplate", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_task_instance_TaskTemplateId",
                table: "task_instance",
                column: "TaskTemplateId");

            migrationBuilder.AddForeignKey(
                name: "FK_task_instance_task_tamplate_TaskTemplateId",
                table: "task_instance",
                column: "TaskTemplateId",
                principalTable: "task_tamplate",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
