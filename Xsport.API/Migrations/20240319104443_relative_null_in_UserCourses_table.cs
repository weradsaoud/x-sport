using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Xsport.API.Migrations
{
    /// <inheritdoc />
    public partial class relative_null_in_UserCourses_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserCourses_Relatives_RelativeId",
                table: "UserCourses");

            migrationBuilder.AlterColumn<long>(
                name: "RelativeId",
                table: "UserCourses",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_UserCourses_Relatives_RelativeId",
                table: "UserCourses",
                column: "RelativeId",
                principalTable: "Relatives",
                principalColumn: "RelativeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserCourses_Relatives_RelativeId",
                table: "UserCourses");

            migrationBuilder.AlterColumn<long>(
                name: "RelativeId",
                table: "UserCourses",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserCourses_Relatives_RelativeId",
                table: "UserCourses",
                column: "RelativeId",
                principalTable: "Relatives",
                principalColumn: "RelativeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
