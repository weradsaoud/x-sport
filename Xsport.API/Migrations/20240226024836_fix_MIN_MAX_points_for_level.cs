using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Xsport.API.Migrations
{
    /// <inheritdoc />
    public partial class fix_MIN_MAX_points_for_level : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MinPoints",
                table: "Levels",
                newName: "MaxPoints");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MaxPoints",
                table: "Levels",
                newName: "MinPoints");
        }
    }
}
