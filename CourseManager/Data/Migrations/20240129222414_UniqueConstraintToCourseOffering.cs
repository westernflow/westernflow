using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class UniqueConstraintToCourseOffering : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_CourseOffering_Year_Suffix_CourseId",
                table: "CourseOffering",
                columns: new[] { "Year", "Suffix", "CourseId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CourseOffering_Year_Suffix_CourseId",
                table: "CourseOffering");
        }
    }
}
