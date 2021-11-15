using Microsoft.EntityFrameworkCore.Migrations;

namespace Hospital_API.Migrations
{
    public partial class AddedPublishableMigration2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isPublishable",
                table: "Feedbacks",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isPublishable",
                table: "Feedbacks");
        }
    }
}
