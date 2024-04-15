using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Xsport.API.Migrations
{
    /// <inheritdoc />
    public partial class modify_floor_table_numPlayers_col : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FloorId",
                table: "Stadiums");

            migrationBuilder.AddColumn<int>(
                name: "NumPlayers",
                table: "Floors",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumPlayers",
                table: "Floors");

            migrationBuilder.AddColumn<long>(
                name: "FloorId",
                table: "Stadiums",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
