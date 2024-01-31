using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Data.Migrations
{
    public partial class RemoveSectionInfoEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseOffering_Courses_CourseId",
                table: "CourseOffering");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_SourceInfo_SourceInfoId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_JoinedReviewerCourseReview_CourseReviews_CourseReviewId",
                table: "JoinedReviewerCourseReview");

            migrationBuilder.DropForeignKey(
                name: "FK_JoinedReviewerCourseReview_Reviewers_ReviewerId",
                table: "JoinedReviewerCourseReview");

            migrationBuilder.DropForeignKey(
                name: "FK_JoinedReviewerProfessorReview_ProfessorReviews_ProfessorRev~",
                table: "JoinedReviewerProfessorReview");

            migrationBuilder.DropForeignKey(
                name: "FK_JoinedReviewerProfessorReview_Reviewers_ReviewerId",
                table: "JoinedReviewerProfessorReview");

            migrationBuilder.DropForeignKey(
                name: "FK_JoinedSectionProfessor_Professors_ProfessorId",
                table: "JoinedSectionProfessor");

            migrationBuilder.DropForeignKey(
                name: "FK_JoinedSectionProfessor_Sections_SectionId",
                table: "JoinedSectionProfessor");

            migrationBuilder.DropForeignKey(
                name: "FK_Sections_CourseOffering_CourseOfferingId",
                table: "Sections");

            migrationBuilder.DropTable(
                name: "SourceInfo");

            migrationBuilder.DropIndex(
                name: "IX_Courses_SourceInfoId",
                table: "Courses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JoinedSectionProfessor",
                table: "JoinedSectionProfessor");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JoinedReviewerProfessorReview",
                table: "JoinedReviewerProfessorReview");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JoinedReviewerCourseReview",
                table: "JoinedReviewerCourseReview");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseOffering",
                table: "CourseOffering");

            migrationBuilder.DropColumn(
                name: "SourceInfoId",
                table: "Courses");

            migrationBuilder.RenameTable(
                name: "JoinedSectionProfessor",
                newName: "JoinedSectionProfessors");

            migrationBuilder.RenameTable(
                name: "JoinedReviewerProfessorReview",
                newName: "JoinedReviewerProfessorReviews");

            migrationBuilder.RenameTable(
                name: "JoinedReviewerCourseReview",
                newName: "JoinedReviewerCourseReviews");

            migrationBuilder.RenameTable(
                name: "CourseOffering",
                newName: "CourseOfferings");

            migrationBuilder.RenameIndex(
                name: "IX_JoinedSectionProfessor_ProfessorId",
                table: "JoinedSectionProfessors",
                newName: "IX_JoinedSectionProfessors_ProfessorId");

            migrationBuilder.RenameIndex(
                name: "IX_JoinedReviewerProfessorReview_ReviewerId",
                table: "JoinedReviewerProfessorReviews",
                newName: "IX_JoinedReviewerProfessorReviews_ReviewerId");

            migrationBuilder.RenameIndex(
                name: "IX_JoinedReviewerCourseReview_ReviewerId",
                table: "JoinedReviewerCourseReviews",
                newName: "IX_JoinedReviewerCourseReviews_ReviewerId");

            migrationBuilder.RenameIndex(
                name: "IX_CourseOffering_Year_Suffix_CourseId",
                table: "CourseOfferings",
                newName: "IX_CourseOfferings_Year_Suffix_CourseId");

            migrationBuilder.RenameIndex(
                name: "IX_CourseOffering_CourseId",
                table: "CourseOfferings",
                newName: "IX_CourseOfferings_CourseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_JoinedSectionProfessors",
                table: "JoinedSectionProfessors",
                columns: new[] { "SectionId", "ProfessorId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_JoinedReviewerProfessorReviews",
                table: "JoinedReviewerProfessorReviews",
                columns: new[] { "ProfessorReviewId", "ReviewerId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_JoinedReviewerCourseReviews",
                table: "JoinedReviewerCourseReviews",
                columns: new[] { "CourseReviewId", "ReviewerId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseOfferings",
                table: "CourseOfferings",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseOfferings_Courses_CourseId",
                table: "CourseOfferings",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JoinedReviewerCourseReviews_CourseReviews_CourseReviewId",
                table: "JoinedReviewerCourseReviews",
                column: "CourseReviewId",
                principalTable: "CourseReviews",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JoinedReviewerCourseReviews_Reviewers_ReviewerId",
                table: "JoinedReviewerCourseReviews",
                column: "ReviewerId",
                principalTable: "Reviewers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JoinedReviewerProfessorReviews_ProfessorReviews_ProfessorRe~",
                table: "JoinedReviewerProfessorReviews",
                column: "ProfessorReviewId",
                principalTable: "ProfessorReviews",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JoinedReviewerProfessorReviews_Reviewers_ReviewerId",
                table: "JoinedReviewerProfessorReviews",
                column: "ReviewerId",
                principalTable: "Reviewers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JoinedSectionProfessors_Professors_ProfessorId",
                table: "JoinedSectionProfessors",
                column: "ProfessorId",
                principalTable: "Professors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JoinedSectionProfessors_Sections_SectionId",
                table: "JoinedSectionProfessors",
                column: "SectionId",
                principalTable: "Sections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sections_CourseOfferings_CourseOfferingId",
                table: "Sections",
                column: "CourseOfferingId",
                principalTable: "CourseOfferings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseOfferings_Courses_CourseId",
                table: "CourseOfferings");

            migrationBuilder.DropForeignKey(
                name: "FK_JoinedReviewerCourseReviews_CourseReviews_CourseReviewId",
                table: "JoinedReviewerCourseReviews");

            migrationBuilder.DropForeignKey(
                name: "FK_JoinedReviewerCourseReviews_Reviewers_ReviewerId",
                table: "JoinedReviewerCourseReviews");

            migrationBuilder.DropForeignKey(
                name: "FK_JoinedReviewerProfessorReviews_ProfessorReviews_ProfessorRe~",
                table: "JoinedReviewerProfessorReviews");

            migrationBuilder.DropForeignKey(
                name: "FK_JoinedReviewerProfessorReviews_Reviewers_ReviewerId",
                table: "JoinedReviewerProfessorReviews");

            migrationBuilder.DropForeignKey(
                name: "FK_JoinedSectionProfessors_Professors_ProfessorId",
                table: "JoinedSectionProfessors");

            migrationBuilder.DropForeignKey(
                name: "FK_JoinedSectionProfessors_Sections_SectionId",
                table: "JoinedSectionProfessors");

            migrationBuilder.DropForeignKey(
                name: "FK_Sections_CourseOfferings_CourseOfferingId",
                table: "Sections");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JoinedSectionProfessors",
                table: "JoinedSectionProfessors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JoinedReviewerProfessorReviews",
                table: "JoinedReviewerProfessorReviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JoinedReviewerCourseReviews",
                table: "JoinedReviewerCourseReviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseOfferings",
                table: "CourseOfferings");

            migrationBuilder.RenameTable(
                name: "JoinedSectionProfessors",
                newName: "JoinedSectionProfessor");

            migrationBuilder.RenameTable(
                name: "JoinedReviewerProfessorReviews",
                newName: "JoinedReviewerProfessorReview");

            migrationBuilder.RenameTable(
                name: "JoinedReviewerCourseReviews",
                newName: "JoinedReviewerCourseReview");

            migrationBuilder.RenameTable(
                name: "CourseOfferings",
                newName: "CourseOffering");

            migrationBuilder.RenameIndex(
                name: "IX_JoinedSectionProfessors_ProfessorId",
                table: "JoinedSectionProfessor",
                newName: "IX_JoinedSectionProfessor_ProfessorId");

            migrationBuilder.RenameIndex(
                name: "IX_JoinedReviewerProfessorReviews_ReviewerId",
                table: "JoinedReviewerProfessorReview",
                newName: "IX_JoinedReviewerProfessorReview_ReviewerId");

            migrationBuilder.RenameIndex(
                name: "IX_JoinedReviewerCourseReviews_ReviewerId",
                table: "JoinedReviewerCourseReview",
                newName: "IX_JoinedReviewerCourseReview_ReviewerId");

            migrationBuilder.RenameIndex(
                name: "IX_CourseOfferings_Year_Suffix_CourseId",
                table: "CourseOffering",
                newName: "IX_CourseOffering_Year_Suffix_CourseId");

            migrationBuilder.RenameIndex(
                name: "IX_CourseOfferings_CourseId",
                table: "CourseOffering",
                newName: "IX_CourseOffering_CourseId");

            migrationBuilder.AddColumn<int>(
                name: "SourceInfoId",
                table: "Courses",
                type: "integer",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_JoinedSectionProfessor",
                table: "JoinedSectionProfessor",
                columns: new[] { "SectionId", "ProfessorId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_JoinedReviewerProfessorReview",
                table: "JoinedReviewerProfessorReview",
                columns: new[] { "ProfessorReviewId", "ReviewerId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_JoinedReviewerCourseReview",
                table: "JoinedReviewerCourseReview",
                columns: new[] { "CourseReviewId", "ReviewerId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseOffering",
                table: "CourseOffering",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "SourceInfo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    Url = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    Year = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SourceInfo", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Courses_SourceInfoId",
                table: "Courses",
                column: "SourceInfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseOffering_Courses_CourseId",
                table: "CourseOffering",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_SourceInfo_SourceInfoId",
                table: "Courses",
                column: "SourceInfoId",
                principalTable: "SourceInfo",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_JoinedReviewerCourseReview_CourseReviews_CourseReviewId",
                table: "JoinedReviewerCourseReview",
                column: "CourseReviewId",
                principalTable: "CourseReviews",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JoinedReviewerCourseReview_Reviewers_ReviewerId",
                table: "JoinedReviewerCourseReview",
                column: "ReviewerId",
                principalTable: "Reviewers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JoinedReviewerProfessorReview_ProfessorReviews_ProfessorRev~",
                table: "JoinedReviewerProfessorReview",
                column: "ProfessorReviewId",
                principalTable: "ProfessorReviews",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JoinedReviewerProfessorReview_Reviewers_ReviewerId",
                table: "JoinedReviewerProfessorReview",
                column: "ReviewerId",
                principalTable: "Reviewers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JoinedSectionProfessor_Professors_ProfessorId",
                table: "JoinedSectionProfessor",
                column: "ProfessorId",
                principalTable: "Professors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JoinedSectionProfessor_Sections_SectionId",
                table: "JoinedSectionProfessor",
                column: "SectionId",
                principalTable: "Sections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sections_CourseOffering_CourseOfferingId",
                table: "Sections",
                column: "CourseOfferingId",
                principalTable: "CourseOffering",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
