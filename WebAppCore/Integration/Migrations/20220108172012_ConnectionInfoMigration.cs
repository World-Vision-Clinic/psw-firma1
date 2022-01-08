using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Integration.Migrations
{
    public partial class ConnectionInfoMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Pharmacies",
                table: "Pharmacies");

            migrationBuilder.DropColumn(
                name: "Localhost",
                table: "Pharmacies");

            migrationBuilder.RenameColumn(
                name: "Protocol",
                table: "Pharmacies",
                newName: "ConnectionInfo_Protocol");

            migrationBuilder.RenameColumn(
                name: "Key",
                table: "Pharmacies",
                newName: "ConnectionInfo_Key");

            migrationBuilder.AlterColumn<int>(
                name: "ConnectionInfo_Protocol",
                table: "Pharmacies",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Pharmacies",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<string>(
                name: "ConnectionInfo_Domain",
                table: "Pharmacies",
                type: "text",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pharmacies",
                table: "Pharmacies",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Pharmacies",
                table: "Pharmacies");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Pharmacies");

            migrationBuilder.DropColumn(
                name: "ConnectionInfo_Domain",
                table: "Pharmacies");

            migrationBuilder.RenameColumn(
                name: "ConnectionInfo_Protocol",
                table: "Pharmacies",
                newName: "Protocol");

            migrationBuilder.RenameColumn(
                name: "ConnectionInfo_Key",
                table: "Pharmacies",
                newName: "Key");

            migrationBuilder.AlterColumn<int>(
                name: "Protocol",
                table: "Pharmacies",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Localhost",
                table: "Pharmacies",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pharmacies",
                table: "Pharmacies",
                column: "Localhost");
        }
    }
}
