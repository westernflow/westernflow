using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Data.Migrations
{
    public partial class RemoveSectionDataAndFixManyToMany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Professors_ProfessorId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Faculties_Professors_ProfessorId",
                table: "Faculties");

            migrationBuilder.DropForeignKey(
                name: "FK_Professors_SectionComponents_SectionComponentId",
                table: "Professors");

            migrationBuilder.DropForeignKey(
                name: "FK_Sections_SectionComponents_SectionDataId",
                table: "Sections");

            migrationBuilder.DropForeignKey(
                name: "FK_Sections_SourceInfo_SourceId",
                table: "Sections");

            migrationBuilder.DropTable(
                name: "SectionComponents");

            migrationBuilder.DropIndex(
                name: "IX_Sections_SectionDataId",
                table: "Sections");

            migrationBuilder.DropIndex(
                name: "IX_Sections_SourceId",
                table: "Sections");

            migrationBuilder.DropIndex(
                name: "IX_Professors_SectionComponentId",
                table: "Professors");

            migrationBuilder.DropIndex(
                name: "IX_Faculties_ProfessorId",
                table: "Faculties");

            migrationBuilder.DropIndex(
                name: "IX_Courses_ProfessorId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "SectionComponentId",
                table: "Professors");

            migrationBuilder.DropColumn(
                name: "ProfessorId",
                table: "Faculties");

            migrationBuilder.DropColumn(
                name: "ProfessorId",
                table: "Courses");

            migrationBuilder.RenameColumn(
                name: "SourceId",
                table: "Sections",
                newName: "Number");

            migrationBuilder.RenameColumn(
                name: "SectionDataId",
                table: "Sections",
                newName: "ComponentNumber");

            migrationBuilder.AddColumn<string>(
                name: "Campus",
                table: "Sections",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ClassNumber",
                table: "Sections",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Delivery",
                table: "Sections",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProfessorId",
                table: "Sections",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Sections",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TimetableRequisiteString",
                table: "Sections",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AntirequisiteString",
                table: "Courses",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CorequisiteString",
                table: "Courses",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PrerequisiteString",
                table: "Courses",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CourseProfessor",
                columns: table => new
                {
                    CurrentCoursesId = table.Column<int>(type: "integer", nullable: false),
                    ProfessorsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseProfessor", x => new { x.CurrentCoursesId, x.ProfessorsId });
                    table.ForeignKey(
                        name: "FK_CourseProfessor_Courses_CurrentCoursesId",
                        column: x => x.CurrentCoursesId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseProfessor_Professors_ProfessorsId",
                        column: x => x.ProfessorsId,
                        principalTable: "Professors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FacultyProfessor",
                columns: table => new
                {
                    FacultiesId = table.Column<int>(type: "integer", nullable: false),
                    ProfessorsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FacultyProfessor", x => new { x.FacultiesId, x.ProfessorsId });
                    table.ForeignKey(
                        name: "FK_FacultyProfessor_Faculties_FacultiesId",
                        column: x => x.FacultiesId,
                        principalTable: "Faculties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FacultyProfessor_Professors_ProfessorsId",
                        column: x => x.ProfessorsId,
                        principalTable: "Professors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sections_ProfessorId",
                table: "Sections",
                column: "ProfessorId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseProfessor_ProfessorsId",
                table: "CourseProfessor",
                column: "ProfessorsId");

            migrationBuilder.CreateIndex(
                name: "IX_FacultyProfessor_ProfessorsId",
                table: "FacultyProfessor",
                column: "ProfessorsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sections_Professors_ProfessorId",
                table: "Sections",
                column: "ProfessorId",
                principalTable: "Professors",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sections_Professors_ProfessorId",
                table: "Sections");

            migrationBuilder.DropTable(
                name: "CourseProfessor");

            migrationBuilder.DropTable(
                name: "FacultyProfessor");

            migrationBuilder.DropIndex(
                name: "IX_Sections_ProfessorId",
                table: "Sections");

            migrationBuilder.DropColumn(
                name: "Campus",
                table: "Sections");

            migrationBuilder.DropColumn(
                name: "ClassNumber",
                table: "Sections");

            migrationBuilder.DropColumn(
                name: "Delivery",
                table: "Sections");

            migrationBuilder.DropColumn(
                name: "ProfessorId",
                table: "Sections");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Sections");

            migrationBuilder.DropColumn(
                name: "TimetableRequisiteString",
                table: "Sections");

            migrationBuilder.DropColumn(
                name: "AntirequisiteString",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "CorequisiteString",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "PrerequisiteString",
                table: "Courses");

            migrationBuilder.RenameColumn(
                name: "Number",
                table: "Sections",
                newName: "SourceId");

            migrationBuilder.RenameColumn(
                name: "ComponentNumber",
                table: "Sections",
                newName: "SectionDataId");

            migrationBuilder.AddColumn<int>(
                name: "SectionComponentId",
                table: "Professors",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProfessorId",
                table: "Faculties",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProfessorId",
                table: "Courses",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SectionComponents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CourseId = table.Column<int>(type: "integer", nullable: false),
                    AntirequisiteString = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    Campus = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    ClassNumber = table.Column<int>(type: "integer", nullable: false),
                    ComponentNumber = table.Column<int>(type: "integer", nullable: false),
                    CorequisiteString = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    Delivery = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Number = table.Column<int>(type: "integer", nullable: false),
                    PrerequisiteString = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    Status = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    TimetableRequisiteString = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SectionComponents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SectionComponents_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sections_SectionDataId",
                table: "Sections",
                column: "SectionDataId");

            migrationBuilder.CreateIndex(
                name: "IX_Sections_SourceId",
                table: "Sections",
                column: "SourceId");

            migrationBuilder.CreateIndex(
                name: "IX_Professors_SectionComponentId",
                table: "Professors",
                column: "SectionComponentId");

            migrationBuilder.CreateIndex(
                name: "IX_Faculties_ProfessorId",
                table: "Faculties",
                column: "ProfessorId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_ProfessorId",
                table: "Courses",
                column: "ProfessorId");

            migrationBuilder.CreateIndex(
                name: "IX_SectionComponents_CourseId",
                table: "SectionComponents",
                column: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Professors_ProfessorId",
                table: "Courses",
                column: "ProfessorId",
                principalTable: "Professors",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Faculties_Professors_ProfessorId",
                table: "Faculties",
                column: "ProfessorId",
                principalTable: "Professors",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Professors_SectionComponents_SectionComponentId",
                table: "Professors",
                column: "SectionComponentId",
                principalTable: "SectionComponents",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Sections_SectionComponents_SectionDataId",
                table: "Sections",
                column: "SectionDataId",
                principalTable: "SectionComponents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sections_SourceInfo_SourceId",
                table: "Sections",
                column: "SourceId",
                principalTable: "SourceInfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
