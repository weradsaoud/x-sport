using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Xsport.API.Migrations
{
    /// <inheritdoc />
    public partial class add_user_preferences_tables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "TypeTranslationId",
                table: "Types",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SportPreferences",
                columns: table => new
                {
                    SportPreferenceId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SportId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SportPreferences", x => x.SportPreferenceId);
                    table.ForeignKey(
                        name: "FK_SportPreferences_Sports_SportId",
                        column: x => x.SportId,
                        principalTable: "Sports",
                        principalColumn: "SportId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TypeTranslations",
                columns: table => new
                {
                    TypeTranslationId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    TypeId = table.Column<long>(type: "bigint", nullable: false),
                    LanguageId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeTranslations", x => x.TypeTranslationId);
                });

            migrationBuilder.CreateTable(
                name: "UserSport",
                columns: table => new
                {
                    UserSportId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Points = table.Column<int>(type: "integer", nullable: false),
                    XsportUserId = table.Column<long>(type: "bigint", nullable: false),
                    SportId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSport", x => x.UserSportId);
                    table.ForeignKey(
                        name: "FK_UserSport_Sports_SportId",
                        column: x => x.SportId,
                        principalTable: "Sports",
                        principalColumn: "SportId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserSport_XsportUsers_XsportUserId",
                        column: x => x.XsportUserId,
                        principalTable: "XsportUsers",
                        principalColumn: "XsportUserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SportPreferenceTranslations",
                columns: table => new
                {
                    SportPreferenceTranslationId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    SportPreferenceId = table.Column<long>(type: "bigint", nullable: false),
                    LanguageId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SportPreferenceTranslations", x => x.SportPreferenceTranslationId);
                    table.ForeignKey(
                        name: "FK_SportPreferenceTranslations_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "LanguageId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SportPreferenceTranslations_SportPreferences_SportPreferenc~",
                        column: x => x.SportPreferenceId,
                        principalTable: "SportPreferences",
                        principalColumn: "SportPreferenceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SportPreferenceValues",
                columns: table => new
                {
                    SportPreferenceValueId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SportPreferenceId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SportPreferenceValues", x => x.SportPreferenceValueId);
                    table.ForeignKey(
                        name: "FK_SportPreferenceValues_SportPreferences_SportPreferenceId",
                        column: x => x.SportPreferenceId,
                        principalTable: "SportPreferences",
                        principalColumn: "SportPreferenceId",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateTable(
                name: "SportPreferenceValueTranslations",
                columns: table => new
                {
                    SportPreferenceValueTranslationId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SportPreferenceValueId = table.Column<long>(type: "bigint", nullable: false),
                    LanguageId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SportPreferenceValueTranslations", x => x.SportPreferenceValueTranslationId);
                    table.ForeignKey(
                        name: "FK_SportPreferenceValueTranslations_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "LanguageId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SportPreferenceValueTranslations_SportPreferenceValues_Spor~",
                        column: x => x.SportPreferenceValueId,
                        principalTable: "SportPreferenceValues",
                        principalColumn: "SportPreferenceValueId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserSportPreferences",
                columns: table => new
                {
                    UserSportPreferenceId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserSportId = table.Column<long>(type: "bigint", nullable: false),
                    SportPreferenceId = table.Column<long>(type: "bigint", nullable: false),
                    SportPreferenceValueId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSportPreferences", x => x.UserSportPreferenceId);
                    table.ForeignKey(
                        name: "FK_UserSportPreferences_SportPreferenceValues_SportPreferenceV~",
                        column: x => x.SportPreferenceValueId,
                        principalTable: "SportPreferenceValues",
                        principalColumn: "SportPreferenceValueId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserSportPreferences_SportPreferences_SportPreferenceId",
                        column: x => x.SportPreferenceId,
                        principalTable: "SportPreferences",
                        principalColumn: "SportPreferenceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserSportPreferences_UserSport_UserSportId",
                        column: x => x.UserSportId,
                        principalTable: "UserSport",
                        principalColumn: "UserSportId",
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

            migrationBuilder.CreateIndex(
                name: "IX_SportPreferenceTranslations_LanguageId",
                table: "SportPreferenceTranslations",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_SportPreferenceTranslations_SportPreferenceId",
                table: "SportPreferenceTranslations",
                column: "SportPreferenceId");

            migrationBuilder.CreateIndex(
                name: "IX_SportPreferenceValueTranslations_LanguageId",
                table: "SportPreferenceValueTranslations",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_SportPreferenceValueTranslations_SportPreferenceValueId",
                table: "SportPreferenceValueTranslations",
                column: "SportPreferenceValueId");

            migrationBuilder.CreateIndex(
                name: "IX_SportPreferenceValues_SportPreferenceId",
                table: "SportPreferenceValues",
                column: "SportPreferenceId");

            migrationBuilder.CreateIndex(
                name: "IX_SportPreferences_SportId",
                table: "SportPreferences",
                column: "SportId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSport_SportId",
                table: "UserSport",
                column: "SportId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSport_XsportUserId",
                table: "UserSport",
                column: "XsportUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSportPreferences_SportPreferenceId",
                table: "UserSportPreferences",
                column: "SportPreferenceId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSportPreferences_SportPreferenceValueId",
                table: "UserSportPreferences",
                column: "SportPreferenceValueId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserSportPreferences_UserSportId",
                table: "UserSportPreferences",
                column: "UserSportId");

            migrationBuilder.AddForeignKey(
                name: "FK_Types_TypeTranslations_TypeTranslationId",
                table: "Types",
                column: "TypeTranslationId",
                principalTable: "TypeTranslations",
                principalColumn: "TypeTranslationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Types_TypeTranslations_TypeTranslationId",
                table: "Types");

            migrationBuilder.DropTable(
                name: "LanguageTypeTranslation");

            migrationBuilder.DropTable(
                name: "SportPreferenceTranslations");

            migrationBuilder.DropTable(
                name: "SportPreferenceValueTranslations");

            migrationBuilder.DropTable(
                name: "UserSportPreferences");

            migrationBuilder.DropTable(
                name: "TypeTranslations");

            migrationBuilder.DropTable(
                name: "SportPreferenceValues");

            migrationBuilder.DropTable(
                name: "UserSport");

            migrationBuilder.DropTable(
                name: "SportPreferences");

            migrationBuilder.DropIndex(
                name: "IX_Types_TypeTranslationId",
                table: "Types");

            migrationBuilder.DropColumn(
                name: "TypeTranslationId",
                table: "Types");
        }
    }
}
