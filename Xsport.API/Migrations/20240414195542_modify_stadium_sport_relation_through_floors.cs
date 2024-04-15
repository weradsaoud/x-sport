using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Xsport.API.Migrations
{
    /// <inheritdoc />
    public partial class modify_stadium_sport_relation_through_floors : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stadiums_Sports_SportId",
                table: "Stadiums");

            migrationBuilder.DropIndex(
                name: "IX_Stadiums_SportId",
                table: "Stadiums");

            migrationBuilder.DropColumn(
                name: "SportId",
                table: "Stadiums");

            migrationBuilder.AddColumn<long>(
                name: "SportId",
                table: "Floors",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Floors_SportId",
                table: "Floors",
                column: "SportId");

            migrationBuilder.AddForeignKey(
                name: "FK_Floors_Sports_SportId",
                table: "Floors",
                column: "SportId",
                principalTable: "Sports",
                principalColumn: "SportId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Floors_Sports_SportId",
                table: "Floors");

            migrationBuilder.DropIndex(
                name: "IX_Floors_SportId",
                table: "Floors");

            migrationBuilder.DropColumn(
                name: "SportId",
                table: "Floors");

            migrationBuilder.AddColumn<long>(
                name: "SportId",
                table: "Stadiums",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Stadiums_SportId",
                table: "Stadiums",
                column: "SportId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stadiums_Sports_SportId",
                table: "Stadiums",
                column: "SportId",
                principalTable: "Sports",
                principalColumn: "SportId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
