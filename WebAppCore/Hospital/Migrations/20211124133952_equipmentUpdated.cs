using Microsoft.EntityFrameworkCore.Migrations;

namespace Hospital.Migrations
{
    public partial class equipmentUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AllEquipment",
                keyColumn: "id",
                keyValue: 1,
                column: "RoomId",
                value: 15);

            migrationBuilder.UpdateData(
                table: "AllEquipment",
                keyColumn: "id",
                keyValue: 2,
                column: "RoomId",
                value: 23);

            migrationBuilder.UpdateData(
                table: "AllEquipment",
                keyColumn: "id",
                keyValue: 6,
                column: "RoomId",
                value: 23);

            migrationBuilder.UpdateData(
                table: "AllEquipment",
                keyColumn: "id",
                keyValue: 11,
                column: "RoomId",
                value: 16);

            migrationBuilder.UpdateData(
                table: "AllEquipment",
                keyColumn: "id",
                keyValue: 13,
                column: "RoomId",
                value: 17);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AllEquipment",
                keyColumn: "id",
                keyValue: 1,
                column: "RoomId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "AllEquipment",
                keyColumn: "id",
                keyValue: 2,
                column: "RoomId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "AllEquipment",
                keyColumn: "id",
                keyValue: 6,
                column: "RoomId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "AllEquipment",
                keyColumn: "id",
                keyValue: 11,
                column: "RoomId",
                value: 4);

            migrationBuilder.UpdateData(
                table: "AllEquipment",
                keyColumn: "id",
                keyValue: 13,
                column: "RoomId",
                value: 5);
        }
    }
}
