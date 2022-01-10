using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Hospital.Migrations
{
    public partial class VacationMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Vacations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Start = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    End = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DoctorId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vacations", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Surveys",
                keyColumn: "IdSurvey",
                keyValue: 1,
                column: "CreationDate",
                value: new DateTime(2022, 1, 6, 11, 22, 11, 303, DateTimeKind.Local).AddTicks(8887));

            migrationBuilder.InsertData(
                table: "Vacations",
                columns: new[] { "Id", "Description", "DoctorId", "End", "Start" },
                values: new object[,]
                {
                    { 1, "aaaa", 1, new DateTime(2022, 1, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 1, 21, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "aaaa", 2, new DateTime(2022, 2, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 2, 21, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, "aaaa", 3, new DateTime(2022, 3, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 3, 21, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Vacations");

            migrationBuilder.UpdateData(
                table: "Surveys",
                keyColumn: "IdSurvey",
                keyValue: 1,
                column: "CreationDate",
                value: new DateTime(2022, 1, 4, 9, 55, 56, 753, DateTimeKind.Local).AddTicks(1268));
        }
    }
}
