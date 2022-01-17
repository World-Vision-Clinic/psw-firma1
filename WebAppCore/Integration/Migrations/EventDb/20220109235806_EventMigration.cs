using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Integration.Migrations.EventDb
{
    public partial class EventMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EventsIntegration",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    EventTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventsIntegration", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "EventsIntegration",
                columns: new[] { "Id", "EventTime", "Name" },
                values: new object[] { 1, new DateTime(2022, 1, 10, 0, 58, 6, 358, DateTimeKind.Local).AddTicks(1413), "Klik" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventsIntegration");
        }
    }
}
