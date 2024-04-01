using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class UseSkipPropertiesInsteadOfJoinTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JoinedReviewerCourseReviews");

            migrationBuilder.DropTable(
                name: "JoinedReviewerProfessorReviews");

            migrationBuilder.DropTable(
                name: "JoinedSectionProfessors");

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

            migrationBuilder.CreateTable(
                name: "ProfessorSection",
                columns: table => new
                {
                    ProfessorsId = table.Column<int>(type: "integer", nullable: false),
                    SectionsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfessorSection", x => new { x.ProfessorsId, x.SectionsId });
                    table.ForeignKey(
                        name: "FK_ProfessorSection_Professors_ProfessorsId",
                        column: x => x.ProfessorsId,
                        principalTable: "Professors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProfessorSection_Sections_SectionsId",
                        column: x => x.SectionsId,
                        principalTable: "Sections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseReviewReviewer_LikedById",
                table: "CourseReviewReviewer",
                column: "LikedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProfessorReviewReviewer_ProfessorReviewsLikedId",
                table: "ProfessorReviewReviewer",
                column: "ProfessorReviewsLikedId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfessorSection_SectionsId",
                table: "ProfessorSection",
                column: "SectionsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseReviewReviewer");

            migrationBuilder.DropTable(
                name: "ProfessorReviewReviewer");

            migrationBuilder.DropTable(
                name: "ProfessorSection");

            migrationBuilder.CreateTable(
                name: "JoinedReviewerCourseReviews",
                columns: table => new
                {
                    CourseReviewId = table.Column<int>(type: "integer", nullable: false),
                    ReviewerId = table.Column<int>(type: "integer", nullable: false),
                    Id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JoinedReviewerCourseReviews", x => new { x.CourseReviewId, x.ReviewerId });
                    table.ForeignKey(
                        name: "FK_JoinedReviewerCourseReviews_CourseReviews_CourseReviewId",
                        column: x => x.CourseReviewId,
                        principalTable: "CourseReviews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JoinedReviewerCourseReviews_Reviewers_ReviewerId",
                        column: x => x.ReviewerId,
                        principalTable: "Reviewers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JoinedReviewerProfessorReviews",
                columns: table => new
                {
                    ProfessorReviewId = table.Column<int>(type: "integer", nullable: false),
                    ReviewerId = table.Column<int>(type: "integer", nullable: false),
                    Id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JoinedReviewerProfessorReviews", x => new { x.ProfessorReviewId, x.ReviewerId });
                    table.ForeignKey(
                        name: "FK_JoinedReviewerProfessorReviews_ProfessorReviews_ProfessorRe~",
                        column: x => x.ProfessorReviewId,
                        principalTable: "ProfessorReviews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JoinedReviewerProfessorReviews_Reviewers_ReviewerId",
                        column: x => x.ReviewerId,
                        principalTable: "Reviewers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JoinedSectionProfessors",
                columns: table => new
                {
                    SectionId = table.Column<int>(type: "integer", nullable: false),
                    ProfessorId = table.Column<int>(type: "integer", nullable: false),
                    Id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JoinedSectionProfessors", x => new { x.SectionId, x.ProfessorId });
                    table.ForeignKey(
                        name: "FK_JoinedSectionProfessors_Professors_ProfessorId",
                        column: x => x.ProfessorId,
                        principalTable: "Professors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JoinedSectionProfessors_Sections_SectionId",
                        column: x => x.SectionId,
                        principalTable: "Sections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JoinedReviewerCourseReviews_ReviewerId",
                table: "JoinedReviewerCourseReviews",
                column: "ReviewerId");

            migrationBuilder.CreateIndex(
                name: "IX_JoinedReviewerProfessorReviews_ReviewerId",
                table: "JoinedReviewerProfessorReviews",
                column: "ReviewerId");

            migrationBuilder.CreateIndex(
                name: "IX_JoinedSectionProfessors_ProfessorId",
                table: "JoinedSectionProfessors",
                column: "ProfessorId");
        }
    }
}
