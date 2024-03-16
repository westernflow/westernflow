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
    [Migration("20240316173452_AddCourseIdToCourseReview")]
    partial class AddCourseIdToCourseReview
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.26")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Data.Entities.Course", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("AntirequisiteString")
                        .HasMaxLength(3000)
                        .HasColumnType("character varying(3000)");

                    b.Property<string>("BreadthCategories")
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<string>("CorequisiteString")
                        .HasMaxLength(3000)
                        .HasColumnType("character varying(3000)");

                    b.Property<string>("Description")
                        .HasMaxLength(3000)
                        .HasColumnType("character varying(3000)");

                    b.Property<string>("ExtraInformation")
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)");

                    b.Property<int>("FacultyId")
                        .HasColumnType("integer");

                    b.Property<string>("InternalCourseId")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<int>("Number")
                        .HasColumnType("integer");

                    b.Property<string>("PrerequisiteString")
                        .HasMaxLength(3000)
                        .HasColumnType("character varying(3000)");

                    b.Property<decimal?>("Weight")
                        .HasColumnType("numeric(2,2)");

                    b.HasKey("Id");

                    b.HasIndex("FacultyId");

                    b.HasIndex("Number", "FacultyId")
                        .IsUnique();

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("Data.Entities.CourseOffering", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CalendarSource")
                        .HasColumnType("integer");

                    b.Property<int>("CourseId")
                        .HasColumnType("integer");

                    b.Property<int>("Suffix")
                        .HasColumnType("integer");

                    b.Property<int>("TermId")
                        .HasColumnType("integer");

                    b.Property<int>("Year")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.HasIndex("Year", "Suffix", "CourseId", "CalendarSource")
                        .IsUnique();

                    b.ToTable("CourseOfferings");
                });

            modelBuilder.Entity("Data.Entities.CourseReview", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CourseId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("EasyRating")
                        .HasColumnType("integer");

                    b.Property<bool>("IsLiked")
                        .HasColumnType("boolean");

                    b.Property<int?>("ProfessorId")
                        .HasColumnType("integer");

                    b.Property<string>("ReviewText")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)");

                    b.Property<int>("ReviewerId")
                        .HasColumnType("integer");

                    b.Property<int>("UsefulRating")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.HasIndex("ProfessorId");

                    b.HasIndex("ReviewerId");

                    b.ToTable("CourseReviews", (string)null);
                });

            modelBuilder.Entity("Data.Entities.Faculty", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Abbreviation")
                        .IsRequired()
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

            modelBuilder.Entity("Data.Entities.JoinTables.JoinedReviewerCourseReview", b =>
                {
                    b.Property<int>("CourseReviewId")
                        .HasColumnType("integer");

                    b.Property<int>("ReviewerId")
                        .HasColumnType("integer");

                    b.Property<int>("Id")
                        .HasColumnType("integer");

                    b.HasKey("CourseReviewId", "ReviewerId");

                    b.HasIndex("ReviewerId");

                    b.ToTable("JoinedReviewerCourseReviews");
                });

            modelBuilder.Entity("Data.Entities.JoinTables.JoinedReviewerProfessorReview", b =>
                {
                    b.Property<int>("ProfessorReviewId")
                        .HasColumnType("integer");

                    b.Property<int>("ReviewerId")
                        .HasColumnType("integer");

                    b.Property<int>("Id")
                        .HasColumnType("integer");

                    b.HasKey("ProfessorReviewId", "ReviewerId");

                    b.HasIndex("ReviewerId");

                    b.ToTable("JoinedReviewerProfessorReviews");
                });

            modelBuilder.Entity("Data.Entities.JoinTables.JoinedSectionProfessor", b =>
                {
                    b.Property<int>("SectionId")
                        .HasColumnType("integer");

                    b.Property<int>("ProfessorId")
                        .HasColumnType("integer");

                    b.Property<int>("Id")
                        .HasColumnType("integer");

                    b.HasKey("SectionId", "ProfessorId");

                    b.HasIndex("ProfessorId");

                    b.ToTable("JoinedSectionProfessors");
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

                    b.HasIndex("Name")
                        .IsUnique();

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

                    b.Property<int?>("CourseId")
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
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)");

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

                    b.Property<string>("SubjectId")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.HasKey("Id");

                    b.HasIndex("SubjectId")
                        .IsUnique();

                    b.ToTable("Reviewers", (string)null);
                });

            modelBuilder.Entity("Data.Entities.Section", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Campus")
                        .HasColumnType("integer");

                    b.Property<int>("ClassNumber")
                        .HasColumnType("integer");

                    b.Property<int>("ComponentType")
                        .HasColumnType("integer");

                    b.Property<int>("CourseOfferingId")
                        .HasColumnType("integer");

                    b.Property<int>("Delivery")
                        .HasColumnType("integer");

                    b.Property<int>("Number")
                        .HasColumnType("integer");

                    b.Property<List<string>>("ProfessorNames")
                        .IsRequired()
                        .HasColumnType("text[]");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<string>("TimetableRequisiteString")
                        .HasMaxLength(5000)
                        .HasColumnType("character varying(5000)");

                    b.Property<int>("WaitListSize")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CourseOfferingId");

                    b.HasIndex("Number", "CourseOfferingId")
                        .IsUnique();

                    b.ToTable("Sections");
                });

            modelBuilder.Entity("Data.Entities.TimingDetails", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("DaysOfWeekBitmap")
                        .IsRequired()
                        .HasMaxLength(7)
                        .HasColumnType("character varying(7)");

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

                    b.HasIndex("DaysOfWeekBitmap", "Time", "SectionId")
                        .IsUnique();

                    b.ToTable("TimingDetails");
                });

            modelBuilder.Entity("Data.Entities.Course", b =>
                {
                    b.HasOne("Data.Entities.Faculty", "Faculty")
                        .WithMany("Courses")
                        .HasForeignKey("FacultyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Faculty");
                });

            modelBuilder.Entity("Data.Entities.CourseOffering", b =>
                {
                    b.HasOne("Data.Entities.Course", "Course")
                        .WithMany("CourseOfferings")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");
                });

            modelBuilder.Entity("Data.Entities.CourseReview", b =>
                {
                    b.HasOne("Data.Entities.Course", "Course")
                        .WithMany()
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Data.Entities.Professor", "Professor")
                        .WithMany("CourseReviews")
                        .HasForeignKey("ProfessorId");

                    b.HasOne("Data.Entities.Reviewer", "Reviewer")
                        .WithMany("CourseReviewsWritten")
                        .HasForeignKey("ReviewerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("Professor");

                    b.Navigation("Reviewer");
                });

            modelBuilder.Entity("Data.Entities.JoinTables.JoinedReviewerCourseReview", b =>
                {
                    b.HasOne("Data.Entities.CourseReview", "CourseReview")
                        .WithMany("LikedBy")
                        .HasForeignKey("CourseReviewId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Data.Entities.Reviewer", "Reviewer")
                        .WithMany("CourseReviewsLiked")
                        .HasForeignKey("ReviewerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CourseReview");

                    b.Navigation("Reviewer");
                });

            modelBuilder.Entity("Data.Entities.JoinTables.JoinedReviewerProfessorReview", b =>
                {
                    b.HasOne("Data.Entities.ProfessorReview", "ProfessorReview")
                        .WithMany("LikedBy")
                        .HasForeignKey("ProfessorReviewId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Data.Entities.Reviewer", "Reviewer")
                        .WithMany("ProfessorReviewsLiked")
                        .HasForeignKey("ReviewerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ProfessorReview");

                    b.Navigation("Reviewer");
                });

            modelBuilder.Entity("Data.Entities.JoinTables.JoinedSectionProfessor", b =>
                {
                    b.HasOne("Data.Entities.Professor", "Professor")
                        .WithMany("Sections")
                        .HasForeignKey("ProfessorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Data.Entities.Section", "Section")
                        .WithMany("Professors")
                        .HasForeignKey("SectionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Professor");

                    b.Navigation("Section");
                });

            modelBuilder.Entity("Data.Entities.ProfessorReview", b =>
                {
                    b.HasOne("Data.Entities.Course", "Course")
                        .WithMany("RelatedProfessorReviews")
                        .HasForeignKey("CourseId");

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
                    b.HasOne("Data.Entities.CourseOffering", "CourseOffering")
                        .WithMany("Sections")
                        .HasForeignKey("CourseOfferingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CourseOffering");
                });

            modelBuilder.Entity("Data.Entities.TimingDetails", b =>
                {
                    b.HasOne("Data.Entities.Section", "Section")
                        .WithMany("TimingDetails")
                        .HasForeignKey("SectionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Section");
                });

            modelBuilder.Entity("Data.Entities.Course", b =>
                {
                    b.Navigation("CourseOfferings");

                    b.Navigation("RelatedProfessorReviews");
                });

            modelBuilder.Entity("Data.Entities.CourseOffering", b =>
                {
                    b.Navigation("Sections");
                });

            modelBuilder.Entity("Data.Entities.CourseReview", b =>
                {
                    b.Navigation("LikedBy");
                });

            modelBuilder.Entity("Data.Entities.Faculty", b =>
                {
                    b.Navigation("Courses");
                });

            modelBuilder.Entity("Data.Entities.Professor", b =>
                {
                    b.Navigation("CourseReviews");

                    b.Navigation("ProfessorReviews");

                    b.Navigation("Sections");
                });

            modelBuilder.Entity("Data.Entities.ProfessorReview", b =>
                {
                    b.Navigation("LikedBy");
                });

            modelBuilder.Entity("Data.Entities.Reviewer", b =>
                {
                    b.Navigation("CourseReviewsLiked");

                    b.Navigation("CourseReviewsWritten");

                    b.Navigation("ProfessorReviewsLiked");

                    b.Navigation("ProfessorReviewsWritten");
                });

            modelBuilder.Entity("Data.Entities.Section", b =>
                {
                    b.Navigation("Professors");

                    b.Navigation("TimingDetails");
                });
#pragma warning restore 612, 618
        }
    }
}
