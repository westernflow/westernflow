using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class SectionAndTimingDetailIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_TimingDetails_DaysOfWeekBitmap_Time_SectionId",
                table: "TimingDetails",
                columns: new[] { "DaysOfWeekBitmap", "Time", "SectionId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sections_Number_CourseOfferingId",
                table: "Sections",
                columns: new[] { "Number", "CourseOfferingId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TimingDetails_DaysOfWeekBitmap_Time_SectionId",
                table: "TimingDetails");

            migrationBuilder.DropIndex(
                name: "IX_Sections_Number_CourseOfferingId",
                table: "Sections");
        }
    }
}
