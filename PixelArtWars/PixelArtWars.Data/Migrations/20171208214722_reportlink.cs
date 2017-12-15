using Microsoft.EntityFrameworkCore.Migrations;

namespace PixelArtWars.Data.Migrations
{
    public partial class reportlink : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReportedId",
                table: "Reports");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReportedId",
                table: "Reports",
                nullable: false,
                defaultValue: 0);
        }
    }
}
