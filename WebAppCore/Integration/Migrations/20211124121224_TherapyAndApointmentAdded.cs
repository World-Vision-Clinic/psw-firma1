using Microsoft.EntityFrameworkCore.Migrations;

namespace Integration.Migrations
{
    public partial class TherapyAndApointmentAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MedicineId",
                table: "Therapy",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MedicineId",
                table: "Therapy");
        }
    }
}
