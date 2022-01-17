using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hospital.Migrations.EventsDb
{
    public partial class EventMigration4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_EventsHospitalApp",
                table: "EventsHospitalApp");

            migrationBuilder.RenameTable(
                name: "EventsHospitalApp",
                newName: "EventsHospital");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EventsHospital",
                table: "EventsHospital",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "EventsHospital",
                keyColumn: "Id",
                keyValue: 1,
                column: "EventTime",
                value: new DateTime(2022, 1, 10, 0, 43, 6, 759, DateTimeKind.Local).AddTicks(8106));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_EventsHospital",
                table: "EventsHospital");

            migrationBuilder.RenameTable(
                name: "EventsHospital",
                newName: "EventsHospitalApp");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EventsHospitalApp",
                table: "EventsHospitalApp",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "EventsHospitalApp",
                keyColumn: "Id",
                keyValue: 1,
                column: "EventTime",
                value: new DateTime(2022, 1, 10, 0, 19, 1, 996, DateTimeKind.Local).AddTicks(4597));
        }
    }
}
