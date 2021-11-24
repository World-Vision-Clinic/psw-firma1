using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Hospital.Migrations
{
    public partial class ChangedPatientModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Residence",
                table: "Patients",
                newName: "Phone");

            migrationBuilder.RenameColumn(
                name: "DoctorName",
                table: "Patients",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "ContactPhone",
                table: "Patients",
                newName: "Jmbg");

            migrationBuilder.AlterColumn<int>(
                name: "BloodType",
                table: "Patients",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Patients",
                type: "text",
                nullable: true);

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

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Patients",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PreferedDoctor",
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

            migrationBuilder.CreateTable(
                name: "PatientAllergens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PatientId = table.Column<int>(type: "integer", nullable: false),
                    AllergenId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientAllergens", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Surveys",
                keyColumn: "IdSurvey",
                keyValue: 1,
                column: "CreationDate",
                value: new DateTime(2021, 11, 24, 22, 28, 15, 843, DateTimeKind.Local).AddTicks(6241));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Allergens");

            migrationBuilder.DropTable(
                name: "Doctors");

            migrationBuilder.DropTable(
                name: "PatientAllergens");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "PreferedDoctor",
                table: "Patients");

            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "Patients",
                newName: "Residence");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Patients",
                newName: "DoctorName");

            migrationBuilder.RenameColumn(
                name: "Jmbg",
                table: "Patients",
                newName: "ContactPhone");

            migrationBuilder.AlterColumn<string>(
                name: "BloodType",
                table: "Patients",
                type: "text",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.UpdateData(
                table: "Surveys",
                keyColumn: "IdSurvey",
                keyValue: 1,
                column: "CreationDate",
                value: new DateTime(2021, 11, 24, 16, 21, 13, 235, DateTimeKind.Local).AddTicks(3363));
        }
    }
}
