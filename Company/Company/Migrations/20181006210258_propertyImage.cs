using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Company.Migrations
{
    public partial class propertyImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "AppStories",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "AppStories");
        }
    }
}
