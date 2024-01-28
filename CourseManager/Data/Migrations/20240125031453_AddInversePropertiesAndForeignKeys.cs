using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class AddInversePropertiesAndForeignKeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "ProfessorId",
                table: "Sections");

            migrationBuilder.CreateTable(
                name: "ProfessorSection",
                columns: table => new
                {
                    ProfessorsId = table.Column<int>(type: "integer", nullable: false),
                    SectionsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfessorSection", x => new { x.ProfessorsId, x.SectionsId });
                    table.ForeignKey(
                        name: "FK_ProfessorSection_Professors_ProfessorsId",
                        column: x => x.ProfessorsId,
                        principalTable: "Professors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProfessorSection_Sections_SectionsId",
                        column: x => x.SectionsId,
                        principalTable: "Sections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProfessorSection_SectionsId",
                table: "ProfessorSection",
                column: "SectionsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProfessorSection");

            migrationBuilder.AddColumn<int>(
                name: "ProfessorId",
                table: "Sections",
                type: "integer",
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
    }
}
