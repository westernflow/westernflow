using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class SimplifyReviewer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Reviewers_Email",
                table: "Reviewers");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Reviewers");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Reviewers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Reviewers");

            migrationBuilder.RenameColumn(
                name: "DateWritten",
                table: "CourseReviews",
                newName: "CreatedAt");

            migrationBuilder.AddColumn<string>(
                name: "SubjectId",
                table: "Reviewers",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "ReviewText",
                table: "ProfessorReviews",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "ReviewText",
                table: "CourseReviews",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500);

            migrationBuilder.CreateIndex(
                name: "IX_Reviewers_SubjectId",
                table: "Reviewers",
                column: "SubjectId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Reviewers_SubjectId",
                table: "Reviewers");

            migrationBuilder.DropColumn(
                name: "SubjectId",
                table: "Reviewers");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "CourseReviews",
                newName: "DateWritten");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Reviewers",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Reviewers",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Reviewers",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "ReviewText",
                table: "ProfessorReviews",
                type: "character varying(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(1000)",
                oldMaxLength: 1000);

            migrationBuilder.AlterColumn<string>(
                name: "ReviewText",
                table: "CourseReviews",
                type: "character varying(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(1000)",
                oldMaxLength: 1000);

            migrationBuilder.CreateIndex(
                name: "IX_Reviewers_Email",
                table: "Reviewers",
                column: "Email",
                unique: true);
        }
    }
}
