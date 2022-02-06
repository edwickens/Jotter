using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JotterService.PostgresMigrations.Migrations;

public partial class Initial : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Password",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                UserId = table.Column<Guid>(type: "uuid", nullable: false),
                Title = table.Column<string>(type: "text", nullable: true),
                Url = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                Username = table.Column<string>(type: "character varying(320)", maxLength: 320, nullable: true),
                Description = table.Column<string>(type: "text", nullable: true),
                CustomerNumber = table.Column<string>(type: "text", nullable: true),
                Secret = table.Column<string>(type: "text", nullable: false)
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
