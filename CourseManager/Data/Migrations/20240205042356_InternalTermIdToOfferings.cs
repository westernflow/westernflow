using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class InternalTermIdToOfferings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CourseOfferings_Year_Suffix_CourseId",
                table: "CourseOfferings");

            migrationBuilder.AddColumn<int>(
                name: "TermId",
                table: "CourseOfferings",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CourseOfferings_Year_Suffix_CourseId_CalendarSource",
                table: "CourseOfferings",
                columns: new[] { "Year", "Suffix", "CourseId", "CalendarSource" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CourseOfferings_Year_Suffix_CourseId_CalendarSource",
                table: "CourseOfferings");

            migrationBuilder.DropColumn(
                name: "TermId",
                table: "CourseOfferings");

            migrationBuilder.CreateIndex(
                name: "IX_CourseOfferings_Year_Suffix_CourseId",
                table: "CourseOfferings",
                columns: new[] { "Year", "Suffix", "CourseId" },
                unique: true);
        }
    }
}
