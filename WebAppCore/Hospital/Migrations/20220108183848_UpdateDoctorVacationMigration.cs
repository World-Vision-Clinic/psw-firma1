using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hospital.Migrations
{
    public partial class UpdateDoctorVacationMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "Vacations",
                type: "text",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Surveys",
                keyColumn: "IdSurvey",
                keyValue: 1,
                column: "CreationDate",
                value: new DateTime(2022, 1, 8, 19, 38, 47, 89, DateTimeKind.Local).AddTicks(1070));

            migrationBuilder.UpdateData(
                table: "Vacations",
                keyColumn: "Id",
                keyValue: 1,
                column: "FullName",
                value: "Slavica Matic");

            migrationBuilder.UpdateData(
                table: "Vacations",
                keyColumn: "Id",
                keyValue: 2,
                column: "FullName",
                value: "Mirko Jankovic");

            migrationBuilder.UpdateData(
                table: "Vacations",
                keyColumn: "Id",
                keyValue: 3,
                column: "FullName",
                value: "Matija Popic");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FullName",
                table: "Vacations");

            migrationBuilder.UpdateData(
                table: "Surveys",
                keyColumn: "IdSurvey",
                keyValue: 1,
                column: "CreationDate",
                value: new DateTime(2022, 1, 6, 11, 45, 16, 65, DateTimeKind.Local).AddTicks(5315));
        }
    }
}
