using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Xsport.API.Migrations
{
    /// <inheritdoc />
    public partial class delete_name_col_from_preferencesValuesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "SportPreferenceValues");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "SportPreferenceValueTranslations",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "SportPreferenceValueTranslations");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "SportPreferenceValues",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
