using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class Fixup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Date",
                table: "ProfessorReviews",
                newName: "DateWritten");

            migrationBuilder.RenameColumn(
                name: "FacultyName",
                table: "Faculties",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "FacultyAbbreviation",
                table: "Faculties",
                newName: "Abbreviation");

            migrationBuilder.RenameIndex(
                name: "IX_Faculties_FacultyName",
                table: "Faculties",
                newName: "IX_Faculties_Name");

            migrationBuilder.RenameColumn(
                name: "CourseWeight",
                table: "Courses",
                newName: "Weight");

            migrationBuilder.RenameColumn(
                name: "CourseSuffix",
                table: "Courses",
                newName: "Suffix");

            migrationBuilder.AlterColumn<string>(
                name: "RmpId",
                table: "Professors",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateWritten",
                table: "CourseReview",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "ProfessorId",
                table: "CourseReview",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CourseReview_ProfessorId",
                table: "CourseReview",
                column: "ProfessorId");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseReview_Professors_ProfessorId",
                table: "CourseReview",
                column: "ProfessorId",
                principalTable: "Professors",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseReview_Professors_ProfessorId",
                table: "CourseReview");

            migrationBuilder.DropIndex(
                name: "IX_CourseReview_ProfessorId",
                table: "CourseReview");

            migrationBuilder.DropColumn(
                name: "DateWritten",
                table: "CourseReview");

            migrationBuilder.DropColumn(
                name: "ProfessorId",
                table: "CourseReview");

            migrationBuilder.RenameColumn(
                name: "DateWritten",
                table: "ProfessorReviews",
                newName: "Date");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Faculties",
                newName: "FacultyName");

            migrationBuilder.RenameColumn(
                name: "Abbreviation",
                table: "Faculties",
                newName: "FacultyAbbreviation");

            migrationBuilder.RenameIndex(
                name: "IX_Faculties_Name",
                table: "Faculties",
                newName: "IX_Faculties_FacultyName");

            migrationBuilder.RenameColumn(
                name: "Weight",
                table: "Courses",
                newName: "CourseWeight");

            migrationBuilder.RenameColumn(
                name: "Suffix",
                table: "Courses",
                newName: "CourseSuffix");

            migrationBuilder.AlterColumn<string>(
                name: "RmpId",
                table: "Professors",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldNullable: true);
        }
    }
}
