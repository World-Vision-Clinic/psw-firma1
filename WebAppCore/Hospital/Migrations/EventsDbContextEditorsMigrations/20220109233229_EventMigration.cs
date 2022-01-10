using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Hospital.Migrations.EventsDbContextEditorsMigrations
{
    public partial class EventMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EventsEditors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    EventTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventsEditors", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "EventsEditors",
                columns: new[] { "Id", "EventTime", "Name" },
                values: new object[] { 1, new DateTime(2022, 1, 10, 0, 32, 28, 861, DateTimeKind.Local).AddTicks(9666), "Klik" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventsEditors");
        }
    }
}
