using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hospital.Migrations
{
    public partial class UpdateMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "Id", "Answer", "IdSurvey", "Question", "Section" },
                values: new object[,]
                {
                    { 2, 0, 1, "How would you rate the professionalism of doctor?", 1 },
                    { 3, 0, 1, "How clearly did the doctor explain you your condition?", 1 },
                    { 4, 0, 1, "How smisli dalje?", 1 },
                    { 5, 0, 1, "What is your overall satisfaction with doctor?", 1 },
                    { 6, 0, 1, "Has doctor been polite to you?", 0 },
                    { 7, 0, 1, "How would you rate the professionalism of doctor?", 0 },
                    { 8, 0, 1, "How clearly did the doctor explain you your condition?", 0 },
                    { 9, 0, 1, "How smisli dalje?", 0 },
                    { 10, 0, 1, "What is your overall satisfaction with doctor?", 0 },
                    { 11, 0, 1, "Has doctor been polite to you?", 2 },
                    { 12, 0, 1, "How would you rate the professionalism of doctor?", 2 },
                    { 13, 0, 1, "How clearly did the doctor explain you your condition?", 2 },
                    { 14, 0, 1, "How smisli dalje?", 2 },
                    { 15, 0, 1, "What is your overall satisfaction with doctor?", 2 }
                });

            migrationBuilder.UpdateData(
                table: "Surveys",
                keyColumn: "IdSurvey",
                keyValue: 1,
                column: "CreationDate",
                value: new DateTime(2021, 11, 22, 15, 41, 37, 865, DateTimeKind.Local).AddTicks(441));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.UpdateData(
                table: "Surveys",
                keyColumn: "IdSurvey",
                keyValue: 1,
                column: "CreationDate",
                value: new DateTime(2021, 11, 22, 15, 28, 33, 150, DateTimeKind.Local).AddTicks(4166));
        }
    }
}
