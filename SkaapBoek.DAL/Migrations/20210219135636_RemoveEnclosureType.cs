using Microsoft.EntityFrameworkCore.Migrations;

namespace SkaapBoek.DAL.Migrations
{
    public partial class RemoveEnclosureType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_enclosure_enclosure_type_EnclosureTypeId",
                table: "enclosure");

            migrationBuilder.DropTable(
                name: "enclosure_type");

            migrationBuilder.DropIndex(
                name: "IX_enclosure_EnclosureTypeId",
                table: "enclosure");

            migrationBuilder.DropColumn(
                name: "EnclosureTypeId",
                table: "enclosure");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EnclosureTypeId",
                table: "enclosure",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "enclosure_type",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_enclosure_type", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_enclosure_EnclosureTypeId",
                table: "enclosure",
                column: "EnclosureTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_enclosure_enclosure_type_EnclosureTypeId",
                table: "enclosure",
                column: "EnclosureTypeId",
                principalTable: "enclosure_type",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
