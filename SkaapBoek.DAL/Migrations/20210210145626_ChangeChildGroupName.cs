using Microsoft.EntityFrameworkCore.Migrations;

namespace SkaapBoek.DAL.Migrations
{
    public partial class ChangeChildGroupName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChildGroup_child_ChildId",
                table: "ChildGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_ChildGroup_group_GroupId",
                table: "ChildGroup");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChildGroup",
                table: "ChildGroup");

            migrationBuilder.RenameTable(
                name: "ChildGroup",
                newName: "child_group");

            migrationBuilder.RenameIndex(
                name: "IX_ChildGroup_GroupId",
                table: "child_group",
                newName: "IX_child_group_GroupId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_child_group",
                table: "child_group",
                columns: new[] { "ChildId", "GroupId" });

            migrationBuilder.AddForeignKey(
                name: "FK_child_group_child_ChildId",
                table: "child_group",
                column: "ChildId",
                principalTable: "child",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_child_group_group_GroupId",
                table: "child_group",
                column: "GroupId",
                principalTable: "group",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_child_group_child_ChildId",
                table: "child_group");

            migrationBuilder.DropForeignKey(
                name: "FK_child_group_group_GroupId",
                table: "child_group");

            migrationBuilder.DropPrimaryKey(
                name: "PK_child_group",
                table: "child_group");

            migrationBuilder.RenameTable(
                name: "child_group",
                newName: "ChildGroup");

            migrationBuilder.RenameIndex(
                name: "IX_child_group_GroupId",
                table: "ChildGroup",
                newName: "IX_ChildGroup_GroupId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChildGroup",
                table: "ChildGroup",
                columns: new[] { "ChildId", "GroupId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ChildGroup_child_ChildId",
                table: "ChildGroup",
                column: "ChildId",
                principalTable: "child",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ChildGroup_group_GroupId",
                table: "ChildGroup",
                column: "GroupId",
                principalTable: "group",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
