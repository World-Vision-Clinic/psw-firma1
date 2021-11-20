using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Hospital.Migrations
{
    public partial class AnswersMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SurveyQuestion",
                table: "SurveyQuestion");

            migrationBuilder.DropColumn(
                name: "Answer",
                table: "SurveyQuestion");

            migrationBuilder.DropColumn(
                name: "PatientForeignKey",
                table: "SurveyQuestion");

            migrationBuilder.DropColumn(
                name: "SurveyForeignKey",
                table: "SurveyQuestion");

            migrationBuilder.RenameTable(
                name: "SurveyQuestion",
                newName: "Questions");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Questions",
                table: "Questions",
                column: "Id");

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnsweredQuestions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Questions",
                table: "Questions");

            migrationBuilder.RenameTable(
                name: "Questions",
                newName: "SurveyQuestion");

            migrationBuilder.AddColumn<int>(
                name: "Answer",
                table: "SurveyQuestion",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PatientForeignKey",
                table: "SurveyQuestion",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SurveyForeignKey",
                table: "SurveyQuestion",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SurveyQuestion",
                table: "SurveyQuestion",
                column: "Id");
        }
    }
}
