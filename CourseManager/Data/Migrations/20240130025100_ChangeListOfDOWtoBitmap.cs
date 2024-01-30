using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Data.Migrations
{
    public partial class ChangeListOfDOWtoBitmap : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JoinedDowSlt");

            migrationBuilder.DropTable(
                name: "DaysOfWeek");

            migrationBuilder.DropTable(
                name: "SectionLocationAndTime");

            migrationBuilder.CreateTable(
                name: "TimingDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DaysOfWeekBitmap = table.Column<string>(type: "character varying(7)", maxLength: 7, nullable: false),
                    Location = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Time = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    SectionId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimingDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TimingDetails_Sections_SectionId",
                        column: x => x.SectionId,
                        principalTable: "Sections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TimingDetails_SectionId",
                table: "TimingDetails",
                column: "SectionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TimingDetails");

            migrationBuilder.CreateTable(
                name: "DaysOfWeek",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DayOfWeek = table.Column<int>(type: "integer", nullable: false),
                    DayOfWeekString = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DaysOfWeek", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SectionLocationAndTime",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SectionId = table.Column<int>(type: "integer", nullable: false),
                    Location = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Time = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SectionLocationAndTime", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SectionLocationAndTime_Sections_SectionId",
                        column: x => x.SectionId,
                        principalTable: "Sections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JoinedDowSlt",
                columns: table => new
                {
                    DowId = table.Column<int>(type: "integer", nullable: false),
                    SltId = table.Column<int>(type: "integer", nullable: false),
                    Id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JoinedDowSlt", x => new { x.DowId, x.SltId });
                    table.ForeignKey(
                        name: "FK_JoinedDowSlt_DaysOfWeek_DowId",
                        column: x => x.DowId,
                        principalTable: "DaysOfWeek",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JoinedDowSlt_SectionLocationAndTime_SltId",
                        column: x => x.SltId,
                        principalTable: "SectionLocationAndTime",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DaysOfWeek_DayOfWeek",
                table: "DaysOfWeek",
                column: "DayOfWeek",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_JoinedDowSlt_SltId",
                table: "JoinedDowSlt",
                column: "SltId");

            migrationBuilder.CreateIndex(
                name: "IX_SectionLocationAndTime_SectionId",
                table: "SectionLocationAndTime",
                column: "SectionId");
        }
    }
}
