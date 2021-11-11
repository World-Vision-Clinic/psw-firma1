using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hospital_API.Migrations
{
    public partial class AddedDateAndUserMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "Feedbacks",
                schema: "public",
                newName: "Feedbacks");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Feedbacks",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Feedbacks",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "Feedbacks");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Feedbacks");

            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.RenameTable(
                name: "Feedbacks",
                newName: "Feedbacks",
                newSchema: "public");
        }
    }
}
