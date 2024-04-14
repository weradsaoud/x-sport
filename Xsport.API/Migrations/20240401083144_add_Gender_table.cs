using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Xsport.API.Migrations
{
    /// <inheritdoc />
    public partial class add_Gender_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "GenderId",
                table: "Courses",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "Genders",
                columns: table => new
                {
                    GenderId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genders", x => x.GenderId);
                });

            migrationBuilder.CreateTable(
                name: "GenderTranslations",
                columns: table => new
                {
                    GenderTranslationId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    GenderId = table.Column<long>(type: "bigint", nullable: false),
                    LanguageId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenderTranslations", x => x.GenderTranslationId);
                    table.ForeignKey(
                        name: "FK_GenderTranslations_Genders_GenderId",
                        column: x => x.GenderId,
                        principalTable: "Genders",
                        principalColumn: "GenderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GenderTranslations_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "LanguageId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Courses_GenderId",
                table: "Courses",
                column: "GenderId");

            migrationBuilder.CreateIndex(
                name: "IX_GenderTranslations_GenderId",
                table: "GenderTranslations",
                column: "GenderId");

            migrationBuilder.CreateIndex(
                name: "IX_GenderTranslations_LanguageId",
                table: "GenderTranslations",
                column: "LanguageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Genders_GenderId",
                table: "Courses",
                column: "GenderId",
                principalTable: "Genders",
                principalColumn: "GenderId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Genders_GenderId",
                table: "Courses");

            migrationBuilder.DropTable(
                name: "GenderTranslations");

            migrationBuilder.DropTable(
                name: "Genders");

            migrationBuilder.DropIndex(
                name: "IX_Courses_GenderId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "GenderId",
                table: "Courses");
        }
    }
}
