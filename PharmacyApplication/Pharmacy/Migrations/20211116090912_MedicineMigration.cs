using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Pharmacy.Migrations
{
    public partial class MedicineMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Medicines",
                columns: table => new
                {
                    MedicineId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MedicineName = table.Column<string>(nullable: true),
                    Manufacturer = table.Column<string>(nullable: true),
                    SideEffects = table.Column<string>(nullable: true),
                    Usage = table.Column<string>(nullable: true),
                    Weigth = table.Column<double>(nullable: false),
                    MainPrecautions = table.Column<string>(nullable: true),
                    PotentialDangers = table.Column<string>(nullable: true),
                    Substance = table.Column<string>(nullable: true),
                    Quantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medicines", x => x.MedicineId);
                });

            migrationBuilder.CreateTable(
                name: "SubstituteMedicines",
                columns: table => new
                {
                    MedicineId = table.Column<long>(nullable: false),
                    SubstituteId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubstituteMedicines", x => new { x.MedicineId, x.SubstituteId });
                    table.ForeignKey(
                        name: "FK_SubstituteMedicines_Medicines_MedicineId",
                        column: x => x.MedicineId,
                        principalTable: "Medicines",
                        principalColumn: "MedicineId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubstituteMedicines_Medicines_SubstituteId",
                        column: x => x.SubstituteId,
                        principalTable: "Medicines",
                        principalColumn: "MedicineId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SubstituteMedicines_SubstituteId",
                table: "SubstituteMedicines",
                column: "SubstituteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubstituteMedicines");

            migrationBuilder.DropTable(
                name: "Medicines");
        }
    }
}
