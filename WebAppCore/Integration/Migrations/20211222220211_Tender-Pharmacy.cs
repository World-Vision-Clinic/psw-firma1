using Microsoft.EntityFrameworkCore.Migrations;

namespace Integration.Migrations
{
    public partial class TenderPharmacy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "WinningPharmacyName",
                table: "Tenders",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WinningPharmacyName",
                table: "Tenders");
        }
    }
}
