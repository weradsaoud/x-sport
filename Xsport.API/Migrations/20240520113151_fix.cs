using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Xsport.API.Migrations
{
    /// <inheritdoc />
    public partial class fix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StadiumCreationProcesses_Stadiums_StadiumId1",
                table: "StadiumCreationProcesses");

            migrationBuilder.DropIndex(
                name: "IX_StadiumCreationProcesses_StadiumId1",
                table: "StadiumCreationProcesses");

            migrationBuilder.DropColumn(
                name: "StadiumId1",
                table: "StadiumCreationProcesses");

            migrationBuilder.AlterColumn<long>(
                name: "StadiumId",
                table: "StadiumCreationProcesses",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.CreateIndex(
                name: "IX_StadiumCreationProcesses_StadiumId",
                table: "StadiumCreationProcesses",
                column: "StadiumId");

            migrationBuilder.AddForeignKey(
                name: "FK_StadiumCreationProcesses_Stadiums_StadiumId",
                table: "StadiumCreationProcesses",
                column: "StadiumId",
                principalTable: "Stadiums",
                principalColumn: "StadiumId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StadiumCreationProcesses_Stadiums_StadiumId",
                table: "StadiumCreationProcesses");

            migrationBuilder.DropIndex(
                name: "IX_StadiumCreationProcesses_StadiumId",
                table: "StadiumCreationProcesses");

            migrationBuilder.AlterColumn<int>(
                name: "StadiumId",
                table: "StadiumCreationProcesses",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<long>(
                name: "StadiumId1",
                table: "StadiumCreationProcesses",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_StadiumCreationProcesses_StadiumId1",
                table: "StadiumCreationProcesses",
                column: "StadiumId1");

            migrationBuilder.AddForeignKey(
                name: "FK_StadiumCreationProcesses_Stadiums_StadiumId1",
                table: "StadiumCreationProcesses",
                column: "StadiumId1",
                principalTable: "Stadiums",
                principalColumn: "StadiumId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
