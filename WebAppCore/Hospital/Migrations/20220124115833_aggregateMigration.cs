using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Hospital.Migrations
{
    public partial class aggregateMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Time",
                table: "Appointments",
                newName: "Length");

            migrationBuilder.CreateTable(
                name: "EquipmentTransportationAggregates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EquipmentId = table.Column<int>(type: "integer", nullable: true),
                    RoomFromId = table.Column<int>(type: "integer", nullable: true),
                    RoomToId = table.Column<int>(type: "integer", nullable: true),
                    Version = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipmentTransportationAggregates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EquipmentTransportationAggregates_AllEquipment_EquipmentId",
                        column: x => x.EquipmentId,
                        principalTable: "AllEquipment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EquipmentTransportationAggregates_Rooms_RoomFromId",
                        column: x => x.RoomFromId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EquipmentTransportationAggregates_Rooms_RoomToId",
                        column: x => x.RoomToId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EquipmentTransportationEvent",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AggregateId = table.Column<int>(type: "integer", nullable: false),
                    ReasonForTransportation = table.Column<string>(type: "text", nullable: true),
                    TimeOfTransport = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    EquipmentTransportationId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipmentTransportationEvent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EquipmentTransportationEvent_EquipmentTransportationAggrega~",
                        column: x => x.EquipmentTransportationId,
                        principalTable: "EquipmentTransportationAggregates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentTransportationAggregates_EquipmentId",
                table: "EquipmentTransportationAggregates",
                column: "EquipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentTransportationAggregates_RoomFromId",
                table: "EquipmentTransportationAggregates",
                column: "RoomFromId");

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentTransportationAggregates_RoomToId",
                table: "EquipmentTransportationAggregates",
                column: "RoomToId");

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentTransportationEvent_EquipmentTransportationId",
                table: "EquipmentTransportationEvent",
                column: "EquipmentTransportationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EquipmentTransportationEvent");

            migrationBuilder.DropTable(
                name: "EquipmentTransportationAggregates");

            migrationBuilder.RenameColumn(
                name: "Length",
                table: "Appointments",
                newName: "Time");

        }
    }
}
