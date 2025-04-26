using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BookMyShow.Models;

public partial class OnlineCourseDbContext : DbContext
{
    public OnlineCourseDbContext()
    {
    }

    public OnlineCourseDbContext(DbContextOptions<OnlineCourseDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<EmployeeFullDetail> EmployeeFullDetails { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
       //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=RAMESHKANDULAPC;Database=OnlineCourseDB;Trusted_Connection=True; TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.CourseId).HasName("PK__Course__C92D71A74AA5E386");

            entity.ToTable("Course");

            entity.Property(e => e.CourseName).HasMaxLength(100);
            entity.Property(e => e.Duration).HasMaxLength(50);
            entity.Property(e => e.Fee).HasColumnType("decimal(10, 2)");
        });

        modelBuilder.Entity<EmployeeFullDetail>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("EmployeeFullDetails");

            entity.Property(e => e.Address).HasMaxLength(500);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.PhoneNumber).HasMaxLength(13);
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.StudentId).HasName("PK__Student__32C52B99D9894687");

            entity.ToTable("Student");

            entity.Property(e => e.Address).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.PhoneNumber).HasMaxLength(14);

            entity.HasMany(d => d.Courses).WithMany(p => p.Students)
                .UsingEntity<Dictionary<string, object>>(
                    "StudentCourse",
                    r => r.HasOne<Course>().WithMany()
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_StudentCourse_CourseId"),
                    l => l.HasOne<Student>().WithMany()
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_StudentCourse_StudentId"),
                    j =>
                    {
                        j.HasKey("StudentId", "CourseId").HasName("PK__StudentC__5E57FC8361EB35A8");
                        j.ToTable("StudentCourse");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
