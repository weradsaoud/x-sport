using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Xsport.API.Migrations
{
    /// <inheritdoc />
    public partial class add_Floor_tables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AcademyWorkingDays_AcademyId",
                table: "AcademyWorkingDays");

            migrationBuilder.AddColumn<long>(
                name: "FloorId",
                table: "Stadiums",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "Floors",
                columns: table => new
                {
                    FloorId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Floors", x => x.FloorId);
                });

            migrationBuilder.CreateTable(
                name: "FloorTranslations",
                columns: table => new
                {
                    FloorTranslationId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    FloorId = table.Column<long>(type: "bigint", nullable: false),
                    LanguageId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FloorTranslations", x => x.FloorTranslationId);
                    table.ForeignKey(
                        name: "FK_FloorTranslations_Floors_FloorId",
                        column: x => x.FloorId,
                        principalTable: "Floors",
                        principalColumn: "FloorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FloorTranslations_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "LanguageId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Stadiums_FloorId",
                table: "Stadiums",
                column: "FloorId");

            migrationBuilder.CreateIndex(
                name: "IX_AcademyWorkingDays_AcademyId_WorkingDayId",
                table: "AcademyWorkingDays",
                columns: new[] { "AcademyId", "WorkingDayId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FloorTranslations_FloorId",
                table: "FloorTranslations",
                column: "FloorId");

            migrationBuilder.CreateIndex(
                name: "IX_FloorTranslations_LanguageId",
                table: "FloorTranslations",
                column: "LanguageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stadiums_Floors_FloorId",
                table: "Stadiums",
                column: "FloorId",
                principalTable: "Floors",
                principalColumn: "FloorId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stadiums_Floors_FloorId",
                table: "Stadiums");

            migrationBuilder.DropTable(
                name: "FloorTranslations");

            migrationBuilder.DropTable(
                name: "Floors");

            migrationBuilder.DropIndex(
                name: "IX_Stadiums_FloorId",
                table: "Stadiums");

            migrationBuilder.DropIndex(
                name: "IX_AcademyWorkingDays_AcademyId_WorkingDayId",
                table: "AcademyWorkingDays");

            migrationBuilder.DropColumn(
                name: "FloorId",
                table: "Stadiums");

            migrationBuilder.CreateIndex(
                name: "IX_AcademyWorkingDays_AcademyId",
                table: "AcademyWorkingDays",
                column: "AcademyId");
        }
    }
}
