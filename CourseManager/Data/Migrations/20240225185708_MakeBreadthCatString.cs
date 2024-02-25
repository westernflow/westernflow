using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class MakeBreadthCatString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BreadthCategory",
                table: "Courses");

            migrationBuilder.AddColumn<string>(
                name: "BreadthCategories",
                table: "Courses",
                type: "character varying(20)",
                maxLength: 20,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BreadthCategories",
                table: "Courses");

            migrationBuilder.AddColumn<int>(
                name: "BreadthCategory",
                table: "Courses",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
