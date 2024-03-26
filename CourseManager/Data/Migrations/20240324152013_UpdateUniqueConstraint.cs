using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class UpdateUniqueConstraint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Sections_Number_CourseOfferingId",
                table: "Sections");

            migrationBuilder.CreateIndex(
                name: "IX_Sections_Number_CourseOfferingId_ClassNumber",
                table: "Sections",
                columns: new[] { "Number", "CourseOfferingId", "ClassNumber" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Sections_Number_CourseOfferingId_ClassNumber",
                table: "Sections");

            migrationBuilder.CreateIndex(
                name: "IX_Sections_Number_CourseOfferingId",
                table: "Sections",
                columns: new[] { "Number", "CourseOfferingId" },
                unique: true);
        }
    }
}
