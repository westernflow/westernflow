using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class AddCourseIdToCourseReview : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CourseId",
                table: "CourseReviews",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CourseReviews_CourseId",
                table: "CourseReviews",
                column: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseReviews_Courses_CourseId",
                table: "CourseReviews",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseReviews_Courses_CourseId",
                table: "CourseReviews");

            migrationBuilder.DropIndex(
                name: "IX_CourseReviews_CourseId",
                table: "CourseReviews");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "CourseReviews");
        }
    }
}
