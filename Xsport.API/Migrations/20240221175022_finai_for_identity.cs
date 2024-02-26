using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Xsport.API.Migrations
{
    /// <inheritdoc />
    public partial class finai_for_identity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_XsportRoleId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_XsportUserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUserRoles_XsportRoleId",
                table: "AspNetUserRoles");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUserRoles_XsportUserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropColumn(
                name: "XsportRoleId",
                table: "AspNetUserRoles");

            migrationBuilder.DropColumn(
                name: "XsportUserId",
                table: "AspNetUserRoles");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "XsportRoleId",
                table: "AspNetUserRoles",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "XsportUserId",
                table: "AspNetUserRoles",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_XsportRoleId",
                table: "AspNetUserRoles",
                column: "XsportRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_XsportUserId",
                table: "AspNetUserRoles",
                column: "XsportUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_XsportRoleId",
                table: "AspNetUserRoles",
                column: "XsportRoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_XsportUserId",
                table: "AspNetUserRoles",
                column: "XsportUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
