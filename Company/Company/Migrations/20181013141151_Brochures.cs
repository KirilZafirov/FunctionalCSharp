using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Company.Migrations
{
    public partial class Brochures : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppBrochures",
                columns: table => new
                {
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Group = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true),
                    ImageUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppBrochures", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppBrochures");
        }
    }
}
