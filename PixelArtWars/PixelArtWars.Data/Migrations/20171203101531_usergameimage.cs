using Microsoft.EntityFrameworkCore.Migrations;

namespace PixelArtWars.Data.Migrations
{
    public partial class usergameimage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasDrawn",
                table: "GameUser",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "GameUser",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasDrawn",
                table: "GameUser");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "GameUser");
        }
    }
}
