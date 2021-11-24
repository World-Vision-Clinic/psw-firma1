using Microsoft.EntityFrameworkCore.Migrations;

namespace Hospital.Migrations
{
    public partial class doorsUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "id",
                keyValue: 4,
                column: "DoorX",
                value: 680);

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "id",
                keyValue: 15,
                column: "DoorY",
                value: 248);

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "id",
                keyValue: 16,
                column: "DoorY",
                value: 248);

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "id",
                keyValue: 17,
                column: "DoorY",
                value: 248);

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "id",
                keyValue: 18,
                column: "DoorY",
                value: 248);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "id",
                keyValue: 4,
                column: "DoorX",
                value: 520);

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "id",
                keyValue: 15,
                column: "DoorY",
                value: 498);

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "id",
                keyValue: 16,
                column: "DoorY",
                value: 498);

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "id",
                keyValue: 17,
                column: "DoorY",
                value: 498);

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "id",
                keyValue: 18,
                column: "DoorY",
                value: 498);
        }
    }
}
