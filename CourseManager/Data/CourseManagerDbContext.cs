using System.Collections.Immutable;
using Data.Entities;
using Data.Entities.JoinTables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Data;

public class CourseManagerDbContext : DbContext
{
    public CourseManagerDbContext(DbContextOptions<CourseManagerDbContext> options) : base(options)
    {
    }

    public DbSet<Faculty> Faculties { get; set; } = null!;
    public DbSet<Course> Courses { get; set; } = null!;
    public DbSet<Professor> Professors { get; set; } = null!;
    public DbSet<ProfessorReview> ProfessorReviews { get; set; } = null!;
    public DbSet<Section> Sections { get; set; } = null!;
    public DbSet<TimingDetails> TimingDetails { get; set; } = null!;
    public DbSet<CourseOffering> CourseOfferings { get; set; } = null!;
    public DbSet<CourseReview> CourseReviews { get; set; } = null!;
    public DbSet<Reviewer> Reviewers { get; set; } = null!;
    public DbSet<JoinedReviewerCourseReview> JoinedReviewerCourseReviews { get; set; } = null!;
    public DbSet<JoinedReviewerProfessorReview> JoinedReviewerProfessorReviews { get; set; } = null!;
    public DbSet<JoinedSectionProfessor> JoinedSectionProfessors { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Professor>()
            .HasIndex(p => p.Name)
            .IsUnique();
        
        modelBuilder.Entity<Course>()
            .HasIndex(c => new {c.Number, c.FacultyId})
            .IsUnique();
        
        modelBuilder.Entity<Reviewer>()
            .HasIndex(r => r.Email)
            .IsUnique();
        
        modelBuilder.Entity<Faculty>()
            .HasIndex(f => f.Name)
            .IsUnique();

        modelBuilder.Entity<CourseOffering>()
            .HasIndex(c => new {c.Year, c.Suffix, c.CourseId, c.CalendarSource})
            .IsUnique();

        modelBuilder.Entity<Section>()
            .HasIndex(c => new { c.Number, c.CourseOfferingId })
            .IsUnique();

        modelBuilder.Entity<TimingDetails>()
            .HasIndex(c => new {c.DaysOfWeekBitmap, c.Time, c.SectionId})
            .IsUnique();
        
        // Name the CourseReview table CourseReviews
        modelBuilder.Entity<CourseReview>()
            .ToTable("CourseReviews");
        
        // name the Reviewer table Reviewers
        modelBuilder.Entity<Reviewer>()
            .ToTable("Reviewers");
        
        // Create CourseReview - Reviewer join table
        modelBuilder.Entity<JoinedReviewerCourseReview>()
            .HasKey(r => new {r.CourseReviewId, r.ReviewerId});
        
        // Configure many-to-many from CourseReview.LikedBy to Reviewer.LikedCourseReviews
        modelBuilder.Entity<JoinedReviewerCourseReview>()
            .HasOne(r => r.Reviewer)
            .WithMany(r => r.CourseReviewsLiked)
            .HasForeignKey(r => r.ReviewerId);
        
        modelBuilder.Entity<JoinedReviewerCourseReview>()
            .HasOne(r => r.CourseReview)
            .WithMany(r => r.LikedBy)
            .HasForeignKey(r => r.CourseReviewId);
        
        // Create ProfessorReview - Reviewer join table
        modelBuilder.Entity<JoinedReviewerProfessorReview>()
            .HasKey(r => new {r.ProfessorReviewId, r.ReviewerId});
        
        // Configure many-to-many from ProfessorReview.LikedBy to Reviewer.LikedProfessorReviews
        modelBuilder.Entity<JoinedReviewerProfessorReview>()
            .HasOne(r => r.Reviewer)
            .WithMany(r => r.ProfessorReviewsLiked)
            .HasForeignKey(r => r.ReviewerId);
        
        modelBuilder.Entity<JoinedReviewerProfessorReview>()
            .HasOne(r => r.ProfessorReview)
            .WithMany(r => r.LikedBy)
            .HasForeignKey(r => r.ProfessorReviewId);
        
        // Create Section - Professor join table
        modelBuilder.Entity<JoinedSectionProfessor>()
            .HasKey(r => new {r.SectionId, r.ProfessorId});
        
        // Configure many-to-many from Section.Professors to Professor.Sections
        modelBuilder.Entity<JoinedSectionProfessor>()
            .HasOne(r => r.Section)
            .WithMany(r => r.Professors)
            .HasForeignKey(r => r.SectionId);
        
        modelBuilder.Entity<JoinedSectionProfessor>()
            .HasOne(r => r.Professor)
            .WithMany(r => r.Sections)
            .HasForeignKey(r => r.ProfessorId);
    }
}