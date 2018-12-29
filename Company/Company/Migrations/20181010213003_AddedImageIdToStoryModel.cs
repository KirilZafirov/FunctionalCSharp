using Microsoft.EntityFrameworkCore.Migrations;

namespace Company.Migrations
{
    public partial class AddedImageIdToStoryModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ImageId",
                table: "AppStories",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "AppStories");
        }
    }
}
