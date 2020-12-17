using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CoreWebPractice.Models
{
    public partial class WEHQAffairsContext : DbContext
    {
        public WEHQAffairsContext()
        {
        }

        public WEHQAffairsContext(DbContextOptions<WEHQAffairsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Course> Course { get; set; }
        public virtual DbSet<CourseInstructor> CourseInstructor { get; set; }
        public virtual DbSet<Department> Department { get; set; }
        public virtual DbSet<Enrollment> Enrollment { get; set; }
        public virtual DbSet<OfficeAssignment> OfficeAssignment { get; set; }
        public virtual DbSet<Person> Person { get; set; }

        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //fluent api
            modelBuilder.Entity<Course>(entity =>
            {
                entity.HasIndex(e => e.DepartmentId)
                    .HasName("IX_DepartmentID");

                entity.Property(e => e.CourseId).ValueGeneratedNever();

                entity.Property(e => e.DepartmentId).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Course)
                    .HasForeignKey(d => d.DepartmentId)
                    .HasConstraintName("FK_dbo.Course_dbo.Department_DepartmentID");
            });

            modelBuilder.Entity<CourseInstructor>(entity =>
            {
                entity.HasKey(e => new { e.CourseId, e.InstructorId })
                    .HasName("PK_dbo.CourseInstructor");

                entity.HasIndex(e => e.CourseId)
                    .HasName("IX_CourseID");

                entity.HasIndex(e => e.InstructorId)
                    .HasName("IX_InstructorID");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.CourseInstructor)
                    .HasForeignKey(d => d.CourseId)
                    .HasConstraintName("FK_dbo.CourseInstructor_dbo.Course_CourseID");

                entity.HasOne(d => d.Instructor)
                    .WithMany(p => p.CourseInstructor)
                    .HasForeignKey(d => d.InstructorId)
                    .HasConstraintName("FK_dbo.CourseInstructor_dbo.Instructor_InstructorID");
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.HasIndex(e => e.InstructorId)
                    .HasName("IX_InstructorID");

                entity.Property(e => e.RowVersion)
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.HasOne(d => d.Instructor)
                    .WithMany(p => p.Department)
                    .HasForeignKey(d => d.InstructorId)
                    .HasConstraintName("FK_dbo.Department_dbo.Instructor_InstructorID");
            });

            modelBuilder.Entity<Enrollment>(entity =>
            {
                entity.HasIndex(e => e.CourseId)
                    .HasName("IX_CourseID");

                entity.HasIndex(e => e.StudentId)
                    .HasName("IX_StudentID");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.Enrollment)
                    .HasForeignKey(d => d.CourseId)
                    .HasConstraintName("FK_dbo.Enrollment_dbo.Course_CourseID");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.Enrollment)
                    .HasForeignKey(d => d.StudentId)
                    .HasConstraintName("FK_dbo.Enrollment_dbo.Person_StudentID");
            });

            modelBuilder.Entity<OfficeAssignment>(entity =>
            {
                entity.HasKey(e => e.InstructorId)
                    .HasName("PK_dbo.OfficeAssignment");

                entity.HasIndex(e => e.InstructorId)
                    .HasName("IX_InstructorID");

                entity.Property(e => e.InstructorId).ValueGeneratedNever();

                entity.HasOne(d => d.Instructor)
                    .WithOne(p => p.OfficeAssignment)
                    .HasForeignKey<OfficeAssignment>(d => d.InstructorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo.OfficeAssignment_dbo.Instructor_InstructorID");
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.Property(e => e.Discriminator).HasDefaultValueSql("('Instructor')");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
