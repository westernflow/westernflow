using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data;

public class CourseManagerDbContext : ContextBase
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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Professor>()
            .HasIndex(p => p.Name)
            .IsUnique();

        modelBuilder.Entity<Course>()
            .HasIndex(c => new { c.Number, c.FacultyId })
            .IsUnique();

        modelBuilder.Entity<Faculty>()
            .HasIndex(f => f.Name)
            .IsUnique();
        
        modelBuilder.Entity<Faculty>() 
            .HasIndex(f => f.Abbreviation)
            .IsUnique();

        modelBuilder.Entity<CourseOffering>()
            .HasIndex(c => new { c.Year, c.Suffix, c.CourseId, c.CalendarSource })
            .IsUnique();

        modelBuilder.Entity<Section>()
            .HasIndex(c => new { c.Number, c.CourseOfferingId, c.ClassNumber })
            .IsUnique();

        modelBuilder.Entity<Professor>()
            .HasIndex(p => new { p.Email })
            .IsUnique();
        
        modelBuilder.Entity<Professor>()
            .HasIndex(p => new { p.UwoId })
            .IsUnique();

        modelBuilder.Entity<TimingDetails>()
            .HasIndex(c => new { c.DaysOfWeekBitmap, c.Time, c.SectionId })
            .IsUnique();

        modelBuilder.Entity<Reviewer>()
            .HasIndex(c => new { c.SubjectId })
            .IsUnique();

        modelBuilder.Entity<CourseReview>()
            .ToTable("CourseReviews");

        modelBuilder.Entity<Reviewer>()
            .ToTable("Reviewers");
    }
}