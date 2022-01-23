using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Pharmacy.Migrations.EventsDb
{
    public partial class EventMigration2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Events",
                table: "Events");

            migrationBuilder.RenameTable(
                name: "Events",
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_EventsPharmacyApp",
                table: "EventsPharmacyApp");

            migrationBuilder.RenameTable(
                name: "EventsPharmacyApp",
                newName: "Events");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Events",
                table: "Events",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 1,
                column: "EventTime",
                value: new DateTime(2022, 1, 10, 0, 15, 25, 131, DateTimeKind.Local).AddTicks(6673));
        }
    }
}
