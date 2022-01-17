using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hospital.Migrations.EventsDb
{
    public partial class EventMigration2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "EventsHospitalApp",
                keyColumn: "Id",
                keyValue: 1,
                column: "EventTime",
                value: new DateTime(2022, 1, 10, 0, 16, 54, 262, DateTimeKind.Local).AddTicks(7750));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "EventsHospitalApp",
                keyColumn: "Id",
                keyValue: 1,
                column: "EventTime",
                value: new DateTime(2022, 1, 10, 0, 12, 13, 337, DateTimeKind.Local).AddTicks(3311));
        }
    }
}
