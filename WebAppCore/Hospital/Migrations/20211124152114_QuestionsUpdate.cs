using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Hospital.Migrations
{
    public partial class QuestionsUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "Feedbacks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Content = table.Column<string>(type: "text", nullable: true),
                    isPublic = table.Column<bool>(type: "boolean", nullable: false),
                    isPublishable = table.Column<bool>(type: "boolean", nullable: false),
                    isAnonymous = table.Column<bool>(type: "boolean", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UserName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feedbacks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserName = table.Column<string>(type: "text", nullable: true),
                    Password = table.Column<string>(type: "text", nullable: true),
                    EMail = table.Column<string>(type: "text", nullable: true),
                    Token = table.Column<string>(type: "text", nullable: true),
                    Activated = table.Column<bool>(type: "boolean", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Gender = table.Column<int>(type: "integer", nullable: false),
                    Residence = table.Column<string>(type: "text", nullable: true),
                    ContactPhone = table.Column<string>(type: "text", nullable: true),
                    DoctorName = table.Column<string>(type: "text", nullable: true),
                    Height = table.Column<int>(type: "integer", nullable: false),
                    Weight = table.Column<int>(type: "integer", nullable: false),
                    BloodType = table.Column<string>(type: "text", nullable: true)
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

            migrationBuilder.InsertData(
                table: "Appointments",
                columns: new[] { "Id", "Date", "DoctorForeignKey", "PatientForeignKey", "Time", "Type" },
                values: new object[] { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 0, new TimeSpan(0, 0, 0, 0, 0), 0 });

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "Id", "Answer", "IdSurvey", "Question", "Section" },
                values: new object[,]
                {
                    { 1, 0, 1, "Has doctor been polite to you?", 1 },
                    { 2, 0, 1, "How would you rate the professionalism of doctor?", 1 },
                    { 3, 0, 1, "How clearly did the doctor explain you your condition?", 1 },
                    { 4, 0, 1, "How would you rate the doctor's patience with you?", 1 },
                    { 5, 0, 1, "What is your overall satisfaction with doctor?", 1 },
                    { 6, 0, 1, "How easy is to use our application?", 0 },
                    { 7, 0, 1, "How easy it was to schedule an appointment?", 0 },
                    { 8, 0, 1, "What is an opportunity to recommend us to your friends and family?", 0 },
                    { 9, 0, 1, "How satisfied are you with the services that the hospital provides you?", 0 },
                    { 10, 0, 1, "What is your overall satisfaction with our hospital?", 0 },
                    { 11, 0, 1, "How would you rate the kindness of our staff?", 2 },
                    { 12, 0, 1, "How would you rate the professionalism of our staff?", 2 },
                    { 13, 0, 1, "How clearly did the staff explain you some procedures of our hospital?", 2 },
                    { 14, 0, 1, "How yould you rate to what extent staff was available to you during your visit to the hospital?", 2 },
                    { 15, 0, 1, "What is your overall satisfaction with our staff?", 2 }
                });

            migrationBuilder.InsertData(
                table: "Surveys",
                columns: new[] { "IdSurvey", "CreationDate", "IdAppointment" },
                values: new object[] { 1, new DateTime(2021, 11, 24, 16, 21, 13, 235, DateTimeKind.Local).AddTicks(3363), 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Surveys_IdAppointment",
                table: "Surveys",
                column: "IdAppointment");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnsweredQuestions");

            migrationBuilder.DropTable(
                name: "Feedbacks");

            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "Surveys");

            migrationBuilder.DropTable(
                name: "Appointments");
        }
    }
}
