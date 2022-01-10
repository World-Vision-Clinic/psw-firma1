using Microsoft.EntityFrameworkCore.Migrations;

namespace Integration.Migrations
{
    public partial class AddressMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "City",
                table: "Pharmacies",
                newName: "Address_City");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "Pharmacies",
                newName: "Address_Street");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Address_City",
                table: "Pharmacies",
                newName: "City");

            migrationBuilder.RenameColumn(
                name: "Address_Street",
                table: "Pharmacies",
                newName: "Address");
        }
    }
}
