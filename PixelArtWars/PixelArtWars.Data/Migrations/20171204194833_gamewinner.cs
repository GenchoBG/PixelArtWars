using Microsoft.EntityFrameworkCore.Migrations;

namespace PixelArtWars.Data.Migrations
{
    public partial class gamewinner : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "WinnerId",
                table: "Games",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Games_WinnerId",
                table: "Games",
                column: "WinnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_AspNetUsers_WinnerId",
                table: "Games",
                column: "WinnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_AspNetUsers_WinnerId",
                table: "Games");

            migrationBuilder.DropIndex(
                name: "IX_Games_WinnerId",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "WinnerId",
                table: "Games");
        }
    }
}
