using Microsoft.EntityFrameworkCore.Migrations;

namespace SkaapBoek.DAL.Migrations
{
    public partial class PenFeedSetNullOnDelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_pen_feed_FeedId",
                table: "pen");

            migrationBuilder.AddForeignKey(
                name: "FK_pen_feed_FeedId",
                table: "pen",
                column: "FeedId",
                principalTable: "feed",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_pen_feed_FeedId",
                table: "pen");

            migrationBuilder.AddForeignKey(
                name: "FK_pen_feed_FeedId",
                table: "pen",
                column: "FeedId",
                principalTable: "feed",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
