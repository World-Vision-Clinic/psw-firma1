using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Hospital.Migrations
{
    public partial class UpdateDoctorsMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AllEquipment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    Amount = table.Column<int>(type: "integer", nullable: false),
                    RoomId = table.Column<int>(type: "integer", nullable: false),
                    InTransport = table.Column<bool>(type: "boolean", nullable: false),
                    TransportStart = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    TransportEnd = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllEquipment", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Allergens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Allergens", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AnsweredQuestions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SurveyForeignKey = table.Column<int>(type: "integer", nullable: false),
                    PatientForeignKey = table.Column<int>(type: "integer", nullable: false),
                    Question = table.Column<string>(type: "text", nullable: true),
                    Section = table.Column<int>(type: "integer", nullable: false),
                    Answer = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnsweredQuestions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PatientForeignKey = table.Column<int>(type: "integer", nullable: false),
                    DoctorForeignKey = table.Column<int>(type: "integer", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Time = table.Column<TimeSpan>(type: "interval", nullable: false),
                    IsCancelled = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.Id);
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
                name: "Doctors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "text", nullable: true),
                    LastName = table.Column<string>(type: "text", nullable: true),
                    Type = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Feedbacks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Content = table.Column<string>(type: "text", nullable: true),
                    IsPublic = table.Column<bool>(type: "boolean", nullable: false),
                    IsPublishable = table.Column<bool>(type: "boolean", nullable: false),
                    IsAnonymous = table.Column<bool>(type: "boolean", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UserName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feedbacks", x => x.Id);
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
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Level = table.Column<string>(type: "text", nullable: true),
                    Info = table.Column<string>(type: "text", nullable: true),
                    BuildingId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Floors", x => x.Id);
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
                name: "Medicines",
                columns: table => new
                {
                    ID = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    DosageInMg = table.Column<double>(type: "double precision", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    Price = table.Column<double>(type: "double precision", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medicines", x => x.ID);
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

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserName = table.Column<string>(type: "text", nullable: true),
                    Password = table.Column<string>(type: "text", nullable: true),
                    FirstName = table.Column<string>(type: "text", nullable: true),
                    LastName = table.Column<string>(type: "text", nullable: true),
                    EMail = table.Column<string>(type: "text", nullable: true),
                    Token = table.Column<string>(type: "text", nullable: true),
                    Activated = table.Column<bool>(type: "boolean", nullable: false),
                    Gender = table.Column<int>(type: "integer", nullable: false),
                    Jmbg = table.Column<string>(type: "text", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Country = table.Column<string>(type: "text", nullable: true),
                    Address = table.Column<string>(type: "text", nullable: true),
                    City = table.Column<string>(type: "text", nullable: true),
                    Phone = table.Column<string>(type: "text", nullable: true),
                    PreferedDoctor = table.Column<int>(type: "integer", nullable: false),
                    Weight = table.Column<int>(type: "integer", nullable: false),
                    Height = table.Column<int>(type: "integer", nullable: false),
                    BloodType = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Answer = table.Column<int>(type: "integer", nullable: false),
                    IdSurvey = table.Column<int>(type: "integer", nullable: false),
                    Question = table.Column<string>(type: "text", nullable: true),
                    Section = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
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
                    table.PrimaryKey("PK_Rooms", x => x.Id);
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
                name: "Surveys",
                columns: table => new
                {
                    IdSurvey = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IdAppointment = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Surveys", x => x.IdSurvey);
                    table.ForeignKey(
                        name: "FK_Surveys_Appointments_IdAppointment",
                        column: x => x.IdAppointment,
                        principalTable: "Appointments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.InsertData(
                table: "AllEquipment",
                columns: new[] { "Id", "Amount", "InTransport", "Name", "RoomId", "TransportEnd", "TransportStart", "Type" },
                values: new object[,]
                {
                    { 13, 6, false, "Chair", 17, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0 },
                    { 14, 25, false, "Bandage", 5, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 12, 11, false, "Bed", 5, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0 },
                    { 11, 4, false, "Chair", 16, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0 },
                    { 10, 7, false, "Bed", 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0 },
                    { 9, 11, false, "Syringe", 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 8, 1, false, "Operating table", 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0 },
                    { 7, 15, false, "Bandage", 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 6, 23, false, "Infusion", 23, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 5, 2, false, "Operating table", 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0 },
                    { 4, 17, false, "Bandage", 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 3, 11, false, "Infusion", 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 2, 3, false, "Operating table", 23, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0 },
                    { 1, 15, false, "Bandage", 15, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 }
                });

            migrationBuilder.InsertData(
                table: "Appointments",
                columns: new[] { "Id", "Date", "DoctorForeignKey", "IsCancelled", "PatientForeignKey", "Time", "Type" },
                values: new object[] { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, false, 0, new TimeSpan(0, 0, 0, 0, 0), 0 });

            migrationBuilder.InsertData(
                table: "Buildings",
                columns: new[] { "id", "Areaid", "Info", "MapPositionId", "Name" },
                values: new object[,]
                {
                    { 1, null, "Gynecology", 1, "Hospital I" },
                    { 2, null, "", 2, "Hospital II" }
                });

            migrationBuilder.InsertData(
                table: "Doctors",
                columns: new[] { "Id", "FirstName", "LastName", "Type" },
                values: new object[,]
                {
                    { 5, "Ignjat", "Jovic", 0 },
                    { 1, "Slavica", "Matic", 0 },
                    { 2, "Mirko", "Jankovic", 0 },
                    { 3, "Matija", "Popic", 0 },
                    { 4, "Sara", "Tot", 0 },
                    { 19, "Vasilije", "Mit", 0 },
                    { 18, "Luka", "Lisica", 0 },
                    { 17, "Jelena", "Stupar", 0 },
                    { 16, "Savina", "Markovic", 0 },
                    { 6, "Milos", "Matijevic", 0 },
                    { 14, "Petar", "Katic", 0 },
                    { 13, "Mileva", "Nakic", 0 },
                    { 12, "Ivana", "Pekic", 0 },
                    { 11, "Momir", "Njegomir", 0 },
                    { 10, "Lidija", "Lakic", 0 },
                    { 9, "Bojan", "Kraljevic", 0 },
                    { 8, "Iva", "Bojanic", 0 },
                    { 7, "Elena", "Kis", 0 },
                    { 15, "Marijana", "Pantic", 0 }
                });

            migrationBuilder.InsertData(
                table: "FloorLabels",
                columns: new[] { "id", "FloorId", "Text", "X", "Y" },
                values: new object[] { 1, 1, "ENTRANCE", 50, 80 });

            migrationBuilder.InsertData(
                table: "Floors",
                columns: new[] { "Id", "BuildingId", "Info", "Level" },
                values: new object[,]
                {
                    { 1, 1, null, "Ground floor" },
                    { 2, 1, null, "First floor" }
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
                    { 2, false, 1, 260, 195 },
                    { 3, true, 2, 545, 505 },
                    { 4, false, 2, 260, 455 },
                    { 1, true, 1, 545, 80 }
                });

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "Id", "Answer", "IdSurvey", "Question", "Section" },
                values: new object[,]
                {
                    { 1, 0, 1, "Has doctor been polite to you?", 1 },
                    { 12, 0, 1, "How would you rate the professionalism of our staff?", 2 },
                    { 11, 0, 1, "How would you rate the kindness of our staff?", 2 },
                    { 10, 0, 1, "What is your overall satisfaction with our hospital?", 0 },
                    { 9, 0, 1, "How satisfied are you with the services that the hospital provides you?", 0 },
                    { 8, 0, 1, "What is an opportunity to recommend us to your friends and family?", 0 },
                    { 7, 0, 1, "How easy it was to schedule an appointment?", 0 },
                    { 6, 0, 1, "How easy is to use our application?", 0 },
                    { 5, 0, 1, "What is your overall satisfaction with doctor?", 1 },
                    { 4, 0, 1, "How would you rate the doctor's patience with you?", 1 },
                    { 3, 0, 1, "How clearly did the doctor explain you your condition?", 1 },
                    { 2, 0, 1, "How would you rate the professionalism of doctor?", 1 },
                    { 13, 0, 1, "How clearly did the staff explain you some procedures of our hospital?", 2 },
                    { 15, 0, 1, "What is your overall satisfaction with our staff?", 2 },
                    { 14, 0, 1, "How yould you rate to what extent staff was available to you during your visit to the hospital?", 2 }
                });

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "Css", "DoctorId", "DoorExist", "DoorX", "DoorY", "FloorId", "Height", "Name", "Purpose", "Vertical", "Width", "X", "Y" },
                values: new object[,]
                {
                    { 16, "room", 13, true, 420, 248, 2, 100, "DOCTOR'S OFFICE 3", "", false, 150, 320, 150 },
                    { 1, "room room-cadetblue", 1, true, 148, 285, 1, 190, "OPERATING ROOM 1", "", true, 150, 0, 150 },
                    { 2, "room room-cadetblue", 2, true, 220, 248, 1, 100, "OPERATING ROOM 2", "", false, 150, 160, 150 },
                    { 3, "room room-cadetblue", 3, true, 370, 248, 1, 100, "OPERATING ROOM 3", "", false, 150, 320, 150 },
                    { 4, "room room-cadetblue", 4, true, 520, 248, 1, 100, "ROOM 1", "", false, 170, 480, 150 },
                    { 5, "room room-cadetblue", 5, true, 680, 248, 1, 100, "ROOM 2", "", false, 180, 660, 150 },
                    { 6, "room room-cadetblue", 6, true, 728, 290, 1, 100, "OFFICE 1", "", true, 110, 730, 260 },
                    { 8, "room room-cadetblue", -1, true, 728, 485, 1, 60, "TOILET", "", true, 110, 730, 470 },
                    { 9, "room room-cadetblue", -1, true, 728, 555, 1, 60, "TOILET", "", true, 110, 730, 540 },
                    { 10, "room room-cadetblue", 7, true, 148, 419, 1, 190, "OPERATING ROOM 4", "", true, 150, 0, 410 },
                    { 11, "room room-cadetblue", 8, true, 200, 498, 1, 100, "ROOM 3", "", false, 100, 160, 500 },
                    { 15, "room", 12, true, 260, 248, 2, 100, "DOCTOR'S OFFICE 2", "", false, 150, 160, 150 },
                    { 12, "room room-cadetblue", 9, true, 315, 498, 1, 100, "ROOM 4", "", false, 150, 270, 500 },
                    { 14, "room", 11, true, 100, 248, 2, 100, "DOCTOR'S OFFICE 1", "", false, 150, 0, 150 },
                    { 26, "room", 19, true, 400, 398, 2, 100, "OPERATING ROOM 1", "", false, 580, 0, 300 },
                    { 25, "room", 18, true, 350, 498, 2, 100, "OPERATING ROOM 3", "", false, 300, 270, 500 },
                    { 24, "room", 17, true, 200, 498, 2, 100, "ROOM 2", "", false, 100, 160, 500 },
                    { 23, "room", 16, true, 148, 419, 2, 190, "OPERATING ROOM 2", "", true, 150, 0, 410 },
                    { 22, "room", -1, true, 728, 555, 2, 60, "TOILET", "", true, 110, 730, 540 },
                    { 21, "room", -1, true, 728, 485, 2, 60, "TOILET", "", true, 110, 730, 470 },
                    { 20, "staircase", -1, false, 728, 290, 2, 90, "LIFT", "", false, 150, 690, 370 },
                    { 19, "room", -1, false, 728, 290, 2, 104, "STAIRS", "", true, 70, 770, 258 },
                    { 18, "room", 15, true, 680, 248, 2, 100, "ROOM 1", "", false, 180, 660, 150 },
                    { 17, "room", 14, true, 595, 248, 2, 100, "DOCTOR'S OFFICE 4", "", false, 170, 480, 150 },
                    { 13, "room room-cadetblue", 10, true, 475, 498, 1, 100, "ROOM 5", "", false, 150, 430, 500 },
                    { 7, "staircase", -1, false, 728, 290, 1, 90, "LIFT", "", false, 150, 690, 370 }
                });

            migrationBuilder.InsertData(
                table: "Surveys",
                columns: new[] { "IdSurvey", "CreationDate", "IdAppointment" },
                values: new object[] { 1, new DateTime(2021, 12, 13, 11, 47, 43, 642, DateTimeKind.Local).AddTicks(3458), 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Buildings_Areaid",
                table: "Buildings",
                column: "Areaid");

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

            migrationBuilder.CreateIndex(
                name: "IX_Surveys_IdAppointment",
                table: "Surveys",
                column: "IdAppointment");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AllEquipment");

            migrationBuilder.DropTable(
                name: "AnsweredQuestions");

            migrationBuilder.DropTable(
                name: "Buildings");

            migrationBuilder.DropTable(
                name: "Doctors");

            migrationBuilder.DropTable(
                name: "Examinations");

            migrationBuilder.DropTable(
                name: "Feedbacks");

            migrationBuilder.DropTable(
                name: "FloorLabels");

            migrationBuilder.DropTable(
                name: "Floors");

            migrationBuilder.DropTable(
                name: "Ingredient");

            migrationBuilder.DropTable(
                name: "MapPositions");

            migrationBuilder.DropTable(
                name: "MedicineTherapy");

            migrationBuilder.DropTable(
                name: "OutsideDoors");

            migrationBuilder.DropTable(
                name: "Parkings");

            migrationBuilder.DropTable(
                name: "PatientAllergens");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropTable(
                name: "Surveys");

            migrationBuilder.DropTable(
                name: "Areas");

            migrationBuilder.DropTable(
                name: "MedicalRecords");

            migrationBuilder.DropTable(
                name: "Medicines");

            migrationBuilder.DropTable(
                name: "Therapies");

            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.DropTable(
                name: "Allergens");

            migrationBuilder.DropTable(
                name: "Patients");
        }
    }
}
