using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class AddTrackableReviews : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateWritten",
                table: "ProfessorReviews",
                newName: "ModifiedDate");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "CourseReviews",
                newName: "ModifiedDate");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "ProfessorReviews",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "CourseReviews",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "ProfessorReviews");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "CourseReviews");

            migrationBuilder.RenameColumn(
                name: "ModifiedDate",
                table: "ProfessorReviews",
                newName: "DateWritten");

            migrationBuilder.RenameColumn(
                name: "ModifiedDate",
                table: "CourseReviews",
                newName: "CreatedAt");
        }
    }
}
