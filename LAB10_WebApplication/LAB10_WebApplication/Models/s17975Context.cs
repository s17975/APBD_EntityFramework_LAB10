using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace LAB10_WebApplication.Models
{
    public partial class s17975Context : DbContext
    {
        public s17975Context()
        {
        }

        public s17975Context(DbContextOptions<s17975Context> options)
            : base(options)
        {
        }

        public virtual DbSet<BooksDb> BooksDb { get; set; }
        public virtual DbSet<Enrollment> Enrollment { get; set; }
        public virtual DbSet<PromocjeGazetkowe> PromocjeGazetkowe { get; set; }
        public virtual DbSet<PromocjeInternetowe> PromocjeInternetowe { get; set; }
        public virtual DbSet<RefreshTokens> RefreshTokens { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<Student> Student { get; set; }
        public virtual DbSet<Studies> Studies { get; set; }

        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                
                optionsBuilder.UseSqlServer("Data Source=db-mssql;Initial Catalog=s17975;Integrated Security=True");
            }
        }
        
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BooksDb>(entity =>
            {
                entity.HasKey(e => e.IdBook)
                    .HasName("BooksDB_PK");

                entity.ToTable("BooksDB");

                entity.Property(e => e.IdBook).ValueGeneratedNever();

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Enrollment>(entity =>
            {
                entity.HasKey(e => e.IdEnrollment)
                    .HasName("Enrollment_pk");

                entity.Property(e => e.IdEnrollment).ValueGeneratedNever();

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.HasOne(d => d.IdStudyNavigation)
                    .WithMany(p => p.Enrollment)
                    .HasForeignKey(d => d.IdStudy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Enrollment_Studies");
            });

            modelBuilder.Entity<PromocjeGazetkowe>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.DataDo)
                    .HasColumnName("dataDO")
                    .HasColumnType("date");

                entity.Property(e => e.DataOd)
                    .HasColumnName("dataOD")
                    .HasColumnType("date");

                entity.Property(e => e.FormatGazetki)
                    .IsRequired()
                    .HasColumnName("formatGazetki")
                    .HasMaxLength(100);

                entity.Property(e => e.SiecHandlowa)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<PromocjeInternetowe>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.DataDo)
                    .HasColumnName("dataDO")
                    .HasColumnType("date");

                entity.Property(e => e.DataOd)
                    .HasColumnName("dataOD")
                    .HasColumnType("date");

                entity.Property(e => e.SiecHandlowa)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Urlgazetki)
                    .IsRequired()
                    .HasColumnName("URLGazetki")
                    .HasMaxLength(150);
            });

            modelBuilder.Entity<RefreshTokens>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.IndexNumber)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Token)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Roles>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.DbRole)
                    .IsRequired()
                    .HasColumnName("DB_Role")
                    .HasMaxLength(100);

                entity.Property(e => e.IndexNumber)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Salt)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasKey(e => e.IndexNumber)
                    .HasName("Student_pk");

                entity.Property(e => e.IndexNumber).HasMaxLength(100);

                entity.Property(e => e.BirthDate).HasColumnType("date");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Salt)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.IdEnrollmentNavigation)
                    .WithMany(p => p.Student)
                    .HasForeignKey(d => d.IdEnrollment)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Student_Enrollment");
            });

            modelBuilder.Entity<Studies>(entity =>
            {
                entity.HasKey(e => e.IdStudy)
                    .HasName("Studies_pk");

                entity.Property(e => e.IdStudy).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
