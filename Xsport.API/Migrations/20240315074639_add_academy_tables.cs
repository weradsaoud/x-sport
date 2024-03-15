using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Xsport.API.Migrations
{
    /// <inheritdoc />
    public partial class add_academy_tables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "LevelTranslations",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Academies",
                columns: table => new
                {
                    AcademyId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Lattitude = table.Column<decimal>(type: "numeric", nullable: false),
                    Longitude = table.Column<decimal>(type: "numeric", nullable: false),
                    Phone = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    OpenAt = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    CloseAt = table.Column<TimeOnly>(type: "time without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Academies", x => x.AcademyId);
                });

            migrationBuilder.CreateTable(
                name: "AgeCategories",
                columns: table => new
                {
                    AgeCategoryId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FromAge = table.Column<int>(type: "integer", nullable: false),
                    ToAge = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgeCategories", x => x.AgeCategoryId);
                });

            migrationBuilder.CreateTable(
                name: "WorkingDays",
                columns: table => new
                {
                    WorkingDayId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkingDays", x => x.WorkingDayId);
                });

            migrationBuilder.CreateTable(
                name: "AcademyTranslations",
                columns: table => new
                {
                    AcademyTranslationId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    AcademyId = table.Column<long>(type: "bigint", nullable: false),
                    LanguageId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcademyTranslations", x => x.AcademyTranslationId);
                    table.ForeignKey(
                        name: "FK_AcademyTranslations_Academies_AcademyId",
                        column: x => x.AcademyId,
                        principalTable: "Academies",
                        principalColumn: "AcademyId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AcademyTranslations_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "LanguageId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AgeCategoryTranslations",
                columns: table => new
                {
                    AgeCategoryTranslationId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    AgeCategoryId = table.Column<long>(type: "bigint", nullable: false),
                    LanguageId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgeCategoryTranslations", x => x.AgeCategoryTranslationId);
                    table.ForeignKey(
                        name: "FK_AgeCategoryTranslations_AgeCategories_AgeCategoryId",
                        column: x => x.AgeCategoryId,
                        principalTable: "AgeCategories",
                        principalColumn: "AgeCategoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AgeCategoryTranslations_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "LanguageId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    CourseId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    AcademyId = table.Column<long>(type: "bigint", nullable: false),
                    SportId = table.Column<long>(type: "bigint", nullable: false),
                    AgeCategoryId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.CourseId);
                    table.ForeignKey(
                        name: "FK_Courses_Academies_AcademyId",
                        column: x => x.AcademyId,
                        principalTable: "Academies",
                        principalColumn: "AcademyId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Courses_AgeCategories_AgeCategoryId",
                        column: x => x.AgeCategoryId,
                        principalTable: "AgeCategories",
                        principalColumn: "AgeCategoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Courses_Sports_SportId",
                        column: x => x.SportId,
                        principalTable: "Sports",
                        principalColumn: "SportId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AcademyWorkingDays",
                columns: table => new
                {
                    AcademyWorkingDayId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OpenAt = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    CloseAt = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    AcademyId = table.Column<long>(type: "bigint", nullable: false),
                    WorkingDayId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcademyWorkingDays", x => x.AcademyWorkingDayId);
                    table.ForeignKey(
                        name: "FK_AcademyWorkingDays_Academies_AcademyId",
                        column: x => x.AcademyId,
                        principalTable: "Academies",
                        principalColumn: "AcademyId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AcademyWorkingDays_WorkingDays_WorkingDayId",
                        column: x => x.WorkingDayId,
                        principalTable: "WorkingDays",
                        principalColumn: "WorkingDayId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkingDayTranslations",
                columns: table => new
                {
                    WorkingDayTranslationId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    WorkingDayId = table.Column<long>(type: "bigint", nullable: false),
                    LanguageId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkingDayTranslations", x => x.WorkingDayTranslationId);
                    table.ForeignKey(
                        name: "FK_WorkingDayTranslations_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "LanguageId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkingDayTranslations_WorkingDays_WorkingDayId",
                        column: x => x.WorkingDayId,
                        principalTable: "WorkingDays",
                        principalColumn: "WorkingDayId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CourseWorkingDays",
                columns: table => new
                {
                    CourseWorkingDayId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StartAt = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    EndAt = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    CourseId = table.Column<long>(type: "bigint", nullable: false),
                    WorkingDayId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseWorkingDays", x => x.CourseWorkingDayId);
                    table.ForeignKey(
                        name: "FK_CourseWorkingDays_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseWorkingDays_WorkingDays_WorkingDayId",
                        column: x => x.WorkingDayId,
                        principalTable: "WorkingDays",
                        principalColumn: "WorkingDayId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserCourses",
                columns: table => new
                {
                    UserCourseId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Points = table.Column<int>(type: "integer", nullable: false),
                    XsportUserId = table.Column<long>(type: "bigint", nullable: false),
                    CourseId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCourses", x => x.UserCourseId);
                    table.ForeignKey(
                        name: "FK_UserCourses_AspNetUsers_XsportUserId",
                        column: x => x.XsportUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserCourses_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AcademyTranslations_AcademyId",
                table: "AcademyTranslations",
                column: "AcademyId");

            migrationBuilder.CreateIndex(
                name: "IX_AcademyTranslations_LanguageId",
                table: "AcademyTranslations",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_AcademyWorkingDays_AcademyId",
                table: "AcademyWorkingDays",
                column: "AcademyId");

            migrationBuilder.CreateIndex(
                name: "IX_AcademyWorkingDays_WorkingDayId",
                table: "AcademyWorkingDays",
                column: "WorkingDayId");

            migrationBuilder.CreateIndex(
                name: "IX_AgeCategoryTranslations_AgeCategoryId",
                table: "AgeCategoryTranslations",
                column: "AgeCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_AgeCategoryTranslations_LanguageId",
                table: "AgeCategoryTranslations",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseWorkingDays_CourseId",
                table: "CourseWorkingDays",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseWorkingDays_WorkingDayId",
                table: "CourseWorkingDays",
                column: "WorkingDayId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_AcademyId",
                table: "Courses",
                column: "AcademyId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_AgeCategoryId",
                table: "Courses",
                column: "AgeCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_SportId",
                table: "Courses",
                column: "SportId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCourses_CourseId",
                table: "UserCourses",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCourses_XsportUserId",
                table: "UserCourses",
                column: "XsportUserId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkingDayTranslations_LanguageId",
                table: "WorkingDayTranslations",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkingDayTranslations_WorkingDayId",
                table: "WorkingDayTranslations",
                column: "WorkingDayId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AcademyTranslations");

            migrationBuilder.DropTable(
                name: "AcademyWorkingDays");

            migrationBuilder.DropTable(
                name: "AgeCategoryTranslations");

            migrationBuilder.DropTable(
                name: "CourseWorkingDays");

            migrationBuilder.DropTable(
                name: "UserCourses");

            migrationBuilder.DropTable(
                name: "WorkingDayTranslations");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "WorkingDays");

            migrationBuilder.DropTable(
                name: "Academies");

            migrationBuilder.DropTable(
                name: "AgeCategories");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "LevelTranslations",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
