using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Data.Migrations
{
    public partial class GroupByOffering : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sections_Courses_CourseId",
                table: "Sections");

            migrationBuilder.RenameColumn(
                name: "CourseId",
                table: "Sections",
                newName: "CourseOfferingId");

            migrationBuilder.RenameIndex(
                name: "IX_Sections_CourseId",
                table: "Sections",
                newName: "IX_Sections_CourseOfferingId");

            migrationBuilder.RenameColumn(
                name: "Suffix",
                table: "Courses",
                newName: "InternalCourseId");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "JoinedSectionProfessor",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "JoinedReviewerProfessorReview",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "JoinedReviewerCourseReview",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "JoinedDowSlt",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "CourseOffering",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Year = table.Column<int>(type: "integer", nullable: false),
                    Suffix = table.Column<int>(type: "integer", nullable: false),
                    CourseId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseOffering", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseOffering_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseOffering_CourseId",
                table: "CourseOffering",
                column: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sections_CourseOffering_CourseOfferingId",
                table: "Sections",
                column: "CourseOfferingId",
                principalTable: "CourseOffering",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sections_CourseOffering_CourseOfferingId",
                table: "Sections");

            migrationBuilder.DropTable(
                name: "CourseOffering");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "JoinedSectionProfessor");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "JoinedReviewerProfessorReview");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "JoinedReviewerCourseReview");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "JoinedDowSlt");

            migrationBuilder.RenameColumn(
                name: "CourseOfferingId",
                table: "Sections",
                newName: "CourseId");

            migrationBuilder.RenameIndex(
                name: "IX_Sections_CourseOfferingId",
                table: "Sections",
                newName: "IX_Sections_CourseId");

            migrationBuilder.RenameColumn(
                name: "InternalCourseId",
                table: "Courses",
                newName: "Suffix");

            migrationBuilder.AddForeignKey(
                name: "FK_Sections_Courses_CourseId",
                table: "Sections",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
