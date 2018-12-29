using Microsoft.EntityFrameworkCore.Migrations;

namespace Company.Migrations
{
    public partial class StoryHeading : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Heading",
                table: "AppStories",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Heading",
                table: "AppStories");
        }
    }
}
