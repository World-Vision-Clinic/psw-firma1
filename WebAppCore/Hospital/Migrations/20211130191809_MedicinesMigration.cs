using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Hospital.Migrations
{
    public partial class MedicinesMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<List<string>>(
                name: "IngredientNames",
                table: "Allergens",
                type: "text[]",
                nullable: true);

            migrationBuilder.AddColumn<List<string>>(
                name: "MedicineNames",
                table: "Allergens",
                type: "text[]",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MedicalRecords",
                columns: table => new
                {
                    MedicalRecordID = table.Column<string>(type: "text", nullable: false),
                    AllergenId = table.Column<int>(type: "integer", nullable: true),
                    PatientId = table.Column<int>(type: "integer", nullable: true),
                    HealthCardNumber = table.Column<string>(type: "text", nullable: true),
                    ParentName = table.Column<string>(type: "text", nullable: true),
                    IsInsured = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalRecords", x => x.MedicalRecordID);
                    table.ForeignKey(
                        name: "FK_MedicalRecords_Allergens_AllergenId",
                        column: x => x.AllergenId,
                        principalTable: "Allergens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MedicalRecords_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Medicines",
                columns: table => new
                {
                    ID = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    DosageInMg = table.Column<double>(type: "double precision", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    Price = table.Column<double>(type: "double precision", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    ReplacementMedicineIDs = table.Column<List<string>>(type: "text[]", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medicines", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Therapies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MedicineId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Therapies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ingredient",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    MedicineID = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredient", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ingredient_Medicines_MedicineID",
                        column: x => x.MedicineID,
                        principalTable: "Medicines",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Examinations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Diagnosis = table.Column<string>(type: "text", nullable: true),
                    anamnesis = table.Column<string>(type: "text", nullable: true),
                    dateOfExamination = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    TherapyId = table.Column<int>(type: "integer", nullable: false),
                    appointmentId = table.Column<int>(type: "integer", nullable: true),
                    patientVisible = table.Column<bool>(type: "boolean", nullable: false),
                    MedicalRecordId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Examinations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Examinations_Appointments_appointmentId",
                        column: x => x.appointmentId,
                        principalTable: "Appointments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Examinations_MedicalRecords_MedicalRecordId",
                        column: x => x.MedicalRecordId,
                        principalTable: "MedicalRecords",
                        principalColumn: "MedicalRecordID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Examinations_Therapies_TherapyId",
                        column: x => x.TherapyId,
                        principalTable: "Therapies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MedicineTherapy",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MedicineID = table.Column<string>(type: "text", nullable: true),
                    DurationInDays = table.Column<int>(type: "integer", nullable: false),
                    TimesPerDay = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    TherapyId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicineTherapy", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicineTherapy_Medicines_MedicineID",
                        column: x => x.MedicineID,
                        principalTable: "Medicines",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MedicineTherapy_Therapies_TherapyId",
                        column: x => x.TherapyId,
                        principalTable: "Therapies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Surveys",
                keyColumn: "IdSurvey",
                keyValue: 1,
                column: "CreationDate",
                value: new DateTime(2021, 11, 30, 20, 18, 8, 180, DateTimeKind.Local).AddTicks(3591));

            migrationBuilder.CreateIndex(
                name: "IX_Examinations_appointmentId",
                table: "Examinations",
                column: "appointmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Examinations_MedicalRecordId",
                table: "Examinations",
                column: "MedicalRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_Examinations_TherapyId",
                table: "Examinations",
                column: "TherapyId");

            migrationBuilder.CreateIndex(
                name: "IX_Ingredient_MedicineID",
                table: "Ingredient",
                column: "MedicineID");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalRecords_AllergenId",
                table: "MedicalRecords",
                column: "AllergenId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalRecords_PatientId",
                table: "MedicalRecords",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicineTherapy_MedicineID",
                table: "MedicineTherapy",
                column: "MedicineID");

            migrationBuilder.CreateIndex(
                name: "IX_MedicineTherapy_TherapyId",
                table: "MedicineTherapy",
                column: "TherapyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Examinations");

            migrationBuilder.DropTable(
                name: "Ingredient");

            migrationBuilder.DropTable(
                name: "MedicineTherapy");

            migrationBuilder.DropTable(
                name: "MedicalRecords");

            migrationBuilder.DropTable(
                name: "Medicines");

            migrationBuilder.DropTable(
                name: "Therapies");

            migrationBuilder.DropColumn(
                name: "IngredientNames",
                table: "Allergens");

            migrationBuilder.DropColumn(
                name: "MedicineNames",
                table: "Allergens");

            migrationBuilder.UpdateData(
                table: "Surveys",
                keyColumn: "IdSurvey",
                keyValue: 1,
                column: "CreationDate",
                value: new DateTime(2021, 11, 30, 0, 19, 50, 483, DateTimeKind.Local).AddTicks(7383));
        }
    }
}
