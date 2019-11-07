using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Lesson7.Models
{
    public partial class PersonDBContext : DbContext
    {
        public virtual DbSet<DepartmentHistory> DepartmentHistory { get; set; }
        public virtual DbSet<Departments> Departments { get; set; }
        public virtual DbSet<Employees> Employees { get; set; }
        public virtual DbSet<Jobs> Jobs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=PersonDB;Trusted_Connection=True;");
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DepartmentHistory>(entity =>
            {
                entity.Property(e => e.DepartmentName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Departments>(entity =>
            {
                entity.HasKey(e => e.DepartmentId);

                entity.Property(e => e.DepartmentId).ValueGeneratedNever();

                entity.Property(e => e.DepartmentTitle).HasColumnType("nchar(50)");
            });

            modelBuilder.Entity<Employees>(entity =>
            {
                entity.HasKey(e => e.PersonelId);

                entity.Property(e => e.PersonelId).ValueGeneratedNever();

                entity.Property(e => e.FirstName).HasColumnType("nchar(50)");

                entity.Property(e => e.LastName).HasColumnType("nchar(50)");

                entity.HasOne(d => d.Deparment)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.DeparmentId)
                    .HasConstraintName("FK__Employees__Depar__286302EC");

                entity.HasOne(d => d.Job)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.JobId)
                    .HasConstraintName("FK__Employees__JobId__25869641");
            });

            modelBuilder.Entity<Jobs>(entity =>
            {
                entity.HasKey(e => e.JobId);

                entity.Property(e => e.JobId).ValueGeneratedNever();

                entity.Property(e => e.JobTitle).HasColumnType("nchar(30)");
            });
        }
    }
}
