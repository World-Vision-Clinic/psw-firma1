using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Integration.Migrations
{
    public partial class DateRangeMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ToDate",
                table: "News",
                newName: "DateRange_ToDate");

            migrationBuilder.RenameColumn(
                name: "FromDate",
                table: "News",
                newName: "DateRange_FromDate");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateRange_ToDate",
                table: "News",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateRange_FromDate",
                table: "News",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateRange_ToDate",
                table: "News",
                newName: "ToDate");

            migrationBuilder.RenameColumn(
                name: "DateRange_FromDate",
                table: "News",
                newName: "FromDate");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ToDate",
                table: "News",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "FromDate",
                table: "News",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);
        }
    }
}
