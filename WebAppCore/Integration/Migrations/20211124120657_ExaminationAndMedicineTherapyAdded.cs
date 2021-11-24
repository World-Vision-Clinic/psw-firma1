using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Integration.Migrations
{
    public partial class ExaminationAndMedicineTherapyAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Examination_MedicalRecords_MedicalRecordID",
                table: "Examination");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Examination",
                table: "Examination");

            migrationBuilder.RenameTable(
                name: "Examination",
                newName: "Examinations");

            migrationBuilder.RenameColumn(
                name: "MedicalRecordID",
                table: "Examinations",
                newName: "MedicalRecordId");

            migrationBuilder.RenameIndex(
                name: "IX_Examination_MedicalRecordID",
                table: "Examinations",
                newName: "IX_Examinations_MedicalRecordId");

            migrationBuilder.AddColumn<string>(
                name: "Diagnosis",
                table: "Examinations",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "anamnesis",
                table: "Examinations",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "appointmentID",
                table: "Examinations",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "dateOfExamination",
                table: "Examinations",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "patientVisible",
                table: "Examinations",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "therapyId",
                table: "Examinations",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Examinations",
                table: "Examinations",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Appointment",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DurationInHours = table.Column<double>(nullable: false),
                    DateTime = table.Column<DateTime>(nullable: false),
                    IDpatient = table.Column<string>(nullable: true),
                    IDDoctor = table.Column<string>(nullable: true),
                    IDAppointment = table.Column<string>(nullable: true),
                    PatientsRecordMedicalRecordID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointment", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Appointment_MedicalRecords_PatientsRecordMedicalRecordID",
                        column: x => x.PatientsRecordMedicalRecordID,
                        principalTable: "MedicalRecords",
                        principalColumn: "MedicalRecordID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Therapy",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Therapy", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MedicineTherapys",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MedicineID = table.Column<string>(nullable: true),
                    DurationInDays = table.Column<int>(nullable: false),
                    TimesPerDay = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    TherapyId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicineTherapys", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicineTherapys_Medicines_MedicineID",
                        column: x => x.MedicineID,
                        principalTable: "Medicines",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MedicineTherapys_Therapy_TherapyId",
                        column: x => x.TherapyId,
                        principalTable: "Therapy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Examinations_appointmentID",
                table: "Examinations",
                column: "appointmentID");

            migrationBuilder.CreateIndex(
                name: "IX_Examinations_therapyId",
                table: "Examinations",
                column: "therapyId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointment_PatientsRecordMedicalRecordID",
                table: "Appointment",
                column: "PatientsRecordMedicalRecordID");

            migrationBuilder.CreateIndex(
                name: "IX_MedicineTherapys_MedicineID",
                table: "MedicineTherapys",
                column: "MedicineID");

            migrationBuilder.CreateIndex(
                name: "IX_MedicineTherapys_TherapyId",
                table: "MedicineTherapys",
                column: "TherapyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Examinations_MedicalRecords_MedicalRecordId",
                table: "Examinations",
                column: "MedicalRecordId",
                principalTable: "MedicalRecords",
                principalColumn: "MedicalRecordID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Examinations_Appointment_appointmentID",
                table: "Examinations",
                column: "appointmentID",
                principalTable: "Appointment",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Examinations_Therapy_therapyId",
                table: "Examinations",
                column: "therapyId",
                principalTable: "Therapy",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Examinations_MedicalRecords_MedicalRecordId",
                table: "Examinations");

            migrationBuilder.DropForeignKey(
                name: "FK_Examinations_Appointment_appointmentID",
                table: "Examinations");

            migrationBuilder.DropForeignKey(
                name: "FK_Examinations_Therapy_therapyId",
                table: "Examinations");

            migrationBuilder.DropTable(
                name: "Appointment");

            migrationBuilder.DropTable(
                name: "MedicineTherapys");

            migrationBuilder.DropTable(
                name: "Therapy");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Examinations",
                table: "Examinations");

            migrationBuilder.DropIndex(
                name: "IX_Examinations_appointmentID",
                table: "Examinations");

            migrationBuilder.DropIndex(
                name: "IX_Examinations_therapyId",
                table: "Examinations");

            migrationBuilder.DropColumn(
                name: "Diagnosis",
                table: "Examinations");

            migrationBuilder.DropColumn(
                name: "anamnesis",
                table: "Examinations");

            migrationBuilder.DropColumn(
                name: "appointmentID",
                table: "Examinations");

            migrationBuilder.DropColumn(
                name: "dateOfExamination",
                table: "Examinations");

            migrationBuilder.DropColumn(
                name: "patientVisible",
                table: "Examinations");

            migrationBuilder.DropColumn(
                name: "therapyId",
                table: "Examinations");

            migrationBuilder.RenameTable(
                name: "Examinations",
                newName: "Examination");

            migrationBuilder.RenameColumn(
                name: "MedicalRecordId",
                table: "Examination",
                newName: "MedicalRecordID");

            migrationBuilder.RenameIndex(
                name: "IX_Examinations_MedicalRecordId",
                table: "Examination",
                newName: "IX_Examination_MedicalRecordID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Examination",
                table: "Examination",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Examination_MedicalRecords_MedicalRecordID",
                table: "Examination",
                column: "MedicalRecordID",
                principalTable: "MedicalRecords",
                principalColumn: "MedicalRecordID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
