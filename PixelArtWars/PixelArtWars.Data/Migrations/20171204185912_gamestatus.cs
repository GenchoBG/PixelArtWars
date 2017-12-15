using Microsoft.EntityFrameworkCore.Migrations;

namespace PixelArtWars.Data.Migrations
{
    public partial class gamestatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Games");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Games",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Games");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Games",
                nullable: false,
                defaultValue: false);
        }
    }
}
