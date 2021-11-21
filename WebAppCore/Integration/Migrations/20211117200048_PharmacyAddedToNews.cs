using Microsoft.EntityFrameworkCore.Migrations;

namespace Integration.Migrations
{
    public partial class PharmacyAddedToNews : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PharmacyName",
                table: "News",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PharmacyName",
                table: "News");
        }
    }
}
