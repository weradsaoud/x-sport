using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Xsport.API.Migrations
{
    /// <inheritdoc />
    public partial class add_sport_translat_language_DbContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SportTranslation_Language_LanguageId",
                table: "SportTranslation");

            migrationBuilder.DropForeignKey(
                name: "FK_SportTranslation_Sports_SportId",
                table: "SportTranslation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SportTranslation",
                table: "SportTranslation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Language",
                table: "Language");

            migrationBuilder.RenameTable(
                name: "SportTranslation",
                newName: "SportTranslations");

            migrationBuilder.RenameTable(
                name: "Language",
                newName: "Languages");

            migrationBuilder.RenameIndex(
                name: "IX_SportTranslation_SportId",
                table: "SportTranslations",
                newName: "IX_SportTranslations_SportId");

            migrationBuilder.RenameIndex(
                name: "IX_SportTranslation_LanguageId",
                table: "SportTranslations",
                newName: "IX_SportTranslations_LanguageId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SportTranslations",
                table: "SportTranslations",
                column: "SportTranslationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Languages",
                table: "Languages",
                column: "LanguageId");

            migrationBuilder.AddForeignKey(
                name: "FK_SportTranslations_Languages_LanguageId",
                table: "SportTranslations",
                column: "LanguageId",
                principalTable: "Languages",
                principalColumn: "LanguageId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SportTranslations_Sports_SportId",
                table: "SportTranslations",
                column: "SportId",
                principalTable: "Sports",
                principalColumn: "SportId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SportTranslations_Languages_LanguageId",
                table: "SportTranslations");

            migrationBuilder.DropForeignKey(
                name: "FK_SportTranslations_Sports_SportId",
                table: "SportTranslations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SportTranslations",
                table: "SportTranslations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Languages",
                table: "Languages");

            migrationBuilder.RenameTable(
                name: "SportTranslations",
                newName: "SportTranslation");

            migrationBuilder.RenameTable(
                name: "Languages",
                newName: "Language");

            migrationBuilder.RenameIndex(
                name: "IX_SportTranslations_SportId",
                table: "SportTranslation",
                newName: "IX_SportTranslation_SportId");

            migrationBuilder.RenameIndex(
                name: "IX_SportTranslations_LanguageId",
                table: "SportTranslation",
                newName: "IX_SportTranslation_LanguageId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SportTranslation",
                table: "SportTranslation",
                column: "SportTranslationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Language",
                table: "Language",
                column: "LanguageId");

            migrationBuilder.AddForeignKey(
                name: "FK_SportTranslation_Language_LanguageId",
                table: "SportTranslation",
                column: "LanguageId",
                principalTable: "Language",
                principalColumn: "LanguageId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SportTranslation_Sports_SportId",
                table: "SportTranslation",
                column: "SportId",
                principalTable: "Sports",
                principalColumn: "SportId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
