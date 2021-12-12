using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hospital.Migrations
{
    public partial class UpdateAppointments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Appointments",
                columns: new[] { "Id", "Date", "DoctorForeignKey", "IsCanceled", "IsFinished", "IsUpcoming", "PatientForeignKey", "Time", "Type" },
                values: new object[,]
                {
                    { 1, new DateTime(2021, 12, 8, 0, 0, 0, 0, DateTimeKind.Local), 2, false, false, true, 1, new TimeSpan(0, 0, 0, 0, 0), 0 },
                    { 2, new DateTime(2021, 12, 8, 0, 0, 0, 0, DateTimeKind.Local), 2, true, false, false, 1, new TimeSpan(0, 0, 0, 0, 0), 0 },
                    { 3, new DateTime(2021, 12, 8, 0, 0, 0, 0, DateTimeKind.Local), 2, false, true, false, 1, new TimeSpan(0, 0, 0, 0, 0), 0 }
                });

            migrationBuilder.UpdateData(
                table: "Surveys",
                keyColumn: "IdSurvey",
                keyValue: 1,
                column: "CreationDate",
                value: new DateTime(2021, 12, 8, 13, 51, 3, 738, DateTimeKind.Local).AddTicks(6544));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.UpdateData(
                table: "Surveys",
                keyColumn: "IdSurvey",
                keyValue: 1,
                column: "CreationDate",
                value: new DateTime(2021, 12, 8, 13, 29, 40, 551, DateTimeKind.Local).AddTicks(236));
        }
    }
}
