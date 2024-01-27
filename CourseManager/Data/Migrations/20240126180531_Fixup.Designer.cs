﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Data.Migrations
{
    [DbContext(typeof(CourseManagerDbContext))]
    [Migration("20240126180531_Fixup")]
    partial class Fixup
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.26")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CourseReviewReviewer", b =>
                {
                    b.Property<int>("CourseReviewsLikedId")
                        .HasColumnType("integer");

                    b.Property<int>("LikedById")
                        .HasColumnType("integer");

                    b.HasKey("CourseReviewsLikedId", "LikedById");

                    b.HasIndex("LikedById");

                    b.ToTable("CourseReviewReviewer");
                });

            modelBuilder.Entity("Data.Entities.Course", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("AntirequisiteString")
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)");

                    b.Property<int>("BreadthCategory")
                        .HasColumnType("integer");

                    b.Property<string>("CorequisiteString")
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)");

                    b.Property<string>("Description")
                        .HasMaxLength(2000)
                        .HasColumnType("character varying(2000)");

                    b.Property<string>("ExtraInformation")
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)");

                    b.Property<int>("FacultyId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<int>("Number")
                        .HasColumnType("integer");

                    b.Property<string>("PrerequisiteString")
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)");

                    b.Property<int>("SourceInfoId")
                        .HasColumnType("integer");

                    b.Property<int>("Suffix")
                        .HasColumnType("integer");

                    b.Property<int?>("Weight")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("FacultyId");

                    b.HasIndex("SourceInfoId");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("Data.Entities.CourseReview", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateWritten")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("EasyRating")
                        .HasColumnType("integer");

                    b.Property<bool>("IsLiked")
                        .HasColumnType("boolean");

                    b.Property<int?>("ProfessorId")
                        .HasColumnType("integer");

                    b.Property<string>("ReviewText")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<int>("ReviewerId")
                        .HasColumnType("integer");

                    b.Property<int>("UsefulRating")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ProfessorId");

                    b.HasIndex("ReviewerId");

                    b.ToTable("CourseReview");
                });

            modelBuilder.Entity("Data.Entities.DayOfWeekEnumEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("DayOfWeek")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("DayOfWeek")
                        .IsUnique();

                    b.ToTable("DayOfWeekEnumEntity");
                });

            modelBuilder.Entity("Data.Entities.Faculty", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Abbreviation")
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Faculties");
                });

            modelBuilder.Entity("Data.Entities.Professor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("RmpId")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.ToTable("Professors");
                });

            modelBuilder.Entity("Data.Entities.ProfessorReview", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Clarity")
                        .HasColumnType("integer");

                    b.Property<int>("CourseId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("DateWritten")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Difficulty")
                        .HasColumnType("integer");

                    b.Property<int>("Helpful")
                        .HasColumnType("integer");

                    b.Property<int>("ProfessorId")
                        .HasColumnType("integer");

                    b.Property<int>("Quality")
                        .HasColumnType("integer");

                    b.Property<string>("ReviewText")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<int>("ReviewerId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.HasIndex("ProfessorId");

                    b.HasIndex("ReviewerId");

                    b.ToTable("ProfessorReviews");
                });

            modelBuilder.Entity("Data.Entities.Reviewer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.ToTable("Reviewer");
                });

            modelBuilder.Entity("Data.Entities.Section", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Campus")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<int>("ClassNumber")
                        .HasColumnType("integer");

                    b.Property<int>("ComponentNumber")
                        .HasColumnType("integer");

                    b.Property<int>("CourseId")
                        .HasColumnType("integer");

                    b.Property<string>("Delivery")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<int>("Number")
                        .HasColumnType("integer");

                    b.Property<List<string>>("ProfessorNames")
                        .IsRequired()
                        .HasColumnType("text[]");

                    b.Property<string>("SectionRequisiteString")
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)");

                    b.Property<string>("Status")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("TimetableRequisiteString")
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)");

                    b.Property<int>("Waitlist")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.ToTable("Sections");
                });

            modelBuilder.Entity("Data.Entities.SectionLocationAndTime", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.Property<int>("SectionId")
                        .HasColumnType("integer");

                    b.Property<string>("Time")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.HasKey("Id");

                    b.HasIndex("SectionId");

                    b.ToTable("SectionLocationAndTime");
                });

            modelBuilder.Entity("Data.Entities.SourceInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("character varying(2000)");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("character varying(2000)");

                    b.Property<string>("Year")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)");

                    b.HasKey("Id");

                    b.ToTable("SourceInfo");
                });

            modelBuilder.Entity("DayOfWeekEnumEntitySectionLocationAndTime", b =>
                {
                    b.Property<int>("DaysOfWeekId")
                        .HasColumnType("integer");

                    b.Property<int>("SectionLocationAndTimesId")
                        .HasColumnType("integer");

                    b.HasKey("DaysOfWeekId", "SectionLocationAndTimesId");

                    b.HasIndex("SectionLocationAndTimesId");

                    b.ToTable("DayOfWeekEnumEntitySectionLocationAndTime");
                });

            modelBuilder.Entity("ProfessorReviewReviewer", b =>
                {
                    b.Property<int>("LikedById")
                        .HasColumnType("integer");

                    b.Property<int>("ProfessorReviewsLikedId")
                        .HasColumnType("integer");

                    b.HasKey("LikedById", "ProfessorReviewsLikedId");

                    b.HasIndex("ProfessorReviewsLikedId");

                    b.ToTable("ProfessorReviewReviewer");
                });

            modelBuilder.Entity("ProfessorSection", b =>
                {
                    b.Property<int>("ProfessorsId")
                        .HasColumnType("integer");

                    b.Property<int>("SectionsId")
                        .HasColumnType("integer");

                    b.HasKey("ProfessorsId", "SectionsId");

                    b.HasIndex("SectionsId");

                    b.ToTable("ProfessorSection");
                });

            modelBuilder.Entity("CourseReviewReviewer", b =>
                {
                    b.HasOne("Data.Entities.CourseReview", null)
                        .WithMany()
                        .HasForeignKey("CourseReviewsLikedId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Data.Entities.Reviewer", null)
                        .WithMany()
                        .HasForeignKey("LikedById")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Data.Entities.Course", b =>
                {
                    b.HasOne("Data.Entities.Faculty", "Faculty")
                        .WithMany()
                        .HasForeignKey("FacultyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Data.Entities.SourceInfo", "Source")
                        .WithMany()
                        .HasForeignKey("SourceInfoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Faculty");

                    b.Navigation("Source");
                });

            modelBuilder.Entity("Data.Entities.CourseReview", b =>
                {
                    b.HasOne("Data.Entities.Professor", "Professor")
                        .WithMany("CourseReviews")
                        .HasForeignKey("ProfessorId");

                    b.HasOne("Data.Entities.Reviewer", "Reviewer")
                        .WithMany("CourseReviewsWritten")
                        .HasForeignKey("ReviewerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Professor");

                    b.Navigation("Reviewer");
                });

            modelBuilder.Entity("Data.Entities.ProfessorReview", b =>
                {
                    b.HasOne("Data.Entities.Course", "Course")
                        .WithMany("RelatedProfessorReviews")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Data.Entities.Professor", "Professor")
                        .WithMany("ProfessorReviews")
                        .HasForeignKey("ProfessorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Data.Entities.Reviewer", "Reviewer")
                        .WithMany("ProfessorReviewsWritten")
                        .HasForeignKey("ReviewerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("Professor");

                    b.Navigation("Reviewer");
                });

            modelBuilder.Entity("Data.Entities.Section", b =>
                {
                    b.HasOne("Data.Entities.Course", "Course")
                        .WithMany("Sections")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");
                });

            modelBuilder.Entity("Data.Entities.SectionLocationAndTime", b =>
                {
                    b.HasOne("Data.Entities.Section", "Section")
                        .WithMany("SectionLocationAndTimes")
                        .HasForeignKey("SectionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Section");
                });

            modelBuilder.Entity("DayOfWeekEnumEntitySectionLocationAndTime", b =>
                {
                    b.HasOne("Data.Entities.DayOfWeekEnumEntity", null)
                        .WithMany()
                        .HasForeignKey("DaysOfWeekId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Data.Entities.SectionLocationAndTime", null)
                        .WithMany()
                        .HasForeignKey("SectionLocationAndTimesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProfessorReviewReviewer", b =>
                {
                    b.HasOne("Data.Entities.Reviewer", null)
                        .WithMany()
                        .HasForeignKey("LikedById")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Data.Entities.ProfessorReview", null)
                        .WithMany()
                        .HasForeignKey("ProfessorReviewsLikedId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProfessorSection", b =>
                {
                    b.HasOne("Data.Entities.Professor", null)
                        .WithMany()
                        .HasForeignKey("ProfessorsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Data.Entities.Section", null)
                        .WithMany()
                        .HasForeignKey("SectionsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Data.Entities.Course", b =>
                {
                    b.Navigation("RelatedProfessorReviews");

                    b.Navigation("Sections");
                });

            modelBuilder.Entity("Data.Entities.Professor", b =>
                {
                    b.Navigation("CourseReviews");

                    b.Navigation("ProfessorReviews");
                });

            modelBuilder.Entity("Data.Entities.Reviewer", b =>
                {
                    b.Navigation("CourseReviewsWritten");

                    b.Navigation("ProfessorReviewsWritten");
                });

            modelBuilder.Entity("Data.Entities.Section", b =>
                {
                    b.Navigation("SectionLocationAndTimes");
                });
#pragma warning restore 612, 618
        }
    }
}
