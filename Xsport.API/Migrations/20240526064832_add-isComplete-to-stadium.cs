using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Xsport.API.Migrations
{
    /// <inheritdoc />
    public partial class addisCompletetostadium : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsComplete",
                table: "Stadiums",
                type: "boolean",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsComplete",
                table: "Stadiums");
        }
    }
}
