using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Hospital.Migrations
{
    public partial class editorMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AllEquipment",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    Amount = table.Column<int>(type: "integer", nullable: false),
                    RoomId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllEquipment", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Areas",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    X = table.Column<int>(type: "integer", nullable: false),
                    Y = table.Column<int>(type: "integer", nullable: false),
                    Width = table.Column<int>(type: "integer", nullable: false),
                    Height = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Areas", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "FloorLabels",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    X = table.Column<int>(type: "integer", nullable: false),
                    Y = table.Column<int>(type: "integer", nullable: false),
                    Text = table.Column<string>(type: "text", nullable: true),
                    FloorId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FloorLabels", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Floors",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Level = table.Column<string>(type: "text", nullable: true),
                    Info = table.Column<string>(type: "text", nullable: true),
                    BuildingId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Floors", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "MapPositions",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    X = table.Column<int>(type: "integer", nullable: false),
                    Y = table.Column<int>(type: "integer", nullable: false),
                    Height = table.Column<int>(type: "integer", nullable: false),
                    Width = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MapPositions", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "OutsideDoors",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    X = table.Column<int>(type: "integer", nullable: false),
                    Y = table.Column<int>(type: "integer", nullable: false),
                    IsVertical = table.Column<bool>(type: "boolean", nullable: false),
                    MapPositionId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OutsideDoors", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Parkings",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    X = table.Column<int>(type: "integer", nullable: false),
                    Y = table.Column<int>(type: "integer", nullable: false),
                    Width = table.Column<int>(type: "integer", nullable: false),
                    Height = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parkings", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Purpose = table.Column<string>(type: "text", nullable: true),
                    DoctorId = table.Column<int>(type: "integer", nullable: false),
                    FloorId = table.Column<int>(type: "integer", nullable: false),
                    X = table.Column<int>(type: "integer", nullable: false),
                    Y = table.Column<int>(type: "integer", nullable: false),
                    Height = table.Column<int>(type: "integer", nullable: false),
                    Width = table.Column<int>(type: "integer", nullable: false),
                    DoorX = table.Column<int>(type: "integer", nullable: false),
                    DoorY = table.Column<int>(type: "integer", nullable: false),
                    Vertical = table.Column<bool>(type: "boolean", nullable: false),
                    Css = table.Column<string>(type: "text", nullable: true),
                    DoorExist = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Buildings",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Info = table.Column<string>(type: "text", nullable: true),
                    Areaid = table.Column<int>(type: "integer", nullable: true),
                    MapPositionId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Buildings", x => x.id);
                    table.ForeignKey(
                        name: "FK_Buildings_Areas_Areaid",
                        column: x => x.Areaid,
                        principalTable: "Areas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AllEquipment",
                columns: new[] { "id", "Amount", "Name", "RoomId", "Type" },
                values: new object[,]
                {
                    { 8, 1, "Operating table", 3, 0 },
                    { 14, 25, "Bandage", 5, 1 },
                    { 13, 6, "Chair", 5, 0 },
                    { 12, 11, "Bed", 5, 0 },
                    { 11, 4, "Chair", 4, 0 },
                    { 10, 7, "Bed", 4, 0 },
                    { 9, 11, "Syringe", 3, 1 },
                    { 7, 15, "Bandage", 3, 1 },
                    { 6, 23, "Infusion", 2, 1 },
                    { 5, 2, "Operating table", 2, 0 },
                    { 4, 17, "Bandage", 2, 1 },
                    { 1, 15, "Bandage", 1, 1 },
                    { 2, 3, "Operating table", 1, 0 },
                    { 3, 11, "Infusion", 1, 1 }
                });

            migrationBuilder.InsertData(
                table: "Buildings",
                columns: new[] { "id", "Areaid", "Info", "MapPositionId", "Name" },
                values: new object[,]
                {
                    { 1, null, "Gynecology", 1, "Hospital I" },
                    { 2, null, "", 2, "Hospital II" }
                });

            migrationBuilder.InsertData(
                table: "FloorLabels",
                columns: new[] { "id", "FloorId", "Text", "X", "Y" },
                values: new object[] { 1, 1, "ENTRANCE", 50, 80 });

            migrationBuilder.InsertData(
                table: "Floors",
                columns: new[] { "id", "BuildingId", "Info", "Level" },
                values: new object[,]
                {
                    { 2, 1, null, "First floor" },
                    { 1, 1, null, "Ground floor" }
                });

            migrationBuilder.InsertData(
                table: "MapPositions",
                columns: new[] { "id", "Height", "Width", "X", "Y" },
                values: new object[,]
                {
                    { 2, 180, 520, 30, 460 },
                    { 1, 180, 520, 30, 20 }
                });

            migrationBuilder.InsertData(
                table: "OutsideDoors",
                columns: new[] { "id", "IsVertical", "MapPositionId", "X", "Y" },
                values: new object[,]
                {
                    { 3, true, 2, 545, 505 },
                    { 2, false, 1, 260, 195 },
                    { 1, true, 1, 545, 80 },
                    { 4, false, 2, 260, 455 }
                });

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "id", "Css", "DoctorId", "DoorExist", "DoorX", "DoorY", "FloorId", "Height", "Name", "Purpose", "Vertical", "Width", "X", "Y" },
                values: new object[,]
                {
                    { 15, "room", -1, true, 260, 498, 0, 100, "DOCTOR'S OFFICE 2", "", false, 150, 160, 150 },
                    { 16, "room", -1, true, 420, 498, 0, 100, "DOCTOR'S OFFICE 3", "", false, 150, 320, 150 },
                    { 17, "room", -1, true, 595, 498, 0, 100, "DOCTOR'S OFFICE 4", "", false, 170, 480, 150 },
                    { 18, "room", -1, true, 680, 498, 0, 100, "ROOM 1", "", false, 180, 660, 150 },
                    { 19, "room", -1, false, 728, 290, 0, 104, "STAIRS", "", true, 70, 770, 258 },
                    { 24, "room", -1, true, 200, 498, 0, 100, "ROOM 2", "", false, 100, 160, 500 },
                    { 21, "room", -1, true, 728, 485, 0, 60, "TOILET", "", true, 110, 730, 470 },
                    { 22, "room", -1, true, 728, 555, 0, 60, "TOILET", "", true, 110, 730, 540 },
                    { 23, "room", -1, true, 148, 419, 0, 190, "OPERATING ROOM 2", "", true, 150, 0, 410 },
                    { 14, "room", -1, true, 100, 248, 0, 100, "DOCTOR'S OFFICE 1", "", false, 150, 0, 150 },
                    { 20, "staircase", -1, false, 728, 290, 0, 90, "LIFT", "", false, 150, 690, 370 },
                    { 13, "room room-cadetblue", -1, true, 475, 498, 0, 100, "ROOM 5", "", false, 150, 430, 500 },
                    { 1, "room room-cadetblue", -1, true, 148, 285, 1, 190, "OPERATING ROOM 1", "", true, 150, 0, 150 },
                    { 11, "room room-cadetblue", -1, true, 200, 498, 0, 100, "ROOM 3", "", false, 100, 160, 500 },
                    { 10, "room room-cadetblue", -1, true, 148, 419, 0, 190, "OPERATING ROOM 4", "", true, 150, 0, 410 },
                    { 9, "room room-cadetblue", -1, true, 728, 555, 0, 60, "TOILET", "", true, 110, 730, 540 },
                    { 8, "room room-cadetblue", -1, true, 728, 485, 0, 60, "TOILET", "", true, 110, 730, 470 },
                    { 7, "staircase", -1, false, 728, 290, 0, 90, "LIFT", "", false, 150, 690, 370 },
                    { 6, "room room-cadetblue", -1, true, 728, 290, 0, 100, "OFFICE 1", "", true, 110, 730, 260 },
                    { 5, "room room-cadetblue", -1, true, 680, 248, 0, 100, "ROOM 2", "", false, 180, 660, 150 },
                    { 4, "room room-cadetblue", -1, true, 520, 248, 0, 100, "ROOM 1", "", false, 170, 480, 150 },
                    { 3, "room room-cadetblue", -1, true, 370, 248, 0, 100, "OPERATING ROOM 3", "", false, 150, 320, 150 },
                    { 2, "room room-cadetblue", -1, true, 220, 248, 0, 100, "OPERATING ROOM 2", "", false, 150, 160, 150 },
                    { 25, "room", -1, true, 350, 498, 0, 100, "OPERATING ROOM 3", "", false, 300, 270, 500 },
                    { 12, "room room-cadetblue", -1, true, 315, 498, 0, 100, "ROOM 4", "", false, 150, 270, 500 },
                    { 26, "room", -1, true, 400, 398, 0, 100, "OPERATING ROOM 1", "", false, 580, 0, 300 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Buildings_Areaid",
                table: "Buildings",
                column: "Areaid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AllEquipment");

            migrationBuilder.DropTable(
                name: "Buildings");

            migrationBuilder.DropTable(
                name: "FloorLabels");

            migrationBuilder.DropTable(
                name: "Floors");

            migrationBuilder.DropTable(
                name: "MapPositions");

            migrationBuilder.DropTable(
                name: "OutsideDoors");

            migrationBuilder.DropTable(
                name: "Parkings");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropTable(
                name: "Areas");
        }
    }
}
