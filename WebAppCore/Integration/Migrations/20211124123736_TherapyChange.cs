using Microsoft.EntityFrameworkCore.Migrations;

namespace Integration.Migrations
{
    public partial class TherapyChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Examinations_Therapy_therapyId",
                table: "Examinations");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicineTherapys_Therapy_TherapyId",
                table: "MedicineTherapys");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Therapy",
                table: "Therapy");

            migrationBuilder.RenameTable(
                name: "Therapy",
                newName: "Therapies");

            migrationBuilder.RenameColumn(
                name: "therapyId",
                table: "Examinations",
                newName: "TherapyId");

            migrationBuilder.RenameIndex(
                name: "IX_Examinations_therapyId",
                table: "Examinations",
                newName: "IX_Examinations_TherapyId");

            migrationBuilder.AlterColumn<int>(
                name: "TherapyId",
                table: "MedicineTherapys",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TherapyId",
                table: "Examinations",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MedicineId",
                table: "Therapies",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Therapies",
                table: "Therapies",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Examinations_Therapies_TherapyId",
                table: "Examinations",
                column: "TherapyId",
                principalTable: "Therapies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicineTherapys_Therapies_TherapyId",
                table: "MedicineTherapys",
                column: "TherapyId",
                principalTable: "Therapies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Examinations_Therapies_TherapyId",
                table: "Examinations");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicineTherapys_Therapies_TherapyId",
                table: "MedicineTherapys");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Therapies",
                table: "Therapies");

            migrationBuilder.RenameTable(
                name: "Therapies",
                newName: "Therapy");

            migrationBuilder.RenameColumn(
                name: "TherapyId",
                table: "Examinations",
                newName: "therapyId");

            migrationBuilder.RenameIndex(
                name: "IX_Examinations_TherapyId",
                table: "Examinations",
                newName: "IX_Examinations_therapyId");

            migrationBuilder.AlterColumn<int>(
                name: "TherapyId",
                table: "MedicineTherapys",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "therapyId",
                table: "Examinations",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "MedicineId",
                table: "Therapy",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Therapy",
                table: "Therapy",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Examinations_Therapy_therapyId",
                table: "Examinations",
                column: "therapyId",
                principalTable: "Therapy",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicineTherapys_Therapy_TherapyId",
                table: "MedicineTherapys",
                column: "TherapyId",
                principalTable: "Therapy",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
