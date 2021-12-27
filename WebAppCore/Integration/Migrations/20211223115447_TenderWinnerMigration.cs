using Microsoft.EntityFrameworkCore.Migrations;

namespace Integration.Migrations
{
    public partial class TenderWinnerMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Winner",
                table: "TenderOffers",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Winner",
                table: "TenderOffers");
        }
    }
}
