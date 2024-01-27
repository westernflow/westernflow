using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class FluentTypingForJoinTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseReviewReviewer");

            migrationBuilder.DropTable(
                name: "DayOfWeekEnumEntitySectionLocationAndTime");

            migrationBuilder.DropTable(
                name: "ProfessorReviewReviewer");

            migrationBuilder.CreateTable(
                name: "JoinedDowSlt",
                columns: table => new
                {
                    DowId = table.Column<int>(type: "integer", nullable: false),
                    SltId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JoinedDowSlt", x => new { x.DowId, x.SltId });
                    table.ForeignKey(
                        name: "FK_JoinedDowSlt_DaysOfWeek_DowId",
                        column: x => x.DowId,
                        principalTable: "DaysOfWeek",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JoinedDowSlt_SectionLocationAndTime_SltId",
                        column: x => x.SltId,
                        principalTable: "SectionLocationAndTime",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JoinedReviewerCourseReview",
                columns: table => new
                {
                    CourseReviewId = table.Column<int>(type: "integer", nullable: false),
                    ReviewerId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JoinedReviewerCourseReview", x => new { x.CourseReviewId, x.ReviewerId });
                    table.ForeignKey(
                        name: "FK_JoinedReviewerCourseReview_CourseReviews_CourseReviewId",
                        column: x => x.CourseReviewId,
                        principalTable: "CourseReviews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JoinedReviewerCourseReview_Reviewers_ReviewerId",
                        column: x => x.ReviewerId,
                        principalTable: "Reviewers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JoinedReviewerProfessorReview",
                columns: table => new
                {
                    ProfessorReviewId = table.Column<int>(type: "integer", nullable: false),
                    ReviewerId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JoinedReviewerProfessorReview", x => new { x.ProfessorReviewId, x.ReviewerId });
                    table.ForeignKey(
                        name: "FK_JoinedReviewerProfessorReview_ProfessorReviews_ProfessorRev~",
                        column: x => x.ProfessorReviewId,
                        principalTable: "ProfessorReviews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JoinedReviewerProfessorReview_Reviewers_ReviewerId",
                        column: x => x.ReviewerId,
                        principalTable: "Reviewers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JoinedDowSlt_SltId",
                table: "JoinedDowSlt",
                column: "SltId");

            migrationBuilder.CreateIndex(
                name: "IX_JoinedReviewerCourseReview_ReviewerId",
                table: "JoinedReviewerCourseReview",
                column: "ReviewerId");

            migrationBuilder.CreateIndex(
                name: "IX_JoinedReviewerProfessorReview_ReviewerId",
                table: "JoinedReviewerProfessorReview",
                column: "ReviewerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JoinedDowSlt");

            migrationBuilder.DropTable(
                name: "JoinedReviewerCourseReview");

            migrationBuilder.DropTable(
                name: "JoinedReviewerProfessorReview");

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
                        name: "FK_CourseReviewReviewer_CourseReviews_CourseReviewsLikedId",
                        column: x => x.CourseReviewsLikedId,
                        principalTable: "CourseReviews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseReviewReviewer_Reviewers_LikedById",
                        column: x => x.LikedById,
                        principalTable: "Reviewers",
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
                        name: "FK_DayOfWeekEnumEntitySectionLocationAndTime_DaysOfWeek_DaysOf~",
                        column: x => x.DaysOfWeekId,
                        principalTable: "DaysOfWeek",
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
                        name: "FK_ProfessorReviewReviewer_Reviewers_LikedById",
                        column: x => x.LikedById,
                        principalTable: "Reviewers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseReviewReviewer_LikedById",
                table: "CourseReviewReviewer",
                column: "LikedById");

            migrationBuilder.CreateIndex(
                name: "IX_DayOfWeekEnumEntitySectionLocationAndTime_SectionLocationAn~",
                table: "DayOfWeekEnumEntitySectionLocationAndTime",
                column: "SectionLocationAndTimesId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfessorReviewReviewer_ProfessorReviewsLikedId",
                table: "ProfessorReviewReviewer",
                column: "ProfessorReviewsLikedId");
        }
    }
}
