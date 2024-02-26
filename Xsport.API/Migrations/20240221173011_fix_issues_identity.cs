using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Xsport.API.Migrations
{
    /// <inheritdoc />
    public partial class fix_issues_identity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TypeTranslations_AspNetRoles_TypeId",
                table: "TypeTranslations");

            migrationBuilder.DropTable(
                name: "UserType");

            migrationBuilder.RenameColumn(
                name: "TypeId",
                table: "TypeTranslations",
                newName: "XsportRoleId");

            migrationBuilder.RenameColumn(
                name: "TypeTranslationId",
                table: "TypeTranslations",
                newName: "XsportRoleTranslationId");

            migrationBuilder.RenameIndex(
                name: "IX_TypeTranslations_TypeId",
                table: "TypeTranslations",
                newName: "IX_TypeTranslations_XsportRoleId");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUserRoles",
                type: "character varying(34)",
                maxLength: 34,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<long>(
                name: "SportId",
                table: "AspNetUserRoles",
                type: "bigint",
                nullable: true);

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
                name: "IX_AspNetUserRoles_SportId",
                table: "AspNetUserRoles",
                column: "SportId");

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
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_XsportUserId",
                table: "AspNetUserRoles",
                column: "XsportUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_Sports_SportId",
                table: "AspNetUserRoles",
                column: "SportId",
                principalTable: "Sports",
                principalColumn: "SportId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TypeTranslations_AspNetRoles_XsportRoleId",
                table: "TypeTranslations",
                column: "XsportRoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
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
                name: "FK_AspNetUserRoles_Sports_SportId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_TypeTranslations_AspNetRoles_XsportRoleId",
                table: "TypeTranslations");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUserRoles_SportId",
                table: "AspNetUserRoles");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUserRoles_XsportRoleId",
                table: "AspNetUserRoles");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUserRoles_XsportUserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUserRoles");

            migrationBuilder.DropColumn(
                name: "SportId",
                table: "AspNetUserRoles");

            migrationBuilder.DropColumn(
                name: "XsportRoleId",
                table: "AspNetUserRoles");

            migrationBuilder.DropColumn(
                name: "XsportUserId",
                table: "AspNetUserRoles");

            migrationBuilder.RenameColumn(
                name: "XsportRoleId",
                table: "TypeTranslations",
                newName: "TypeId");

            migrationBuilder.RenameColumn(
                name: "XsportRoleTranslationId",
                table: "TypeTranslations",
                newName: "TypeTranslationId");

            migrationBuilder.RenameIndex(
                name: "IX_TypeTranslations_XsportRoleId",
                table: "TypeTranslations",
                newName: "IX_TypeTranslations_TypeId");

            migrationBuilder.CreateTable(
                name: "UserType",
                columns: table => new
                {
                    UserTypeId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SportId = table.Column<long>(type: "bigint", nullable: false),
                    TypeId = table.Column<long>(type: "bigint", nullable: false),
                    XsportUserId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserType", x => x.UserTypeId);
                    table.ForeignKey(
                        name: "FK_UserType_AspNetRoles_TypeId",
                        column: x => x.TypeId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserType_AspNetUsers_XsportUserId",
                        column: x => x.XsportUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserType_Sports_SportId",
                        column: x => x.SportId,
                        principalTable: "Sports",
                        principalColumn: "SportId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserType_SportId",
                table: "UserType",
                column: "SportId");

            migrationBuilder.CreateIndex(
                name: "IX_UserType_TypeId",
                table: "UserType",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_UserType_XsportUserId",
                table: "UserType",
                column: "XsportUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TypeTranslations_AspNetRoles_TypeId",
                table: "TypeTranslations",
                column: "TypeId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
