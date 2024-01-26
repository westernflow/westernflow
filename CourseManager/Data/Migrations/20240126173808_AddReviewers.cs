using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Data.Migrations
{
    public partial class AddReviewers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Suffix",
                table: "Courses");

            migrationBuilder.AddColumn<List<string>>(
                name: "ProfessorNames",
                table: "Sections",
                type: "text[]",
                nullable: false);

            migrationBuilder.AddColumn<string>(
                name: "SectionRequisiteString",
                table: "Sections",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Waitlist",
                table: "Sections",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "ReviewText",
                table: "ProfessorReviews",
                type: "character varying(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CourseId",
                table: "ProfessorReviews",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ReviewerId",
                table: "ProfessorReviews",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "FacultyAbbreviation",
                table: "Faculties",
                type: "character varying(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BreadthCategory",
                table: "Courses",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CourseSuffix",
                table: "Courses",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CourseWeight",
                table: "Courses",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExtraInformation",
                table: "Courses",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DayOfWeekEnumEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DayOfWeek = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DayOfWeekEnumEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Reviewer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Email = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    FirstName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviewer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SectionLocationAndTime",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Location = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Time = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    SectionId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SectionLocationAndTime", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SectionLocationAndTime_Sections_SectionId",
                        column: x => x.SectionId,
                        principalTable: "Sections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CourseReview",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ReviewerId = table.Column<int>(type: "integer", nullable: false),
                    IsLiked = table.Column<bool>(type: "boolean", nullable: false),
                    EasyRating = table.Column<int>(type: "integer", nullable: false),
                    UsefulRating = table.Column<int>(type: "integer", nullable: false),
                    ReviewText = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseReview", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseReview_Reviewer_ReviewerId",
                        column: x => x.ReviewerId,
                        principalTable: "Reviewer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProfessorReviewReviewer",
                columns: table => new
                {
                    LikedById = table.Column<int>(type: "integer", nullable: false),
                    ProfessorReviewsLikedId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfessorReviewReviewer", x => new { x.LikedById, x.ProfessorReviewsLikedId });
                    table.ForeignKey(
                        name: "FK_ProfessorReviewReviewer_ProfessorReviews_ProfessorReviewsLi~",
                        column: x => x.ProfessorReviewsLikedId,
                        principalTable: "ProfessorReviews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProfessorReviewReviewer_Reviewer_LikedById",
                        column: x => x.LikedById,
                        principalTable: "Reviewer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DayOfWeekEnumEntitySectionLocationAndTime",
                columns: table => new
                {
                    DaysOfWeekId = table.Column<int>(type: "integer", nullable: false),
                    SectionLocationAndTimesId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DayOfWeekEnumEntitySectionLocationAndTime", x => new { x.DaysOfWeekId, x.SectionLocationAndTimesId });
                    table.ForeignKey(
                        name: "FK_DayOfWeekEnumEntitySectionLocationAndTime_DayOfWeekEnumEnti~",
                        column: x => x.DaysOfWeekId,
                        principalTable: "DayOfWeekEnumEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DayOfWeekEnumEntitySectionLocationAndTime_SectionLocationAn~",
                        column: x => x.SectionLocationAndTimesId,
                        principalTable: "SectionLocationAndTime",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CourseReviewReviewer",
                columns: table => new
                {
                    CourseReviewsLikedId = table.Column<int>(type: "integer", nullable: false),
                    LikedById = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseReviewReviewer", x => new { x.CourseReviewsLikedId, x.LikedById });
                    table.ForeignKey(
                        name: "FK_CourseReviewReviewer_CourseReview_CourseReviewsLikedId",
                        column: x => x.CourseReviewsLikedId,
                        principalTable: "CourseReview",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseReviewReviewer_Reviewer_LikedById",
                        column: x => x.LikedById,
                        principalTable: "Reviewer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProfessorReviews_CourseId",
                table: "ProfessorReviews",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfessorReviews_ReviewerId",
                table: "ProfessorReviews",
                column: "ReviewerId");

            migrationBuilder.CreateIndex(
                name: "IX_Faculties_FacultyName",
                table: "Faculties",
                column: "FacultyName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CourseReview_ReviewerId",
                table: "CourseReview",
                column: "ReviewerId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseReviewReviewer_LikedById",
                table: "CourseReviewReviewer",
                column: "LikedById");

            migrationBuilder.CreateIndex(
                name: "IX_DayOfWeekEnumEntity_DayOfWeek",
                table: "DayOfWeekEnumEntity",
                column: "DayOfWeek",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DayOfWeekEnumEntitySectionLocationAndTime_SectionLocationAn~",
                table: "DayOfWeekEnumEntitySectionLocationAndTime",
                column: "SectionLocationAndTimesId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfessorReviewReviewer_ProfessorReviewsLikedId",
                table: "ProfessorReviewReviewer",
                column: "ProfessorReviewsLikedId");

            migrationBuilder.CreateIndex(
                name: "IX_SectionLocationAndTime_SectionId",
                table: "SectionLocationAndTime",
                column: "SectionId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProfessorReviews_Courses_CourseId",
                table: "ProfessorReviews",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProfessorReviews_Reviewer_ReviewerId",
                table: "ProfessorReviews",
                column: "ReviewerId",
                principalTable: "Reviewer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProfessorReviews_Courses_CourseId",
                table: "ProfessorReviews");

            migrationBuilder.DropForeignKey(
                name: "FK_ProfessorReviews_Reviewer_ReviewerId",
                table: "ProfessorReviews");

            migrationBuilder.DropTable(
                name: "CourseReviewReviewer");

            migrationBuilder.DropTable(
                name: "DayOfWeekEnumEntitySectionLocationAndTime");

            migrationBuilder.DropTable(
                name: "ProfessorReviewReviewer");

            migrationBuilder.DropTable(
                name: "CourseReview");

            migrationBuilder.DropTable(
                name: "DayOfWeekEnumEntity");

            migrationBuilder.DropTable(
                name: "SectionLocationAndTime");

            migrationBuilder.DropTable(
                name: "Reviewer");

            migrationBuilder.DropIndex(
                name: "IX_ProfessorReviews_CourseId",
                table: "ProfessorReviews");

            migrationBuilder.DropIndex(
                name: "IX_ProfessorReviews_ReviewerId",
                table: "ProfessorReviews");

            migrationBuilder.DropIndex(
                name: "IX_Faculties_FacultyName",
                table: "Faculties");

            migrationBuilder.DropColumn(
                name: "ProfessorNames",
                table: "Sections");

            migrationBuilder.DropColumn(
                name: "SectionRequisiteString",
                table: "Sections");

            migrationBuilder.DropColumn(
                name: "Waitlist",
                table: "Sections");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "ProfessorReviews");

            migrationBuilder.DropColumn(
                name: "ReviewerId",
                table: "ProfessorReviews");

            migrationBuilder.DropColumn(
                name: "FacultyAbbreviation",
                table: "Faculties");

            migrationBuilder.DropColumn(
                name: "BreadthCategory",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "CourseSuffix",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "CourseWeight",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "ExtraInformation",
                table: "Courses");

            migrationBuilder.AlterColumn<string>(
                name: "ReviewText",
                table: "ProfessorReviews",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500);

            migrationBuilder.AddColumn<string>(
                name: "Suffix",
                table: "Courses",
                type: "character varying(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");
        }
    }
}
