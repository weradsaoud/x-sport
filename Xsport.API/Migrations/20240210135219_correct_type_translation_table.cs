using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Xsport.API.Migrations
{
    /// <inheritdoc />
    public partial class correct_type_translation_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Types_TypeTranslations_TypeTranslationId",
                table: "Types");

            migrationBuilder.DropTable(
                name: "LanguageTypeTranslation");

            migrationBuilder.DropIndex(
                name: "IX_Types_TypeTranslationId",
                table: "Types");

            migrationBuilder.DropColumn(
                name: "TypeTranslationId",
                table: "Types");

            migrationBuilder.CreateIndex(
                name: "IX_TypeTranslations_LanguageId",
                table: "TypeTranslations",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_TypeTranslations_TypeId",
                table: "TypeTranslations",
                column: "TypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_TypeTranslations_Languages_LanguageId",
                table: "TypeTranslations",
                column: "LanguageId",
                principalTable: "Languages",
                principalColumn: "LanguageId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TypeTranslations_Types_TypeId",
                table: "TypeTranslations",
                column: "TypeId",
                principalTable: "Types",
                principalColumn: "TypeId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TypeTranslations_Languages_LanguageId",
                table: "TypeTranslations");

            migrationBuilder.DropForeignKey(
                name: "FK_TypeTranslations_Types_TypeId",
                table: "TypeTranslations");

            migrationBuilder.DropIndex(
                name: "IX_TypeTranslations_LanguageId",
                table: "TypeTranslations");

            migrationBuilder.DropIndex(
                name: "IX_TypeTranslations_TypeId",
                table: "TypeTranslations");

            migrationBuilder.AddColumn<long>(
                name: "TypeTranslationId",
                table: "Types",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "LanguageTypeTranslation",
                columns: table => new
                {
                    LanguagesLanguageId = table.Column<long>(type: "bigint", nullable: false),
                    TypeTranslationsTypeTranslationId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LanguageTypeTranslation", x => new { x.LanguagesLanguageId, x.TypeTranslationsTypeTranslationId });
                    table.ForeignKey(
                        name: "FK_LanguageTypeTranslation_Languages_LanguagesLanguageId",
                        column: x => x.LanguagesLanguageId,
                        principalTable: "Languages",
                        principalColumn: "LanguageId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LanguageTypeTranslation_TypeTranslations_TypeTranslationsTy~",
                        column: x => x.TypeTranslationsTypeTranslationId,
                        principalTable: "TypeTranslations",
                        principalColumn: "TypeTranslationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Types_TypeTranslationId",
                table: "Types",
                column: "TypeTranslationId");

            migrationBuilder.CreateIndex(
                name: "IX_LanguageTypeTranslation_TypeTranslationsTypeTranslationId",
                table: "LanguageTypeTranslation",
                column: "TypeTranslationsTypeTranslationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Types_TypeTranslations_TypeTranslationId",
                table: "Types",
                column: "TypeTranslationId",
                principalTable: "TypeTranslations",
                principalColumn: "TypeTranslationId");
        }
    }
}
