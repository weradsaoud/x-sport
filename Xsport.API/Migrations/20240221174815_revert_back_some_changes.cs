using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Xsport.API.Migrations
{
    /// <inheritdoc />
    public partial class revert_back_some_changes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TypeTranslations_AspNetRoles_XsportRoleId",
                table: "TypeTranslations");

            migrationBuilder.DropForeignKey(
                name: "FK_TypeTranslations_Languages_LanguageId",
                table: "TypeTranslations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TypeTranslations",
                table: "TypeTranslations");

            migrationBuilder.RenameTable(
                name: "TypeTranslations",
                newName: "XsportRoleTranslations");

            migrationBuilder.RenameIndex(
                name: "IX_TypeTranslations_XsportRoleId",
                table: "XsportRoleTranslations",
                newName: "IX_XsportRoleTranslations_XsportRoleId");

            migrationBuilder.RenameIndex(
                name: "IX_TypeTranslations_LanguageId",
                table: "XsportRoleTranslations",
                newName: "IX_XsportRoleTranslations_LanguageId");

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

            migrationBuilder.AddPrimaryKey(
                name: "PK_XsportRoleTranslations",
                table: "XsportRoleTranslations",
                column: "XsportRoleTranslationId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_XsportRoleTranslations_AspNetRoles_XsportRoleId",
                table: "XsportRoleTranslations",
                column: "XsportRoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_XsportRoleTranslations_Languages_LanguageId",
                table: "XsportRoleTranslations",
                column: "LanguageId",
                principalTable: "Languages",
                principalColumn: "LanguageId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_XsportRoleId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_XsportUserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_XsportRoleTranslations_AspNetRoles_XsportRoleId",
                table: "XsportRoleTranslations");

            migrationBuilder.DropForeignKey(
                name: "FK_XsportRoleTranslations_Languages_LanguageId",
                table: "XsportRoleTranslations");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUserRoles_XsportRoleId",
                table: "AspNetUserRoles");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUserRoles_XsportUserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_XsportRoleTranslations",
                table: "XsportRoleTranslations");

            migrationBuilder.DropColumn(
                name: "XsportRoleId",
                table: "AspNetUserRoles");

            migrationBuilder.DropColumn(
                name: "XsportUserId",
                table: "AspNetUserRoles");

            migrationBuilder.RenameTable(
                name: "XsportRoleTranslations",
                newName: "TypeTranslations");

            migrationBuilder.RenameIndex(
                name: "IX_XsportRoleTranslations_XsportRoleId",
                table: "TypeTranslations",
                newName: "IX_TypeTranslations_XsportRoleId");

            migrationBuilder.RenameIndex(
                name: "IX_XsportRoleTranslations_LanguageId",
                table: "TypeTranslations",
                newName: "IX_TypeTranslations_LanguageId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TypeTranslations",
                table: "TypeTranslations",
                column: "XsportRoleTranslationId");

            migrationBuilder.AddForeignKey(
                name: "FK_TypeTranslations_AspNetRoles_XsportRoleId",
                table: "TypeTranslations",
                column: "XsportRoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TypeTranslations_Languages_LanguageId",
                table: "TypeTranslations",
                column: "LanguageId",
                principalTable: "Languages",
                principalColumn: "LanguageId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
