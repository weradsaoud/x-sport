using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Xsport.API.Migrations
{
    /// <inheritdoc />
    public partial class fix_context : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Match_Sports_SportId",
                table: "Match");

            migrationBuilder.DropForeignKey(
                name: "FK_UserMatch_AspNetUsers_XsportUserId",
                table: "UserMatch");

            migrationBuilder.DropForeignKey(
                name: "FK_UserMatch_Match_MatchId",
                table: "UserMatch");

            migrationBuilder.DropForeignKey(
                name: "FK_UserSport_AspNetUsers_XsportUserId",
                table: "UserSport");

            migrationBuilder.DropForeignKey(
                name: "FK_UserSport_Sports_SportId",
                table: "UserSport");

            migrationBuilder.DropForeignKey(
                name: "FK_UserSportPreferenceValues_UserSport_UserSportId",
                table: "UserSportPreferenceValues");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserSport",
                table: "UserSport");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserMatch",
                table: "UserMatch");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Match",
                table: "Match");

            migrationBuilder.RenameTable(
                name: "UserSport",
                newName: "UserSports");

            migrationBuilder.RenameTable(
                name: "UserMatch",
                newName: "UserMatchs");

            migrationBuilder.RenameTable(
                name: "Match",
                newName: "Matchs");

            migrationBuilder.RenameIndex(
                name: "IX_UserSport_XsportUserId",
                table: "UserSports",
                newName: "IX_UserSports_XsportUserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserSport_SportId",
                table: "UserSports",
                newName: "IX_UserSports_SportId");

            migrationBuilder.RenameIndex(
                name: "IX_UserMatch_XsportUserId",
                table: "UserMatchs",
                newName: "IX_UserMatchs_XsportUserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserMatch_MatchId",
                table: "UserMatchs",
                newName: "IX_UserMatchs_MatchId");

            migrationBuilder.RenameIndex(
                name: "IX_Match_SportId",
                table: "Matchs",
                newName: "IX_Matchs_SportId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserSports",
                table: "UserSports",
                column: "UserSportId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserMatchs",
                table: "UserMatchs",
                column: "UserMatchId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Matchs",
                table: "Matchs",
                column: "MatchId");

            migrationBuilder.AddForeignKey(
                name: "FK_Matchs_Sports_SportId",
                table: "Matchs",
                column: "SportId",
                principalTable: "Sports",
                principalColumn: "SportId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserMatchs_AspNetUsers_XsportUserId",
                table: "UserMatchs",
                column: "XsportUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserMatchs_Matchs_MatchId",
                table: "UserMatchs",
                column: "MatchId",
                principalTable: "Matchs",
                principalColumn: "MatchId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserSportPreferenceValues_UserSports_UserSportId",
                table: "UserSportPreferenceValues",
                column: "UserSportId",
                principalTable: "UserSports",
                principalColumn: "UserSportId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserSports_AspNetUsers_XsportUserId",
                table: "UserSports",
                column: "XsportUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserSports_Sports_SportId",
                table: "UserSports",
                column: "SportId",
                principalTable: "Sports",
                principalColumn: "SportId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matchs_Sports_SportId",
                table: "Matchs");

            migrationBuilder.DropForeignKey(
                name: "FK_UserMatchs_AspNetUsers_XsportUserId",
                table: "UserMatchs");

            migrationBuilder.DropForeignKey(
                name: "FK_UserMatchs_Matchs_MatchId",
                table: "UserMatchs");

            migrationBuilder.DropForeignKey(
                name: "FK_UserSportPreferenceValues_UserSports_UserSportId",
                table: "UserSportPreferenceValues");

            migrationBuilder.DropForeignKey(
                name: "FK_UserSports_AspNetUsers_XsportUserId",
                table: "UserSports");

            migrationBuilder.DropForeignKey(
                name: "FK_UserSports_Sports_SportId",
                table: "UserSports");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserSports",
                table: "UserSports");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserMatchs",
                table: "UserMatchs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Matchs",
                table: "Matchs");

            migrationBuilder.RenameTable(
                name: "UserSports",
                newName: "UserSport");

            migrationBuilder.RenameTable(
                name: "UserMatchs",
                newName: "UserMatch");

            migrationBuilder.RenameTable(
                name: "Matchs",
                newName: "Match");

            migrationBuilder.RenameIndex(
                name: "IX_UserSports_XsportUserId",
                table: "UserSport",
                newName: "IX_UserSport_XsportUserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserSports_SportId",
                table: "UserSport",
                newName: "IX_UserSport_SportId");

            migrationBuilder.RenameIndex(
                name: "IX_UserMatchs_XsportUserId",
                table: "UserMatch",
                newName: "IX_UserMatch_XsportUserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserMatchs_MatchId",
                table: "UserMatch",
                newName: "IX_UserMatch_MatchId");

            migrationBuilder.RenameIndex(
                name: "IX_Matchs_SportId",
                table: "Match",
                newName: "IX_Match_SportId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserSport",
                table: "UserSport",
                column: "UserSportId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserMatch",
                table: "UserMatch",
                column: "UserMatchId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Match",
                table: "Match",
                column: "MatchId");

            migrationBuilder.AddForeignKey(
                name: "FK_Match_Sports_SportId",
                table: "Match",
                column: "SportId",
                principalTable: "Sports",
                principalColumn: "SportId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserMatch_AspNetUsers_XsportUserId",
                table: "UserMatch",
                column: "XsportUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserMatch_Match_MatchId",
                table: "UserMatch",
                column: "MatchId",
                principalTable: "Match",
                principalColumn: "MatchId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserSport_AspNetUsers_XsportUserId",
                table: "UserSport",
                column: "XsportUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserSport_Sports_SportId",
                table: "UserSport",
                column: "SportId",
                principalTable: "Sports",
                principalColumn: "SportId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserSportPreferenceValues_UserSport_UserSportId",
                table: "UserSportPreferenceValues",
                column: "UserSportId",
                principalTable: "UserSport",
                principalColumn: "UserSportId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
