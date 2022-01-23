using Microsoft.EntityFrameworkCore.Migrations;

namespace Integration.Migrations
{
    public partial class EmailAddedToPharmacy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Pharmacies",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Pharmacies");
        }
    }
}
