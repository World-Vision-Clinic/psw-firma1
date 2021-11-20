using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hospital.Migrations
{
    public partial class SurveyUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Surveys_SurveyId",
                table: "Questions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Questions",
                table: "Questions");

            migrationBuilder.DropIndex(
                name: "IX_Questions_SurveyId",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "QuestionId",
                table: "Surveys");

            migrationBuilder.DropColumn(
                name: "SurveyId",
                table: "Questions");

            migrationBuilder.RenameTable(
                name: "Questions",
                newName: "SurveyQuestion");

            migrationBuilder.AddColumn<int>(
                name: "PatientForeignKey",
                table: "SurveyQuestion",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Section",
                table: "SurveyQuestion",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SurveyQuestion",
                table: "SurveyQuestion",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SurveyQuestion",
                table: "SurveyQuestion");

            migrationBuilder.DropColumn(
                name: "PatientForeignKey",
                table: "SurveyQuestion");

            migrationBuilder.DropColumn(
                name: "Section",
                table: "SurveyQuestion");

            migrationBuilder.RenameTable(
                name: "SurveyQuestion",
                newName: "Questions");

            migrationBuilder.AddColumn<List<int>>(
                name: "QuestionId",
                table: "Surveys",
                type: "integer[]",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SurveyId",
                table: "Questions",
                type: "integer",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Questions",
                table: "Questions",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_SurveyId",
                table: "Questions",
                column: "SurveyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Surveys_SurveyId",
                table: "Questions",
                column: "SurveyId",
                principalTable: "Surveys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
