using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Xsport.API.Migrations
{
    /// <inheritdoc />
    public partial class correct_service_int_long : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceTranslations_Services_ServiceId1",
                table: "ServiceTranslations");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceValues_Services_ServiceId1",
                table: "ServiceValues");

            migrationBuilder.DropIndex(
                name: "IX_ServiceValues_ServiceId1",
                table: "ServiceValues");

            migrationBuilder.DropIndex(
                name: "IX_ServiceTranslations_ServiceId1",
                table: "ServiceTranslations");

            migrationBuilder.DropColumn(
                name: "ServiceId1",
                table: "ServiceValues");

            migrationBuilder.DropColumn(
                name: "ServiceId1",
                table: "ServiceTranslations");

            migrationBuilder.AlterColumn<long>(
                name: "ServiceId",
                table: "Services",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.CreateIndex(
                name: "IX_ServiceValues_ServiceId",
                table: "ServiceValues",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceTranslations_ServiceId",
                table: "ServiceTranslations",
                column: "ServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceTranslations_Services_ServiceId",
                table: "ServiceTranslations",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "ServiceId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceValues_Services_ServiceId",
                table: "ServiceValues",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "ServiceId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceTranslations_Services_ServiceId",
                table: "ServiceTranslations");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceValues_Services_ServiceId",
                table: "ServiceValues");

            migrationBuilder.DropIndex(
                name: "IX_ServiceValues_ServiceId",
                table: "ServiceValues");

            migrationBuilder.DropIndex(
                name: "IX_ServiceTranslations_ServiceId",
                table: "ServiceTranslations");

            migrationBuilder.AlterColumn<int>(
                name: "ServiceId",
                table: "Services",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "ServiceId1",
                table: "ServiceValues",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ServiceId1",
                table: "ServiceTranslations",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ServiceValues_ServiceId1",
                table: "ServiceValues",
                column: "ServiceId1");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceTranslations_ServiceId1",
                table: "ServiceTranslations",
                column: "ServiceId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceTranslations_Services_ServiceId1",
                table: "ServiceTranslations",
                column: "ServiceId1",
                principalTable: "Services",
                principalColumn: "ServiceId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceValues_Services_ServiceId1",
                table: "ServiceValues",
                column: "ServiceId1",
                principalTable: "Services",
                principalColumn: "ServiceId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
