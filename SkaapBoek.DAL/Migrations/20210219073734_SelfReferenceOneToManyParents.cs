using Microsoft.EntityFrameworkCore.Migrations;

namespace SkaapBoek.DAL.Migrations
{
    public partial class SelfReferenceOneToManyParents : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_sheep_FatherId",
                table: "sheep");

            migrationBuilder.DropIndex(
                name: "IX_sheep_MotherId",
                table: "sheep");

            migrationBuilder.CreateIndex(
                name: "IX_sheep_FatherId",
                table: "sheep",
                column: "FatherId");

            migrationBuilder.CreateIndex(
                name: "IX_sheep_MotherId",
                table: "sheep",
                column: "MotherId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_sheep_FatherId",
                table: "sheep");

            migrationBuilder.DropIndex(
                name: "IX_sheep_MotherId",
                table: "sheep");

            migrationBuilder.CreateIndex(
                name: "IX_sheep_FatherId",
                table: "sheep",
                column: "FatherId",
                unique: true,
                filter: "[FatherId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_sheep_MotherId",
                table: "sheep",
                column: "MotherId",
                unique: true,
                filter: "[MotherId] IS NOT NULL");
        }
    }
}
