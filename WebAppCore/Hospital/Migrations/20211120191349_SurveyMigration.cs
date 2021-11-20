using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hospital.Migrations
{
    public partial class SurveyMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Surveys_SurveyForeignKey",
                table: "Questions");

            migrationBuilder.DropIndex(
                name: "IX_Questions_SurveyForeignKey",
                table: "Questions");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Surveys_SurveyId",
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

            migrationBuilder.CreateIndex(
                name: "IX_Questions_SurveyForeignKey",
                table: "Questions",
                column: "SurveyForeignKey");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Surveys_SurveyForeignKey",
                table: "Questions",
                column: "SurveyForeignKey",
                principalTable: "Surveys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
