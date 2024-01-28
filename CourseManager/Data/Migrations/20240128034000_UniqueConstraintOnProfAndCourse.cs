using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class UniqueConstraintOnProfAndCourse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_SourceInfo_SourceInfoId",
                table: "Courses");

            migrationBuilder.AlterColumn<int>(
                name: "SourceInfoId",
                table: "Courses",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.CreateIndex(
                name: "IX_Reviewers_Email",
                table: "Reviewers",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Professors_Name",
                table: "Professors",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Courses_Number_FacultyId",
                table: "Courses",
                columns: new[] { "Number", "FacultyId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_SourceInfo_SourceInfoId",
                table: "Courses",
                column: "SourceInfoId",
                principalTable: "SourceInfo",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_SourceInfo_SourceInfoId",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Reviewers_Email",
                table: "Reviewers");

            migrationBuilder.DropIndex(
                name: "IX_Professors_Name",
                table: "Professors");

            migrationBuilder.DropIndex(
                name: "IX_Courses_Number_FacultyId",
                table: "Courses");

            migrationBuilder.AlterColumn<int>(
                name: "SourceInfoId",
                table: "Courses",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_SourceInfo_SourceInfoId",
                table: "Courses",
                column: "SourceInfoId",
                principalTable: "SourceInfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
