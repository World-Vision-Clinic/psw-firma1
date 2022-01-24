using Microsoft.EntityFrameworkCore.Migrations;

namespace Hospital.Migrations.EventsDb
{
    public partial class AddedPatientAge : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PatientAge",
                table: "EventsHospital",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PatientAge",
                table: "EventsHospital");
        }
    }
}
