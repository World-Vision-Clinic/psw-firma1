using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hospital.Migrations.EventsDb
{
    public partial class EventMigration3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Application",
                table: "EventsHospitalApp");

            migrationBuilder.UpdateData(
                table: "EventsHospitalApp",
                keyColumn: "Id",
                keyValue: 1,
                column: "EventTime",
                value: new DateTime(2022, 1, 10, 0, 19, 1, 996, DateTimeKind.Local).AddTicks(4597));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Application",
                table: "EventsHospitalApp",
                type: "text",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "EventsHospitalApp",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Application", "EventTime" },
                values: new object[] { "Hospital", new DateTime(2022, 1, 10, 0, 16, 54, 262, DateTimeKind.Local).AddTicks(7750) });
        }
    }
}
