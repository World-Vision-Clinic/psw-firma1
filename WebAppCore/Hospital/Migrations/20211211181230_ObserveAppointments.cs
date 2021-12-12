using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hospital.Migrations
{
    public partial class ObserveAppointments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DoctorName",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "DoctorSurname",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "IsCanceled",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "IsFinished",
                table: "Appointments");

            migrationBuilder.RenameColumn(
                name: "IsUpcoming",
                table: "Appointments",
                newName: "IsCancelled");

            migrationBuilder.UpdateData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Date", "IsCancelled" },
                values: new object[] { new DateTime(2021, 12, 11, 0, 0, 0, 0, DateTimeKind.Local), false });

            migrationBuilder.UpdateData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Date", "IsCancelled" },
                values: new object[] { new DateTime(2021, 12, 11, 0, 0, 0, 0, DateTimeKind.Local), true });

            migrationBuilder.UpdateData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 3,
                column: "Date",
                value: new DateTime(2021, 12, 11, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Surveys",
                keyColumn: "IdSurvey",
                keyValue: 1,
                column: "CreationDate",
                value: new DateTime(2021, 12, 11, 19, 12, 29, 920, DateTimeKind.Local).AddTicks(5596));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsCancelled",
                table: "Appointments",
                newName: "IsUpcoming");

            migrationBuilder.AddColumn<string>(
                name: "DoctorName",
                table: "Appointments",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DoctorSurname",
                table: "Appointments",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsCanceled",
                table: "Appointments",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFinished",
                table: "Appointments",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Date", "IsUpcoming" },
                values: new object[] { new DateTime(2021, 12, 8, 0, 0, 0, 0, DateTimeKind.Local), true });

            migrationBuilder.UpdateData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Date", "IsCanceled", "IsUpcoming" },
                values: new object[] { new DateTime(2021, 12, 8, 0, 0, 0, 0, DateTimeKind.Local), true, false });

            migrationBuilder.UpdateData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Date", "IsFinished" },
                values: new object[] { new DateTime(2021, 12, 8, 0, 0, 0, 0, DateTimeKind.Local), true });

            migrationBuilder.UpdateData(
                table: "Surveys",
                keyColumn: "IdSurvey",
                keyValue: 1,
                column: "CreationDate",
                value: new DateTime(2021, 12, 8, 15, 10, 54, 731, DateTimeKind.Local).AddTicks(6640));
        }
    }
}
