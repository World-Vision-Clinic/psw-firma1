using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Hospital.Migrations
{
    public partial class UpdateMigration : Migration
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
                    Time = table.Column<TimeSpan>(type: "interval", nullable: false)
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
                    LastName = table.Column<string>(type: "text", nullable: true)
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

            migrationBuilder.InsertData(
                table: "AllEquipment",
                columns: new[] { "Id", "Amount", "InTransport", "Name", "RoomId", "TransportEnd", "TransportStart", "Type" },
                values: new object[,]
                {
                    { 9, 11, false, "Syringe", 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 14, 25, false, "Bandage", 5, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 13, 6, false, "Chair", 17, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0 },
                    { 12, 11, false, "Bed", 5, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0 },
                    { 11, 4, false, "Chair", 16, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0 },
                    { 10, 7, false, "Bed", 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0 },
                    { 8, 1, false, "Operating table", 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0 },
                    { 7, 15, false, "Bandage", 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 6, 23, false, "Infusion", 23, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 5, 2, false, "Operating table", 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0 },
                    { 4, 17, false, "Bandage", 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 1, 15, false, "Bandage", 15, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 2, 3, false, "Operating table", 23, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0 },
                    { 3, 11, false, "Infusion", 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 }
                });

            migrationBuilder.InsertData(
                table: "Appointments",
                columns: new[] { "Id", "Date", "DoctorForeignKey", "PatientForeignKey", "Time", "Type" },
                values: new object[] { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 0, new TimeSpan(0, 0, 0, 0, 0), 0 });

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
                    { 4, false, 2, 260, 455 },
                    { 3, true, 2, 545, 505 },
                    { 1, true, 1, 545, 80 },
                    { 2, false, 1, 260, 195 }
                });

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "Id", "Answer", "IdSurvey", "Question", "Section" },
                values: new object[,]
                {
                    { 13, 0, 1, "How clearly did the staff explain you some procedures of our hospital?", 2 },
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
                    { 1, 0, 1, "Has doctor been polite to you?", 1 },
                    { 12, 0, 1, "How would you rate the professionalism of our staff?", 2 },
                    { 15, 0, 1, "What is your overall satisfaction with our staff?", 2 },
                    { 14, 0, 1, "How yould you rate to what extent staff was available to you during your visit to the hospital?", 2 }
                });

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "Css", "DoctorId", "DoorExist", "DoorX", "DoorY", "FloorId", "Height", "Name", "Purpose", "Vertical", "Width", "X", "Y" },
                values: new object[,]
                {
                    { 16, "room", -1, true, 420, 248, 2, 100, "DOCTOR'S OFFICE 3", "", false, 150, 320, 150 },
                    { 1, "room room-cadetblue", -1, true, 148, 285, 1, 190, "OPERATING ROOM 1", "", true, 150, 0, 150 },
                    { 2, "room room-cadetblue", -1, true, 220, 248, 1, 100, "OPERATING ROOM 2", "", false, 150, 160, 150 },
                    { 3, "room room-cadetblue", -1, true, 370, 248, 1, 100, "OPERATING ROOM 3", "", false, 150, 320, 150 },
                    { 4, "room room-cadetblue", -1, true, 520, 248, 1, 100, "ROOM 1", "", false, 170, 480, 150 },
                    { 5, "room room-cadetblue", -1, true, 680, 248, 1, 100, "ROOM 2", "", false, 180, 660, 150 },
                    { 6, "room room-cadetblue", -1, true, 728, 290, 1, 100, "OFFICE 1", "", true, 110, 730, 260 },
                    { 7, "staircase", -1, false, 728, 290, 1, 90, "LIFT", "", false, 150, 690, 370 },
                    { 8, "room room-cadetblue", -1, true, 728, 485, 1, 60, "TOILET", "", true, 110, 730, 470 },
                    { 10, "room room-cadetblue", -1, true, 148, 419, 1, 190, "OPERATING ROOM 4", "", true, 150, 0, 410 },
                    { 11, "room room-cadetblue", -1, true, 200, 498, 1, 100, "ROOM 3", "", false, 100, 160, 500 },
                    { 15, "room", -1, true, 260, 248, 2, 100, "DOCTOR'S OFFICE 2", "", false, 150, 160, 150 },
                    { 12, "room room-cadetblue", -1, true, 315, 498, 1, 100, "ROOM 4", "", false, 150, 270, 500 },
                    { 14, "room", -1, true, 100, 248, 2, 100, "DOCTOR'S OFFICE 1", "", false, 150, 0, 150 },
                    { 26, "room", -1, true, 400, 398, 2, 100, "OPERATING ROOM 1", "", false, 580, 0, 300 },
                    { 25, "room", -1, true, 350, 498, 2, 100, "OPERATING ROOM 3", "", false, 300, 270, 500 },
                    { 24, "room", -1, true, 200, 498, 2, 100, "ROOM 2", "", false, 100, 160, 500 },
                    { 23, "room", -1, true, 148, 419, 2, 190, "OPERATING ROOM 2", "", true, 150, 0, 410 },
                    { 22, "room", -1, true, 728, 555, 2, 60, "TOILET", "", true, 110, 730, 540 },
                    { 21, "room", -1, true, 728, 485, 2, 60, "TOILET", "", true, 110, 730, 470 },
                    { 20, "staircase", -1, false, 728, 290, 2, 90, "LIFT", "", false, 150, 690, 370 },
                    { 19, "room", -1, false, 728, 290, 2, 104, "STAIRS", "", true, 70, 770, 258 },
                    { 18, "room", -1, true, 680, 248, 2, 100, "ROOM 1", "", false, 180, 660, 150 },
                    { 17, "room", -1, true, 595, 248, 2, 100, "DOCTOR'S OFFICE 4", "", false, 170, 480, 150 },
                    { 13, "room room-cadetblue", -1, true, 475, 498, 1, 100, "ROOM 5", "", false, 150, 430, 500 },
                    { 9, "room room-cadetblue", -1, true, 728, 555, 1, 60, "TOILET", "", true, 110, 730, 540 }
                });

            migrationBuilder.InsertData(
                table: "Surveys",
                columns: new[] { "IdSurvey", "CreationDate", "IdAppointment" },
                values: new object[] { 1, new DateTime(2021, 11, 29, 22, 43, 18, 896, DateTimeKind.Local).AddTicks(8286), 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Buildings_Areaid",
                table: "Buildings",
                column: "Areaid");

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
                name: "Allergens");

            migrationBuilder.DropTable(
                name: "AnsweredQuestions");

            migrationBuilder.DropTable(
                name: "Buildings");

            migrationBuilder.DropTable(
                name: "Doctors");

            migrationBuilder.DropTable(
                name: "Feedbacks");

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
                name: "PatientAllergens");

            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropTable(
                name: "Surveys");

            migrationBuilder.DropTable(
                name: "Areas");

            migrationBuilder.DropTable(
                name: "Appointments");
        }
    }
}
