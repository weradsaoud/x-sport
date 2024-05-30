using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Xsport.API.Migrations
{
    /// <inheritdoc />
    public partial class addownerIdtostadium : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "OwnerId",
                table: "Stadiums",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Stadiums_OwnerId",
                table: "Stadiums",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stadiums_AspNetUsers_OwnerId",
                table: "Stadiums",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stadiums_AspNetUsers_OwnerId",
                table: "Stadiums");

            migrationBuilder.DropIndex(
                name: "IX_Stadiums_OwnerId",
                table: "Stadiums");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Stadiums");
        }
    }
}
