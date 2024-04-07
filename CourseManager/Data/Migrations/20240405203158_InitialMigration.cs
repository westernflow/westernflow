using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Data.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Faculties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EnumBitmap = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: true),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Abbreviation = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Faculties", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Professors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    UwoId = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    RmpId = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Professors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Reviewers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SubjectId = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviewers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Number = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Prerequisites = table.Column<string>(type: "character varying(3000)", maxLength: 3000, nullable: false),
                    Corequisites = table.Column<string>(type: "character varying(3000)", maxLength: 3000, nullable: false),
                    Antirequisites = table.Column<string>(type: "character varying(3000)", maxLength: 3000, nullable: false),
                    Description = table.Column<string>(type: "character varying(3000)", maxLength: 3000, nullable: false),
                    ExtraInformation = table.Column<string>(type: "character varying(3000)", maxLength: 3000, nullable: false),
                    Weight = table.Column<decimal>(type: "numeric(3,2)", nullable: true),
                    InternalCourseId = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    FacultyId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Courses_Faculties_FacultyId",
                        column: x => x.FacultyId,
                        principalTable: "Faculties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CourseOfferings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TermId = table.Column<int>(type: "integer", nullable: false),
                    Year = table.Column<int>(type: "integer", nullable: false),
                    CalendarSource = table.Column<int>(type: "integer", nullable: false),
                    Suffix = table.Column<int>(type: "integer", nullable: false),
                    CourseId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseOfferings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseOfferings_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CourseReviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ReviewerId = table.Column<int>(type: "integer", nullable: false),
                    CourseId = table.Column<int>(type: "integer", nullable: false),
                    IsLiked = table.Column<bool>(type: "boolean", nullable: false),
                    EasyRating = table.Column<int>(type: "integer", nullable: false),
                    UsefulRating = table.Column<int>(type: "integer", nullable: false),
                    ReviewText = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    ProfessorId = table.Column<int>(type: "integer", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseReviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseReviews_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseReviews_Professors_ProfessorId",
                        column: x => x.ProfessorId,
                        principalTable: "Professors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CourseReviews_Reviewers_ReviewerId",
                        column: x => x.ReviewerId,
                        principalTable: "Reviewers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProfessorReviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Quality = table.Column<int>(type: "integer", nullable: false),
                    Difficulty = table.Column<int>(type: "integer", nullable: false),
                    Helpful = table.Column<int>(type: "integer", nullable: false),
                    Clarity = table.Column<int>(type: "integer", nullable: false),
                    ReviewText = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    ReviewerId = table.Column<int>(type: "integer", nullable: false),
                    CourseId = table.Column<int>(type: "integer", nullable: true),
                    ProfessorId = table.Column<int>(type: "integer", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfessorReviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProfessorReviews_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProfessorReviews_Professors_ProfessorId",
                        column: x => x.ProfessorId,
                        principalTable: "Professors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProfessorReviews_Reviewers_ReviewerId",
                        column: x => x.ReviewerId,
                        principalTable: "Reviewers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Number = table.Column<int>(type: "integer", nullable: false),
                    ComponentType = table.Column<int>(type: "integer", nullable: false),
                    ClassNumber = table.Column<int>(type: "integer", nullable: false),
                    TimetableRequisiteString = table.Column<string>(type: "character varying(5000)", maxLength: 5000, nullable: true),
                    WaitListSize = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    Campus = table.Column<int>(type: "integer", nullable: false),
                    Delivery = table.Column<int>(type: "integer", nullable: false),
                    ProfessorNames = table.Column<List<string>>(type: "text[]", nullable: false),
                    TimingDetailsText = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    CourseOfferingId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sections_CourseOfferings_CourseOfferingId",
                        column: x => x.CourseOfferingId,
                        principalTable: "CourseOfferings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CourseReviewReviewer",
                columns: table => new
                {
                    CourseReviewsLikedId = table.Column<int>(type: "integer", nullable: false),
                    LikedById = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseReviewReviewer", x => new { x.CourseReviewsLikedId, x.LikedById });
                    table.ForeignKey(
                        name: "FK_CourseReviewReviewer_CourseReviews_CourseReviewsLikedId",
                        column: x => x.CourseReviewsLikedId,
                        principalTable: "CourseReviews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseReviewReviewer_Reviewers_LikedById",
                        column: x => x.LikedById,
                        principalTable: "Reviewers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProfessorReviewReviewer",
                columns: table => new
                {
                    LikedById = table.Column<int>(type: "integer", nullable: false),
                    ProfessorReviewsLikedId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfessorReviewReviewer", x => new { x.LikedById, x.ProfessorReviewsLikedId });
                    table.ForeignKey(
                        name: "FK_ProfessorReviewReviewer_ProfessorReviews_ProfessorReviewsLi~",
                        column: x => x.ProfessorReviewsLikedId,
                        principalTable: "ProfessorReviews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProfessorReviewReviewer_Reviewers_LikedById",
                        column: x => x.LikedById,
                        principalTable: "Reviewers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                name: "IX_CourseOfferings_CourseId",
                table: "CourseOfferings",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseOfferings_Year_Suffix_CourseId_CalendarSource",
                table: "CourseOfferings",
                columns: new[] { "Year", "Suffix", "CourseId", "CalendarSource" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CourseReviewReviewer_LikedById",
                table: "CourseReviewReviewer",
                column: "LikedById");

            migrationBuilder.CreateIndex(
                name: "IX_CourseReviews_CourseId",
                table: "CourseReviews",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseReviews_ProfessorId",
                table: "CourseReviews",
                column: "ProfessorId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseReviews_ReviewerId",
                table: "CourseReviews",
                column: "ReviewerId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_FacultyId",
                table: "Courses",
                column: "FacultyId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_Number_FacultyId",
                table: "Courses",
                columns: new[] { "Number", "FacultyId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Faculties_Abbreviation",
                table: "Faculties",
                column: "Abbreviation",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Faculties_Name",
                table: "Faculties",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProfessorReviewReviewer_ProfessorReviewsLikedId",
                table: "ProfessorReviewReviewer",
                column: "ProfessorReviewsLikedId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfessorReviews_CourseId",
                table: "ProfessorReviews",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfessorReviews_ProfessorId",
                table: "ProfessorReviews",
                column: "ProfessorId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfessorReviews_ReviewerId",
                table: "ProfessorReviews",
                column: "ReviewerId");

            migrationBuilder.CreateIndex(
                name: "IX_Professors_Email",
                table: "Professors",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Professors_Name",
                table: "Professors",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Professors_UwoId",
                table: "Professors",
                column: "UwoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProfessorSection_SectionsId",
                table: "ProfessorSection",
                column: "SectionsId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviewers_SubjectId",
                table: "Reviewers",
                column: "SubjectId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sections_CourseOfferingId",
                table: "Sections",
                column: "CourseOfferingId");

            migrationBuilder.CreateIndex(
                name: "IX_Sections_Number_CourseOfferingId_ClassNumber",
                table: "Sections",
                columns: new[] { "Number", "CourseOfferingId", "ClassNumber" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TimingDetails_DaysOfWeekBitmap_Time_SectionId",
                table: "TimingDetails",
                columns: new[] { "DaysOfWeekBitmap", "Time", "SectionId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TimingDetails_SectionId",
                table: "TimingDetails",
                column: "SectionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseReviewReviewer");

            migrationBuilder.DropTable(
                name: "ProfessorReviewReviewer");

            migrationBuilder.DropTable(
                name: "ProfessorSection");

            migrationBuilder.DropTable(
                name: "TimingDetails");

            migrationBuilder.DropTable(
                name: "CourseReviews");

            migrationBuilder.DropTable(
                name: "ProfessorReviews");

            migrationBuilder.DropTable(
                name: "Sections");

            migrationBuilder.DropTable(
                name: "Professors");

            migrationBuilder.DropTable(
                name: "Reviewers");

            migrationBuilder.DropTable(
                name: "CourseOfferings");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "Faculties");
        }
    }
}
