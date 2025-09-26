using System;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Models;

namespace StudentManagement.Data;

public class SchoolContext : DbContext
{
    public SchoolContext(DbContextOptions<SchoolContext> options)
        : base(options)
    {
    }

    public DbSet<Student> Students => Set<Student>();
    public DbSet<Course> Courses => Set<Course>();
    public DbSet<Enrollment> Enrollments=> Set<Enrollment>();
    public DbSet<Instructor> Instructors => Set<Instructor>();
    public DbSet<Department> Departments => Set<Department>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Student>(e =>
        {
            e.Property(p => p.FirstName).HasMaxLength(50);
            e.Property(p => p.LastName).HasMaxLength(50);
            e.Property(p => p.Email).HasMaxLength(255);
            e.HasIndex(p => p.Email).IsUnique();
        });

        modelBuilder.Entity<Course>(e =>
        {
            e.Property(p => p.Title).HasMaxLength(100);
            e.Property(p => p.Credits).HasColumnType("decimal(5, 2)");
        });

        modelBuilder.Entity<Enrollment>(e =>
        {
            e.HasOne(p => p.Student).WithMany(s => s.Enrollments).HasForeignKey(p => p.StudentId);
            e.HasOne(p => p.Course).WithMany(c => c.Enrollments).HasForeignKey(p => p.CourseId);
        });

        modelBuilder.Entity<Instructor>(e =>
        {
            e.Property(p => p.FirstName).HasMaxLength(50);
            e.Property(p => p.LastName).HasMaxLength(50);
            e.Property(p => p.Email).HasMaxLength(255);
            e.HasIndex(p => p.Email).IsUnique();
        });

        modelBuilder.Entity<Course>(e =>
        {
            e.Property(p => p.Title).HasMaxLength(100);
            e.HasOne(p => p.Instructor)
                .WithMany(i => i.Courses)
                .HasForeignKey(p => p.InstructorId);
        });

        modelBuilder.Entity<Department>(e =>
        {
            e.Property(p => p.Name).HasMaxLength(150);
            e.HasOne(p => p.DepartmentHead)
                .WithOne(i => i.DepartmentHeadOf)
                .HasForeignKey<Department>(p => p.DepartmentHeadId);

            e.HasIndex(p => p.DepartmentHeadId)
                .IsUnique()
                .HasFilter("[DepartmentHeadId] IS NOT NULL");
        });
    }
}