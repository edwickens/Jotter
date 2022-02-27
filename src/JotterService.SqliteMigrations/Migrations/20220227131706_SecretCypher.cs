using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JotterService.SqliteMigrations.Migrations
{
    public partial class SecretCypher : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Secret",
                table: "Password",
                newName: "Secret_Text");

            migrationBuilder.AddColumn<byte[]>(
                name: "Secret_Iv",
                table: "Password",
                type: "BLOB",
                nullable: false,
                defaultValue: new byte[0]);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Secret_Iv",
                table: "Password");

            migrationBuilder.RenameColumn(
                name: "Secret_Text",
                table: "Password",
                newName: "Secret");
        }
    }
}
