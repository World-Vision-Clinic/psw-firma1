using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Hospital.Migrations
{
    public partial class Manager : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Managers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserName = table.Column<string>(type: "text", nullable: true),
                    Password = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Managers", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Surveys",
                keyColumn: "IdSurvey",
                keyValue: 1,
                column: "CreationDate",
                value: new DateTime(2021, 12, 17, 22, 43, 57, 983, DateTimeKind.Local).AddTicks(9890));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Managers");

            migrationBuilder.UpdateData(
                table: "Surveys",
                keyColumn: "IdSurvey",
                keyValue: 1,
                column: "CreationDate",
                value: new DateTime(2021, 12, 13, 11, 47, 43, 642, DateTimeKind.Local).AddTicks(3458));
        }
    }
}
