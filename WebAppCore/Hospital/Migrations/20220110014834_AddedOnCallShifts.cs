using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hospital.Migrations
{
    public partial class AddedOnCallShifts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_OnCallShift",
                table: "OnCallShift");

            migrationBuilder.RenameTable(
                name: "OnCallShift",
                newName: "OnCallShifts");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OnCallShifts",
                table: "OnCallShifts",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Surveys",
                keyColumn: "IdSurvey",
                keyValue: 1,
                column: "CreationDate",
                value: new DateTime(2022, 1, 10, 2, 48, 32, 601, DateTimeKind.Local).AddTicks(1002));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_OnCallShifts",
                table: "OnCallShifts");

            migrationBuilder.RenameTable(
                name: "OnCallShifts",
                newName: "OnCallShift");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OnCallShift",
                table: "OnCallShift",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Surveys",
                keyColumn: "IdSurvey",
                keyValue: 1,
                column: "CreationDate",
                value: new DateTime(2022, 1, 10, 2, 44, 49, 793, DateTimeKind.Local).AddTicks(7677));
        }
    }
}
