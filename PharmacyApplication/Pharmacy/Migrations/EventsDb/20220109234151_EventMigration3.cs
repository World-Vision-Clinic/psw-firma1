using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Pharmacy.Migrations.EventsDb
{
    public partial class EventMigration3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_EventsPharmacyApp",
                table: "EventsPharmacyApp");

            migrationBuilder.RenameTable(
                name: "EventsPharmacyApp",
                newName: "EventsPharmacy");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EventsPharmacy",
                table: "EventsPharmacy",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "EventsPharmacy",
                keyColumn: "Id",
                keyValue: 1,
                column: "EventTime",
                value: new DateTime(2022, 1, 10, 0, 41, 50, 998, DateTimeKind.Local).AddTicks(1384));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_EventsPharmacy",
                table: "EventsPharmacy");

            migrationBuilder.RenameTable(
                name: "EventsPharmacy",
                newName: "EventsPharmacyApp");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EventsPharmacyApp",
                table: "EventsPharmacyApp",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "EventsPharmacyApp",
                keyColumn: "Id",
                keyValue: 1,
                column: "EventTime",
                value: new DateTime(2022, 1, 10, 0, 20, 23, 136, DateTimeKind.Local).AddTicks(4458));
        }
    }
}
