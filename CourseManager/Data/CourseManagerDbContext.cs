using System.Collections.Immutable;
using Data.Entities;
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
    public DbSet<SourceInfo> SourceInfo { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Faculty>()
            .HasIndex(f => f.FacultyName)
            .IsUnique();

        modelBuilder.Entity<DayOfWeekEnumEntity>()
            .HasIndex(f => f.DayOfWeek)
            .IsUnique();
    }
}