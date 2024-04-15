using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Xsport.API.Migrations
{
    /// <inheritdoc />
    public partial class add_Reservation_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    ReservationId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    From = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    To = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    Status = table.Column<short>(type: "smallint", nullable: false),
                    XsportUserId = table.Column<long>(type: "bigint", nullable: false),
                    StadiumFloorId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.ReservationId);
                    table.ForeignKey(
                        name: "FK_Reservations_AspNetUsers_XsportUserId",
                        column: x => x.XsportUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservations_StadiumFloors_StadiumFloorId",
                        column: x => x.StadiumFloorId,
                        principalTable: "StadiumFloors",
                        principalColumn: "StadiumFloorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_StadiumFloorId",
                table: "Reservations",
                column: "StadiumFloorId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_XsportUserId",
                table: "Reservations",
                column: "XsportUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reservations");
        }
    }
}
