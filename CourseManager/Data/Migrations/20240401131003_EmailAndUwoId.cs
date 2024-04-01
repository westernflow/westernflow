using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class EmailAndUwoId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Professors",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UwoId",
                table: "Professors",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Professors_Email",
                table: "Professors",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Professors_UwoId",
                table: "Professors",
                column: "UwoId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Professors_Email",
                table: "Professors");

            migrationBuilder.DropIndex(
                name: "IX_Professors_UwoId",
                table: "Professors");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Professors");

            migrationBuilder.DropColumn(
                name: "UwoId",
                table: "Professors");
        }
    }
}
