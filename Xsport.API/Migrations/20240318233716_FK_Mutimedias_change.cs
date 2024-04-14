using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Xsport.API.Migrations
{
    /// <inheritdoc />
    public partial class FK_Mutimedias_change : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mutimedias_Academies_AcademyId",
                table: "Mutimedias");

            migrationBuilder.DropForeignKey(
                name: "FK_Mutimedias_Stadiums_StadiumId",
                table: "Mutimedias");

            migrationBuilder.AlterColumn<long>(
                name: "StadiumId",
                table: "Mutimedias",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "AcademyId",
                table: "Mutimedias",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_Mutimedias_Academies_AcademyId",
                table: "Mutimedias",
                column: "AcademyId",
                principalTable: "Academies",
                principalColumn: "AcademyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Mutimedias_Stadiums_StadiumId",
                table: "Mutimedias",
                column: "StadiumId",
                principalTable: "Stadiums",
                principalColumn: "StadiumId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mutimedias_Academies_AcademyId",
                table: "Mutimedias");

            migrationBuilder.DropForeignKey(
                name: "FK_Mutimedias_Stadiums_StadiumId",
                table: "Mutimedias");

            migrationBuilder.AlterColumn<long>(
                name: "StadiumId",
                table: "Mutimedias",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "AcademyId",
                table: "Mutimedias",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Mutimedias_Academies_AcademyId",
                table: "Mutimedias",
                column: "AcademyId",
                principalTable: "Academies",
                principalColumn: "AcademyId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Mutimedias_Stadiums_StadiumId",
                table: "Mutimedias",
                column: "StadiumId",
                principalTable: "Stadiums",
                principalColumn: "StadiumId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
