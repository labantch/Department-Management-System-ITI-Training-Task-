using System;
using System.Collections.Generic;
using System.Text;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.DBContext
{
    public class ITIDbContext: DbContext
    {
        public ITIDbContext()
        {
        }

        public ITIDbContext(DbContextOptions<ITIDbContext> options) : base(options)
        {
        }

        override protected void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
               
                optionsBuilder.UseSqlServer("Server=.;Database=ITI;Trusted_Connection=True; TrustServerCertificate=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Enrollment>()
                .HasKey(e => new { e.TraineeId, e.CourseId });

            modelBuilder.Entity<Course>()
         .HasOne(c => c.Instructor)
         .WithMany(i => i.Courses)
         .HasForeignKey(c => c.InstructorId)
         .IsRequired(false)
         .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Department>()
        .HasOne(d => d.Manager)
        .WithMany() 
        .HasForeignKey(d => d.ManagerId)
        .OnDelete(DeleteBehavior.NoAction);
        }
        

        public DbSet<Course> Courses { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Trainee> Trainees { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }



    }
}
