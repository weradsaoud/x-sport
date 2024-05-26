using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Xsport.API.Migrations
{
    /// <inheritdoc />
    public partial class addforigenkey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StadiumCreationProcesses_Stadiums_StadiumDataStadiumId",
                table: "StadiumCreationProcesses");

            migrationBuilder.RenameColumn(
                name: "StadiumDataStadiumId",
                table: "StadiumCreationProcesses",
                newName: "StadiumId1");

            migrationBuilder.RenameIndex(
                name: "IX_StadiumCreationProcesses_StadiumDataStadiumId",
                table: "StadiumCreationProcesses",
                newName: "IX_StadiumCreationProcesses_StadiumId1");

            migrationBuilder.AddColumn<int>(
                name: "StadiumId",
                table: "StadiumCreationProcesses",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_StadiumCreationProcesses_Stadiums_StadiumId1",
                table: "StadiumCreationProcesses",
                column: "StadiumId1",
                principalTable: "Stadiums",
                principalColumn: "StadiumId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StadiumCreationProcesses_Stadiums_StadiumId1",
                table: "StadiumCreationProcesses");

            migrationBuilder.DropColumn(
                name: "StadiumId",
                table: "StadiumCreationProcesses");

            migrationBuilder.RenameColumn(
                name: "StadiumId1",
                table: "StadiumCreationProcesses",
                newName: "StadiumDataStadiumId");

            migrationBuilder.RenameIndex(
                name: "IX_StadiumCreationProcesses_StadiumId1",
                table: "StadiumCreationProcesses",
                newName: "IX_StadiumCreationProcesses_StadiumDataStadiumId");

            migrationBuilder.AddForeignKey(
                name: "FK_StadiumCreationProcesses_Stadiums_StadiumDataStadiumId",
                table: "StadiumCreationProcesses",
                column: "StadiumDataStadiumId",
                principalTable: "Stadiums",
                principalColumn: "StadiumId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
