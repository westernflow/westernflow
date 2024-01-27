using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class NullableCourseIdForProfessorReviews : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProfessorReviews_Courses_CourseId",
                table: "ProfessorReviews");

            migrationBuilder.AlterColumn<int>(
                name: "CourseId",
                table: "ProfessorReviews",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_ProfessorReviews_Courses_CourseId",
                table: "ProfessorReviews",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProfessorReviews_Courses_CourseId",
                table: "ProfessorReviews");

            migrationBuilder.AlterColumn<int>(
                name: "CourseId",
                table: "ProfessorReviews",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProfessorReviews_Courses_CourseId",
                table: "ProfessorReviews",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
