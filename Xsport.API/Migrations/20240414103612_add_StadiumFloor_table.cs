using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Xsport.API.Migrations
{
    /// <inheritdoc />
    public partial class add_StadiumFloor_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stadiums_Floors_FloorId",
                table: "Stadiums");

            migrationBuilder.DropIndex(
                name: "IX_Stadiums_FloorId",
                table: "Stadiums");

            migrationBuilder.CreateTable(
                name: "StadiumFloors",
                columns: table => new
                {
                    StadiumFloorId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StadiumId = table.Column<long>(type: "bigint", nullable: false),
                    FloorId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StadiumFloors", x => x.StadiumFloorId);
                    table.ForeignKey(
                        name: "FK_StadiumFloors_Floors_FloorId",
                        column: x => x.FloorId,
                        principalTable: "Floors",
                        principalColumn: "FloorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StadiumFloors_Stadiums_StadiumId",
                        column: x => x.StadiumId,
                        principalTable: "Stadiums",
                        principalColumn: "StadiumId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StadiumFloors_FloorId",
                table: "StadiumFloors",
                column: "FloorId");

            migrationBuilder.CreateIndex(
                name: "IX_StadiumFloors_StadiumId",
                table: "StadiumFloors",
                column: "StadiumId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StadiumFloors");

            migrationBuilder.CreateIndex(
                name: "IX_Stadiums_FloorId",
                table: "Stadiums",
                column: "FloorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stadiums_Floors_FloorId",
                table: "Stadiums",
                column: "FloorId",
                principalTable: "Floors",
                principalColumn: "FloorId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
