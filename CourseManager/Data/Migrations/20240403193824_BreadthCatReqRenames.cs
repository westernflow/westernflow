using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class BreadthCatReqRenames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AntirequisiteString",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "CorequisiteString",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "PrerequisiteString",
                table: "Courses");

            migrationBuilder.AddColumn<string>(
                name: "EnumBitmap",
                table: "Faculties",
                type: "character varying(3)",
                maxLength: 3,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ExtraInformation",
                table: "Courses",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(1000)",
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Courses",
                type: "character varying(3000)",
                maxLength: 3000,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(3000)",
                oldMaxLength: 3000,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BreadthCategories",
                table: "Courses",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Antirequisites",
                table: "Courses",
                type: "character varying(3000)",
                maxLength: 3000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Corequisites",
                table: "Courses",
                type: "character varying(3000)",
                maxLength: 3000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Prerequisites",
                table: "Courses",
                type: "character varying(3000)",
                maxLength: 3000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Faculties_Abbreviation",
                table: "Faculties",
                column: "Abbreviation",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Faculties_Abbreviation",
                table: "Faculties");

            migrationBuilder.DropColumn(
                name: "EnumBitmap",
                table: "Faculties");

            migrationBuilder.DropColumn(
                name: "Antirequisites",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "Corequisites",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "Prerequisites",
                table: "Courses");

            migrationBuilder.AlterColumn<string>(
                name: "ExtraInformation",
                table: "Courses",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(1000)",
                oldMaxLength: 1000);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Courses",
                type: "character varying(3000)",
                maxLength: 3000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(3000)",
                oldMaxLength: 3000);

            migrationBuilder.AlterColumn<string>(
                name: "BreadthCategories",
                table: "Courses",
                type: "character varying(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20);

            migrationBuilder.AddColumn<string>(
                name: "AntirequisiteString",
                table: "Courses",
                type: "character varying(3000)",
                maxLength: 3000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CorequisiteString",
                table: "Courses",
                type: "character varying(3000)",
                maxLength: 3000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PrerequisiteString",
                table: "Courses",
                type: "character varying(3000)",
                maxLength: 3000,
                nullable: true);
        }
    }
}
