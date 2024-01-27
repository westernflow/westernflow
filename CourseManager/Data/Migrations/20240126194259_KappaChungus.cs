using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class KappaChungus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseReview_Professors_ProfessorId",
                table: "CourseReview");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseReview_Reviewer_ReviewerId",
                table: "CourseReview");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseReviewReviewer_CourseReview_CourseReviewsLikedId",
                table: "CourseReviewReviewer");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseReviewReviewer_Reviewer_LikedById",
                table: "CourseReviewReviewer");

            migrationBuilder.DropForeignKey(
                name: "FK_DayOfWeekEnumEntitySectionLocationAndTime_DayOfWeekEnumEnti~",
                table: "DayOfWeekEnumEntitySectionLocationAndTime");

            migrationBuilder.DropForeignKey(
                name: "FK_ProfessorReviewReviewer_Reviewer_LikedById",
                table: "ProfessorReviewReviewer");

            migrationBuilder.DropForeignKey(
                name: "FK_ProfessorReviews_Reviewer_ReviewerId",
                table: "ProfessorReviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reviewer",
                table: "Reviewer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DayOfWeekEnumEntity",
                table: "DayOfWeekEnumEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseReview",
                table: "CourseReview");

            migrationBuilder.DropColumn(
                name: "SectionRequisiteString",
                table: "Sections");

            migrationBuilder.RenameTable(
                name: "Reviewer",
                newName: "Reviewers");

            migrationBuilder.RenameTable(
                name: "DayOfWeekEnumEntity",
                newName: "DaysOfWeek");

            migrationBuilder.RenameTable(
                name: "CourseReview",
                newName: "CourseReviews");

            migrationBuilder.RenameIndex(
                name: "IX_DayOfWeekEnumEntity_DayOfWeek",
                table: "DaysOfWeek",
                newName: "IX_DaysOfWeek_DayOfWeek");

            migrationBuilder.RenameIndex(
                name: "IX_CourseReview_ReviewerId",
                table: "CourseReviews",
                newName: "IX_CourseReviews_ReviewerId");

            migrationBuilder.RenameIndex(
                name: "IX_CourseReview_ProfessorId",
                table: "CourseReviews",
                newName: "IX_CourseReviews_ProfessorId");

            migrationBuilder.AlterColumn<string>(
                name: "Year",
                table: "SourceInfo",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(10)",
                oldMaxLength: 10);

            migrationBuilder.AddColumn<string>(
                name: "DayOfWeekString",
                table: "DaysOfWeek",
                type: "character varying(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reviewers",
                table: "Reviewers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DaysOfWeek",
                table: "DaysOfWeek",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseReviews",
                table: "CourseReviews",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseReviewReviewer_CourseReviews_CourseReviewsLikedId",
                table: "CourseReviewReviewer",
                column: "CourseReviewsLikedId",
                principalTable: "CourseReviews",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseReviewReviewer_Reviewers_LikedById",
                table: "CourseReviewReviewer",
                column: "LikedById",
                principalTable: "Reviewers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseReviews_Professors_ProfessorId",
                table: "CourseReviews",
                column: "ProfessorId",
                principalTable: "Professors",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseReviews_Reviewers_ReviewerId",
                table: "CourseReviews",
                column: "ReviewerId",
                principalTable: "Reviewers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DayOfWeekEnumEntitySectionLocationAndTime_DaysOfWeek_DaysOf~",
                table: "DayOfWeekEnumEntitySectionLocationAndTime",
                column: "DaysOfWeekId",
                principalTable: "DaysOfWeek",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProfessorReviewReviewer_Reviewers_LikedById",
                table: "ProfessorReviewReviewer",
                column: "LikedById",
                principalTable: "Reviewers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProfessorReviews_Reviewers_ReviewerId",
                table: "ProfessorReviews",
                column: "ReviewerId",
                principalTable: "Reviewers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseReviewReviewer_CourseReviews_CourseReviewsLikedId",
                table: "CourseReviewReviewer");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseReviewReviewer_Reviewers_LikedById",
                table: "CourseReviewReviewer");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseReviews_Professors_ProfessorId",
                table: "CourseReviews");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseReviews_Reviewers_ReviewerId",
                table: "CourseReviews");

            migrationBuilder.DropForeignKey(
                name: "FK_DayOfWeekEnumEntitySectionLocationAndTime_DaysOfWeek_DaysOf~",
                table: "DayOfWeekEnumEntitySectionLocationAndTime");

            migrationBuilder.DropForeignKey(
                name: "FK_ProfessorReviewReviewer_Reviewers_LikedById",
                table: "ProfessorReviewReviewer");

            migrationBuilder.DropForeignKey(
                name: "FK_ProfessorReviews_Reviewers_ReviewerId",
                table: "ProfessorReviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reviewers",
                table: "Reviewers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DaysOfWeek",
                table: "DaysOfWeek");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseReviews",
                table: "CourseReviews");

            migrationBuilder.DropColumn(
                name: "DayOfWeekString",
                table: "DaysOfWeek");

            migrationBuilder.RenameTable(
                name: "Reviewers",
                newName: "Reviewer");

            migrationBuilder.RenameTable(
                name: "DaysOfWeek",
                newName: "DayOfWeekEnumEntity");

            migrationBuilder.RenameTable(
                name: "CourseReviews",
                newName: "CourseReview");

            migrationBuilder.RenameIndex(
                name: "IX_DaysOfWeek_DayOfWeek",
                table: "DayOfWeekEnumEntity",
                newName: "IX_DayOfWeekEnumEntity_DayOfWeek");

            migrationBuilder.RenameIndex(
                name: "IX_CourseReviews_ReviewerId",
                table: "CourseReview",
                newName: "IX_CourseReview_ReviewerId");

            migrationBuilder.RenameIndex(
                name: "IX_CourseReviews_ProfessorId",
                table: "CourseReview",
                newName: "IX_CourseReview_ProfessorId");

            migrationBuilder.AlterColumn<string>(
                name: "Year",
                table: "SourceInfo",
                type: "character varying(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<string>(
                name: "SectionRequisiteString",
                table: "Sections",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reviewer",
                table: "Reviewer",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DayOfWeekEnumEntity",
                table: "DayOfWeekEnumEntity",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseReview",
                table: "CourseReview",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseReview_Professors_ProfessorId",
                table: "CourseReview",
                column: "ProfessorId",
                principalTable: "Professors",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseReview_Reviewer_ReviewerId",
                table: "CourseReview",
                column: "ReviewerId",
                principalTable: "Reviewer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseReviewReviewer_CourseReview_CourseReviewsLikedId",
                table: "CourseReviewReviewer",
                column: "CourseReviewsLikedId",
                principalTable: "CourseReview",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseReviewReviewer_Reviewer_LikedById",
                table: "CourseReviewReviewer",
                column: "LikedById",
                principalTable: "Reviewer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DayOfWeekEnumEntitySectionLocationAndTime_DayOfWeekEnumEnti~",
                table: "DayOfWeekEnumEntitySectionLocationAndTime",
                column: "DaysOfWeekId",
                principalTable: "DayOfWeekEnumEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProfessorReviewReviewer_Reviewer_LikedById",
                table: "ProfessorReviewReviewer",
                column: "LikedById",
                principalTable: "Reviewer",
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
    }
}
