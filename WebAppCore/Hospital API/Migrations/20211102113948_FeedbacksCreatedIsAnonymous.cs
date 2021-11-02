using Microsoft.EntityFrameworkCore.Migrations;

namespace Hospital_API.Migrations
{
    public partial class FeedbacksCreatedIsAnonymous : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.RenameTable(
                name: "Feedbacks",
                newName: "Feedbacks",
                newSchema: "public");

            migrationBuilder.AddColumn<bool>(
                name: "isAnonymous",
                schema: "public",
                table: "Feedbacks",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isAnonymous",
                schema: "public",
                table: "Feedbacks");

            migrationBuilder.RenameTable(
                name: "Feedbacks",
                schema: "public",
                newName: "Feedbacks");
        }
    }
}
