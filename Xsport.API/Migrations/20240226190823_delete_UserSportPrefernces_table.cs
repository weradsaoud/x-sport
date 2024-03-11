using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Xsport.API.Migrations
{
    /// <inheritdoc />
    public partial class delete_UserSportPrefernces_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserSportPreferences");

            migrationBuilder.CreateTable(
                name: "UserSportPreferenceValuess",
                columns: table => new
                {
                    UserSportPreferenceValueId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserSportId = table.Column<long>(type: "bigint", nullable: false),
                    SportPreferenceValueId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSportPreferenceValuess", x => x.UserSportPreferenceValueId);
                    table.ForeignKey(
                        name: "FK_UserSportPreferenceValuess_SportPreferenceValues_SportPrefe~",
                        column: x => x.SportPreferenceValueId,
                        principalTable: "SportPreferenceValues",
                        principalColumn: "SportPreferenceValueId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserSportPreferenceValuess_UserSport_UserSportId",
                        column: x => x.UserSportId,
                        principalTable: "UserSport",
                        principalColumn: "UserSportId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserSportPreferenceValuess_SportPreferenceValueId",
                table: "UserSportPreferenceValuess",
                column: "SportPreferenceValueId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSportPreferenceValuess_UserSportId",
                table: "UserSportPreferenceValuess",
                column: "UserSportId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserSportPreferenceValuess");

            migrationBuilder.CreateTable(
                name: "UserSportPreferences",
                columns: table => new
                {
                    UserSportPreferenceId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SportPreferenceId = table.Column<long>(type: "bigint", nullable: false),
                    SportPreferenceValueId = table.Column<long>(type: "bigint", nullable: false),
                    UserSportId = table.Column<long>(type: "bigint", nullable: false)
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
        }
    }
}
