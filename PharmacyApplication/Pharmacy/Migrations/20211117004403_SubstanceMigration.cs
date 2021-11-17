using Microsoft.EntityFrameworkCore.Migrations;

namespace Pharmacy.Migrations
{
    public partial class SubstanceMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Substance",
                table: "Medicines");

            migrationBuilder.CreateTable(
                name: "Substances",
                columns: table => new
                {
                    SubstanceId = table.Column<long>(nullable: false),
                    MedicineId = table.Column<long>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Quantity = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Substances", x => new { x.SubstanceId, x.MedicineId });
                    table.ForeignKey(
                        name: "FK_Substances_Medicines_MedicineId",
                        column: x => x.MedicineId,
                        principalTable: "Medicines",
                        principalColumn: "MedicineId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Substances_MedicineId",
                table: "Substances",
                column: "MedicineId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Substances");

            migrationBuilder.AddColumn<string>(
                name: "Substance",
                table: "Medicines",
                type: "text",
                nullable: true);
        }
    }
}
