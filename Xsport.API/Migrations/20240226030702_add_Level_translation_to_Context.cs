using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Xsport.API.Migrations
{
    /// <inheritdoc />
    public partial class add_Level_translation_to_Context : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LevelTranslation_Languages_LanguageId",
                table: "LevelTranslation");

            migrationBuilder.DropForeignKey(
                name: "FK_LevelTranslation_Levels_LevelId",
                table: "LevelTranslation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LevelTranslation",
                table: "LevelTranslation");

            migrationBuilder.RenameTable(
                name: "LevelTranslation",
                newName: "LevelTranslations");

            migrationBuilder.RenameIndex(
                name: "IX_LevelTranslation_LevelId",
                table: "LevelTranslations",
                newName: "IX_LevelTranslations_LevelId");

            migrationBuilder.RenameIndex(
                name: "IX_LevelTranslation_LanguageId",
                table: "LevelTranslations",
                newName: "IX_LevelTranslations_LanguageId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LevelTranslations",
                table: "LevelTranslations",
                column: "LevelTranslationId");

            migrationBuilder.AddForeignKey(
                name: "FK_LevelTranslations_Languages_LanguageId",
                table: "LevelTranslations",
                column: "LanguageId",
                principalTable: "Languages",
                principalColumn: "LanguageId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LevelTranslations_Levels_LevelId",
                table: "LevelTranslations",
                column: "LevelId",
                principalTable: "Levels",
                principalColumn: "LevelId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LevelTranslations_Languages_LanguageId",
                table: "LevelTranslations");

            migrationBuilder.DropForeignKey(
                name: "FK_LevelTranslations_Levels_LevelId",
                table: "LevelTranslations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LevelTranslations",
                table: "LevelTranslations");

            migrationBuilder.RenameTable(
                name: "LevelTranslations",
                newName: "LevelTranslation");

            migrationBuilder.RenameIndex(
                name: "IX_LevelTranslations_LevelId",
                table: "LevelTranslation",
                newName: "IX_LevelTranslation_LevelId");

            migrationBuilder.RenameIndex(
                name: "IX_LevelTranslations_LanguageId",
                table: "LevelTranslation",
                newName: "IX_LevelTranslation_LanguageId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LevelTranslation",
                table: "LevelTranslation",
                column: "LevelTranslationId");

            migrationBuilder.AddForeignKey(
                name: "FK_LevelTranslation_Languages_LanguageId",
                table: "LevelTranslation",
                column: "LanguageId",
                principalTable: "Languages",
                principalColumn: "LanguageId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LevelTranslation_Levels_LevelId",
                table: "LevelTranslation",
                column: "LevelId",
                principalTable: "Levels",
                principalColumn: "LevelId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
