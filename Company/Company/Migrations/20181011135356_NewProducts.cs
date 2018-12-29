using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Company.Migrations
{
    public partial class NewProducts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppProducts",
                columns: table => new
                {
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Lang = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Code = table.Column<string>(nullable: true),
                    CorrespondsTo = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Image = table.Column<byte[]>(nullable: true),
                    ImageId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppProducts", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppProducts");
        }
    }
}
