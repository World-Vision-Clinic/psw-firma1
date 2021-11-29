using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hospital.Migrations
{
    public partial class FixedProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsAnonymousss",
                table: "Feedbacks",
                newName: "IsAnonymous");

            migrationBuilder.UpdateData(
                table: "Surveys",
                keyColumn: "IdSurvey",
                keyValue: 1,
                column: "CreationDate",
                value: new DateTime(2021, 11, 30, 0, 19, 50, 483, DateTimeKind.Local).AddTicks(7383));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsAnonymous",
                table: "Feedbacks",
                newName: "IsAnonymousss");

            migrationBuilder.UpdateData(
                table: "Surveys",
                keyColumn: "IdSurvey",
                keyValue: 1,
                column: "CreationDate",
                value: new DateTime(2021, 11, 29, 23, 17, 29, 630, DateTimeKind.Local).AddTicks(2995));
        }
    }
}
