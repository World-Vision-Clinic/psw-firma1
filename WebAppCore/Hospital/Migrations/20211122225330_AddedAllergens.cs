using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Hospital.Migrations
{
    public partial class AddedAllergens : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Patients",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Allergens",
                table: "Patients",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BloodType",
                table: "Patients",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Patients",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Patients",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "Patients",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Patients",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Gender",
                table: "Patients",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Height",
                table: "Patients",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Jmbg",
                table: "Patients",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Patients",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Patients",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PreferedDoctor",
                table: "Patients",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Weight",
                table: "Patients",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Allergens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Allergens", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Doctors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "text", nullable: true),
                    LastName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctors", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Allergens");

            migrationBuilder.DropTable(
                name: "Doctors");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "Allergens",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "BloodType",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "Height",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "Jmbg",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "PreferedDoctor",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "Patients");
        }
    }
}
