using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JotterService.SqliteMigrations.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Password",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: true),
                    Url = table.Column<string>(type: "TEXT", maxLength: 2000, nullable: true),
                    Username = table.Column<string>(type: "TEXT", maxLength: 320, nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    CustomerNumber = table.Column<string>(type: "TEXT", nullable: true),
                    Secret = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Password", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Password_Title",
                table: "Password",
                column: "Title");

            migrationBuilder.CreateIndex(
                name: "IX_Password_Url",
                table: "Password",
                column: "Url");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Password");
        }
    }
}
