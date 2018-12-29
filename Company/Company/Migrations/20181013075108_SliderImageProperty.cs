using Microsoft.EntityFrameworkCore.Migrations;

namespace Company.Migrations
{
    public partial class SliderImageProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "SliderImage",
                table: "AppFiles",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SliderImage",
                table: "AppFiles");
        }
    }
}
