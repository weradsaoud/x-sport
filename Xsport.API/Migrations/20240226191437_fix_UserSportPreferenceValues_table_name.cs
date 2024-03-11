using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Xsport.API.Migrations
{
    /// <inheritdoc />
    public partial class fix_UserSportPreferenceValues_table_name : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserSportPreferenceValuess_SportPreferenceValues_SportPrefe~",
                table: "UserSportPreferenceValuess");

            migrationBuilder.DropForeignKey(
                name: "FK_UserSportPreferenceValuess_UserSport_UserSportId",
                table: "UserSportPreferenceValuess");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserSportPreferenceValuess",
                table: "UserSportPreferenceValuess");

            migrationBuilder.RenameTable(
                name: "UserSportPreferenceValuess",
                newName: "UserSportPreferenceValues");

            migrationBuilder.RenameIndex(
                name: "IX_UserSportPreferenceValuess_UserSportId",
                table: "UserSportPreferenceValues",
                newName: "IX_UserSportPreferenceValues_UserSportId");

            migrationBuilder.RenameIndex(
                name: "IX_UserSportPreferenceValuess_SportPreferenceValueId",
                table: "UserSportPreferenceValues",
                newName: "IX_UserSportPreferenceValues_SportPreferenceValueId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserSportPreferenceValues",
                table: "UserSportPreferenceValues",
                column: "UserSportPreferenceValueId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserSportPreferenceValues_SportPreferenceValues_SportPrefer~",
                table: "UserSportPreferenceValues",
                column: "SportPreferenceValueId",
                principalTable: "SportPreferenceValues",
                principalColumn: "SportPreferenceValueId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserSportPreferenceValues_UserSport_UserSportId",
                table: "UserSportPreferenceValues",
                column: "UserSportId",
                principalTable: "UserSport",
                principalColumn: "UserSportId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserSportPreferenceValues_SportPreferenceValues_SportPrefer~",
                table: "UserSportPreferenceValues");

            migrationBuilder.DropForeignKey(
                name: "FK_UserSportPreferenceValues_UserSport_UserSportId",
                table: "UserSportPreferenceValues");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserSportPreferenceValues",
                table: "UserSportPreferenceValues");

            migrationBuilder.RenameTable(
                name: "UserSportPreferenceValues",
                newName: "UserSportPreferenceValuess");

            migrationBuilder.RenameIndex(
                name: "IX_UserSportPreferenceValues_UserSportId",
                table: "UserSportPreferenceValuess",
                newName: "IX_UserSportPreferenceValuess_UserSportId");

            migrationBuilder.RenameIndex(
                name: "IX_UserSportPreferenceValues_SportPreferenceValueId",
                table: "UserSportPreferenceValuess",
                newName: "IX_UserSportPreferenceValuess_SportPreferenceValueId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserSportPreferenceValuess",
                table: "UserSportPreferenceValuess",
                column: "UserSportPreferenceValueId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserSportPreferenceValuess_SportPreferenceValues_SportPrefe~",
                table: "UserSportPreferenceValuess",
                column: "SportPreferenceValueId",
                principalTable: "SportPreferenceValues",
                principalColumn: "SportPreferenceValueId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserSportPreferenceValuess_UserSport_UserSportId",
                table: "UserSportPreferenceValuess",
                column: "UserSportId",
                principalTable: "UserSport",
                principalColumn: "UserSportId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
