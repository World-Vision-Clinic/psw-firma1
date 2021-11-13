using Microsoft.EntityFrameworkCore.Migrations;

namespace Integration.Migrations
{
    public partial class PharmacyProfileUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Pharmacies",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Pharmacies",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Pharmacies");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Pharmacies");
        }
    }
}
