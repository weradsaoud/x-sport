using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Xsport.API.Migrations
{
    /// <inheritdoc />
    public partial class add_academy_tables2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Academies");

            migrationBuilder.AddColumn<int>(
                name: "OrderInWeek",
                table: "WorkingDays",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsPersonal",
                table: "UserCourses",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<long>(
                name: "RelativeId",
                table: "UserCourses",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "SportTranslations",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "EndDate",
                table: "Courses",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<DateOnly>(
                name: "StartDate",
                table: "Courses",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<long>(
                name: "AcademyId",
                table: "AgeCategories",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "AcademyTranslations",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "AcademyReviews",
                columns: table => new
                {
                    AcademyReviewId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Evaluation = table.Column<double>(type: "double precision", nullable: false),
                    ReviewDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    XsportUserId = table.Column<long>(type: "bigint", nullable: false),
                    AcademyId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcademyReviews", x => x.AcademyReviewId);
                    table.ForeignKey(
                        name: "FK_AcademyReviews_Academies_AcademyId",
                        column: x => x.AcademyId,
                        principalTable: "Academies",
                        principalColumn: "AcademyId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AcademyReviews_AspNetUsers_XsportUserId",
                        column: x => x.XsportUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CourseTranslations",
                columns: table => new
                {
                    CourseTranslationId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    CourseId = table.Column<long>(type: "bigint", nullable: false),
                    LanguageId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseTranslations", x => x.CourseTranslationId);
                    table.ForeignKey(
                        name: "FK_CourseTranslations_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseTranslations_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "LanguageId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Mutimedias",
                columns: table => new
                {
                    MutimediaId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FilePath = table.Column<string>(type: "text", nullable: false),
                    IsVideo = table.Column<bool>(type: "boolean", nullable: false),
                    IsCover = table.Column<bool>(type: "boolean", nullable: false),
                    AcademyId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mutimedias", x => x.MutimediaId);
                    table.ForeignKey(
                        name: "FK_Mutimedias_Academies_AcademyId",
                        column: x => x.AcademyId,
                        principalTable: "Academies",
                        principalColumn: "AcademyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Relatives",
                columns: table => new
                {
                    RelativeId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Relatives", x => x.RelativeId);
                });

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    ServiceId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.ServiceId);
                });

            migrationBuilder.CreateTable(
                name: "RelativeTranslations",
                columns: table => new
                {
                    RelativeTranslationId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    RelativeId = table.Column<long>(type: "bigint", nullable: false),
                    LanguageId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RelativeTranslations", x => x.RelativeTranslationId);
                    table.ForeignKey(
                        name: "FK_RelativeTranslations_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "LanguageId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RelativeTranslations_Relatives_RelativeId",
                        column: x => x.RelativeId,
                        principalTable: "Relatives",
                        principalColumn: "RelativeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServiceTranslations",
                columns: table => new
                {
                    ServiceTranslationId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    ServiceId = table.Column<long>(type: "bigint", nullable: false),
                    LanguageId = table.Column<long>(type: "bigint", nullable: false),
                    ServiceId1 = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceTranslations", x => x.ServiceTranslationId);
                    table.ForeignKey(
                        name: "FK_ServiceTranslations_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "LanguageId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServiceTranslations_Services_ServiceId1",
                        column: x => x.ServiceId1,
                        principalTable: "Services",
                        principalColumn: "ServiceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServiceValues",
                columns: table => new
                {
                    ServiceValueId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ServiceId = table.Column<long>(type: "bigint", nullable: false),
                    ServiceId1 = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceValues", x => x.ServiceValueId);
                    table.ForeignKey(
                        name: "FK_ServiceValues_Services_ServiceId1",
                        column: x => x.ServiceId1,
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
                    Name = table.Column<string>(type: "text", nullable: false),
                    ServiceValueId = table.Column<long>(type: "bigint", nullable: false),
                    LanguageId = table.Column<long>(type: "bigint", nullable: false)
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
                name: "IX_UserCourses_RelativeId",
                table: "UserCourses",
                column: "RelativeId");

            migrationBuilder.CreateIndex(
                name: "IX_AgeCategories_AcademyId",
                table: "AgeCategories",
                column: "AcademyId");

            migrationBuilder.CreateIndex(
                name: "IX_AcademyReviews_AcademyId",
                table: "AcademyReviews",
                column: "AcademyId");

            migrationBuilder.CreateIndex(
                name: "IX_AcademyReviews_XsportUserId",
                table: "AcademyReviews",
                column: "XsportUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AcademyServiceValues_AcademyId",
                table: "AcademyServiceValues",
                column: "AcademyId");

            migrationBuilder.CreateIndex(
                name: "IX_AcademyServiceValues_ServiceValueId",
                table: "AcademyServiceValues",
                column: "ServiceValueId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseTranslations_CourseId",
                table: "CourseTranslations",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseTranslations_LanguageId",
                table: "CourseTranslations",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_Mutimedias_AcademyId",
                table: "Mutimedias",
                column: "AcademyId");

            migrationBuilder.CreateIndex(
                name: "IX_RelativeTranslations_LanguageId",
                table: "RelativeTranslations",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_RelativeTranslations_RelativeId",
                table: "RelativeTranslations",
                column: "RelativeId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceTranslations_LanguageId",
                table: "ServiceTranslations",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceTranslations_ServiceId1",
                table: "ServiceTranslations",
                column: "ServiceId1");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceValueTranslations_LanguageId",
                table: "ServiceValueTranslations",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceValueTranslations_ServiceValueId",
                table: "ServiceValueTranslations",
                column: "ServiceValueId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceValues_ServiceId1",
                table: "ServiceValues",
                column: "ServiceId1");

            migrationBuilder.AddForeignKey(
                name: "FK_AgeCategories_Academies_AcademyId",
                table: "AgeCategories",
                column: "AcademyId",
                principalTable: "Academies",
                principalColumn: "AcademyId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserCourses_Relatives_RelativeId",
                table: "UserCourses",
                column: "RelativeId",
                principalTable: "Relatives",
                principalColumn: "RelativeId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AgeCategories_Academies_AcademyId",
                table: "AgeCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_UserCourses_Relatives_RelativeId",
                table: "UserCourses");

            migrationBuilder.DropTable(
                name: "AcademyReviews");

            migrationBuilder.DropTable(
                name: "AcademyServiceValues");

            migrationBuilder.DropTable(
                name: "CourseTranslations");

            migrationBuilder.DropTable(
                name: "Mutimedias");

            migrationBuilder.DropTable(
                name: "RelativeTranslations");

            migrationBuilder.DropTable(
                name: "ServiceTranslations");

            migrationBuilder.DropTable(
                name: "ServiceValueTranslations");

            migrationBuilder.DropTable(
                name: "Relatives");

            migrationBuilder.DropTable(
                name: "ServiceValues");

            migrationBuilder.DropTable(
                name: "Services");

            migrationBuilder.DropIndex(
                name: "IX_UserCourses_RelativeId",
                table: "UserCourses");

            migrationBuilder.DropIndex(
                name: "IX_AgeCategories_AcademyId",
                table: "AgeCategories");

            migrationBuilder.DropColumn(
                name: "OrderInWeek",
                table: "WorkingDays");

            migrationBuilder.DropColumn(
                name: "IsPersonal",
                table: "UserCourses");

            migrationBuilder.DropColumn(
                name: "RelativeId",
                table: "UserCourses");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "AcademyId",
                table: "AgeCategories");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "AcademyTranslations");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "SportTranslations",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Courses",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Academies",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
