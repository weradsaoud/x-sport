using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Xsport.API.Migrations
{
    /// <inheritdoc />
    public partial class add_stadium_tables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AcademyServiceValues");

            migrationBuilder.DropTable(
                name: "ServiceValueTranslations");

            migrationBuilder.DropTable(
                name: "ServiceValues");

            migrationBuilder.AddColumn<string>(
                name: "ImgPath",
                table: "Services",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<long>(
                name: "StadiumId",
                table: "Mutimedias",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "AcademyServices",
                columns: table => new
                {
                    AcademyServiceId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AcademyId = table.Column<long>(type: "bigint", nullable: false),
                    ServiceId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcademyServices", x => x.AcademyServiceId);
                    table.ForeignKey(
                        name: "FK_AcademyServices_Academies_AcademyId",
                        column: x => x.AcademyId,
                        principalTable: "Academies",
                        principalColumn: "AcademyId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AcademyServices_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "ServiceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Stadiums",
                columns: table => new
                {
                    StadiumId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Latitude = table.Column<decimal>(type: "numeric", nullable: false),
                    Longitude = table.Column<decimal>(type: "numeric", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    SportId = table.Column<long>(type: "bigint", nullable: false),
                    AcademyId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stadiums", x => x.StadiumId);
                    table.ForeignKey(
                        name: "FK_Stadiums_Academies_AcademyId",
                        column: x => x.AcademyId,
                        principalTable: "Academies",
                        principalColumn: "AcademyId");
                    table.ForeignKey(
                        name: "FK_Stadiums_Sports_SportId",
                        column: x => x.SportId,
                        principalTable: "Sports",
                        principalColumn: "SportId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StadiumReviews",
                columns: table => new
                {
                    StadiumReviewId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Evaluation = table.Column<double>(type: "double precision", nullable: false),
                    ReviewDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    XsportUserId = table.Column<long>(type: "bigint", nullable: false),
                    StadiumId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StadiumReviews", x => x.StadiumReviewId);
                    table.ForeignKey(
                        name: "FK_StadiumReviews_AspNetUsers_XsportUserId",
                        column: x => x.XsportUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StadiumReviews_Stadiums_StadiumId",
                        column: x => x.StadiumId,
                        principalTable: "Stadiums",
                        principalColumn: "StadiumId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StadiumServices",
                columns: table => new
                {
                    StadiumServiceId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StadiumId = table.Column<long>(type: "bigint", nullable: false),
                    ServiceId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StadiumServices", x => x.StadiumServiceId);
                    table.ForeignKey(
                        name: "FK_StadiumServices_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "ServiceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StadiumServices_Stadiums_StadiumId",
                        column: x => x.StadiumId,
                        principalTable: "Stadiums",
                        principalColumn: "StadiumId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StadiumTranslations",
                columns: table => new
                {
                    StadiumTranslationId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    StadiumId = table.Column<long>(type: "bigint", nullable: false),
                    LanguageId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StadiumTranslations", x => x.StadiumTranslationId);
                    table.ForeignKey(
                        name: "FK_StadiumTranslations_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "LanguageId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StadiumTranslations_Stadiums_StadiumId",
                        column: x => x.StadiumId,
                        principalTable: "Stadiums",
                        principalColumn: "StadiumId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StadiumWorkingDays",
                columns: table => new
                {
                    StadiumWorkingDayId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OpenAt = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    CloseAt = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    StadiumId = table.Column<long>(type: "bigint", nullable: false),
                    WorkingDayId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StadiumWorkingDays", x => x.StadiumWorkingDayId);
                    table.ForeignKey(
                        name: "FK_StadiumWorkingDays_Stadiums_StadiumId",
                        column: x => x.StadiumId,
                        principalTable: "Stadiums",
                        principalColumn: "StadiumId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StadiumWorkingDays_WorkingDays_WorkingDayId",
                        column: x => x.WorkingDayId,
                        principalTable: "WorkingDays",
                        principalColumn: "WorkingDayId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Mutimedias_StadiumId",
                table: "Mutimedias",
                column: "StadiumId");

            migrationBuilder.CreateIndex(
                name: "IX_AcademyServices_AcademyId",
                table: "AcademyServices",
                column: "AcademyId");

            migrationBuilder.CreateIndex(
                name: "IX_AcademyServices_ServiceId",
                table: "AcademyServices",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_StadiumReviews_StadiumId",
                table: "StadiumReviews",
                column: "StadiumId");

            migrationBuilder.CreateIndex(
                name: "IX_StadiumReviews_XsportUserId",
                table: "StadiumReviews",
                column: "XsportUserId");

            migrationBuilder.CreateIndex(
                name: "IX_StadiumServices_ServiceId",
                table: "StadiumServices",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_StadiumServices_StadiumId",
                table: "StadiumServices",
                column: "StadiumId");

            migrationBuilder.CreateIndex(
                name: "IX_StadiumTranslations_LanguageId",
                table: "StadiumTranslations",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_StadiumTranslations_StadiumId",
                table: "StadiumTranslations",
                column: "StadiumId");

            migrationBuilder.CreateIndex(
                name: "IX_StadiumWorkingDays_StadiumId",
                table: "StadiumWorkingDays",
                column: "StadiumId");

            migrationBuilder.CreateIndex(
                name: "IX_StadiumWorkingDays_WorkingDayId",
                table: "StadiumWorkingDays",
                column: "WorkingDayId");

            migrationBuilder.CreateIndex(
                name: "IX_Stadiums_AcademyId",
                table: "Stadiums",
                column: "AcademyId");

            migrationBuilder.CreateIndex(
                name: "IX_Stadiums_SportId",
                table: "Stadiums",
                column: "SportId");

            migrationBuilder.AddForeignKey(
                name: "FK_Mutimedias_Stadiums_StadiumId",
                table: "Mutimedias",
                column: "StadiumId",
                principalTable: "Stadiums",
                principalColumn: "StadiumId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mutimedias_Stadiums_StadiumId",
                table: "Mutimedias");

            migrationBuilder.DropTable(
                name: "AcademyServices");

            migrationBuilder.DropTable(
                name: "StadiumReviews");

            migrationBuilder.DropTable(
                name: "StadiumServices");

            migrationBuilder.DropTable(
                name: "StadiumTranslations");

            migrationBuilder.DropTable(
                name: "StadiumWorkingDays");

            migrationBuilder.DropTable(
                name: "Stadiums");

            migrationBuilder.DropIndex(
                name: "IX_Mutimedias_StadiumId",
                table: "Mutimedias");

            migrationBuilder.DropColumn(
                name: "ImgPath",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "StadiumId",
                table: "Mutimedias");

            migrationBuilder.CreateTable(
                name: "ServiceValues",
                columns: table => new
                {
                    ServiceValueId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ServiceId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceValues", x => x.ServiceValueId);
                    table.ForeignKey(
                        name: "FK_ServiceValues_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "ServiceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AcademyServiceValues",
                columns: table => new
                {
                    AcademyServiceValueId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AcademyId = table.Column<long>(type: "bigint", nullable: false),
                    ServiceValueId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcademyServiceValues", x => x.AcademyServiceValueId);
                    table.ForeignKey(
                        name: "FK_AcademyServiceValues_Academies_AcademyId",
                        column: x => x.AcademyId,
                        principalTable: "Academies",
                        principalColumn: "AcademyId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AcademyServiceValues_ServiceValues_ServiceValueId",
                        column: x => x.ServiceValueId,
                        principalTable: "ServiceValues",
                        principalColumn: "ServiceValueId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServiceValueTranslations",
                columns: table => new
                {
                    ServiceValueTranslationId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LanguageId = table.Column<long>(type: "bigint", nullable: false),
                    ServiceValueId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceValueTranslations", x => x.ServiceValueTranslationId);
                    table.ForeignKey(
                        name: "FK_ServiceValueTranslations_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "LanguageId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServiceValueTranslations_ServiceValues_ServiceValueId",
                        column: x => x.ServiceValueId,
                        principalTable: "ServiceValues",
                        principalColumn: "ServiceValueId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AcademyServiceValues_AcademyId",
                table: "AcademyServiceValues",
                column: "AcademyId");

            migrationBuilder.CreateIndex(
                name: "IX_AcademyServiceValues_ServiceValueId",
                table: "AcademyServiceValues",
                column: "ServiceValueId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceValueTranslations_LanguageId",
                table: "ServiceValueTranslations",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceValueTranslations_ServiceValueId",
                table: "ServiceValueTranslations",
                column: "ServiceValueId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceValues_ServiceId",
                table: "ServiceValues",
                column: "ServiceId");
        }
    }
}
