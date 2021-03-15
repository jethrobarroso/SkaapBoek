using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SkaapBoek.DAL.Migrations
{
    public partial class AddEndDateToTask : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DurationDays",
                table: "task_instance");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "task_instance",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "task_instance");

            migrationBuilder.AddColumn<int>(
                name: "DurationDays",
                table: "task_instance",
                type: "int",
                nullable: true);
        }
    }
}
