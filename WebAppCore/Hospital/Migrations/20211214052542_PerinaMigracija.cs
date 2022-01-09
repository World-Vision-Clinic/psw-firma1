using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Hospital.Migrations
{
    public partial class PerinaMigracija : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Renovations",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Room1Id = table.Column<int>(type: "integer", nullable: false),
                    Room2Id = table.Column<int>(type: "integer", nullable: false),
                    NewRoomName1 = table.Column<string>(type: "text", nullable: true),
                    NewRoomName2 = table.Column<string>(type: "text", nullable: true),
                    NewRoomPurpose1 = table.Column<string>(type: "text", nullable: true),
                    NewRoomPurpose2 = table.Column<string>(type: "text", nullable: true),
                    StartDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    isMerge = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Renovations", x => x.id);
                });

            migrationBuilder.InsertData(
                table: "Renovations",
                columns: new[] { "id", "EndDate", "NewRoomName1", "NewRoomName2", "NewRoomPurpose1", "NewRoomPurpose2", "Room1Id", "Room2Id", "StartDate", "isMerge" },
                values: new object[] { 1, new DateTime(2021, 12, 27, 8, 0, 0, 0, DateTimeKind.Local), "Test 123", "", "123", "", 4, 5, new DateTime(2021, 12, 20, 8, 0, 0, 0, DateTimeKind.Local), true });

            migrationBuilder.UpdateData(
                table: "Surveys",
                keyColumn: "IdSurvey",
                keyValue: 1,
                column: "CreationDate",
                value: new DateTime(2021, 12, 14, 6, 25, 39, 901, DateTimeKind.Local).AddTicks(1433));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Renovations");

            migrationBuilder.UpdateData(
                table: "Surveys",
                keyColumn: "IdSurvey",
                keyValue: 1,
                column: "CreationDate",
                value: new DateTime(2021, 12, 13, 15, 3, 35, 578, DateTimeKind.Local).AddTicks(8222));
        }
    }
}
