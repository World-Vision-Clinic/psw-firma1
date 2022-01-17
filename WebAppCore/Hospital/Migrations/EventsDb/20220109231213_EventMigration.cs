using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Hospital.Migrations.EventsDb
{
    public partial class EventMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EventsHospitalApp",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    EventTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Application = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventsHospitalApp", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "EventsHospitalApp",
                columns: new[] { "Id", "Application", "EventTime", "Name" },
                values: new object[] { 1, "Hospital", new DateTime(2022, 1, 10, 0, 12, 13, 337, DateTimeKind.Local).AddTicks(3311), "Klik" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventsHospitalApp");
        }
    }
}
