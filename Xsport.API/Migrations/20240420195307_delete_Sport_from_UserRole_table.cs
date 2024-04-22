using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Xsport.API.Migrations
{
    /// <inheritdoc />
    public partial class delete_Sport_from_UserRole_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_Sports_SportId",
                table: "AspNetUserRoles");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_Sports_SportId",
                table: "AspNetUserRoles",
                column: "SportId",
                principalTable: "Sports",
                principalColumn: "SportId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_Sports_SportId",
                table: "AspNetUserRoles");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_Sports_SportId",
                table: "AspNetUserRoles",
                column: "SportId",
                principalTable: "Sports",
                principalColumn: "SportId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
