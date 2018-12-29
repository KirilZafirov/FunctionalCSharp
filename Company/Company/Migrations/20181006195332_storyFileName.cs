using Microsoft.EntityFrameworkCore.Migrations;

namespace Company.Migrations
{
    public partial class storyFileName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "AppStories",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileName",
                table: "AppStories");
        }
    }
}
