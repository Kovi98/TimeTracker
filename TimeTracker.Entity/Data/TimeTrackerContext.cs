using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Timetracker.Entity
{
    public partial class TimeTrackerContext : DbContext
    {
        public TimeTrackerContext()
        {
        }

        public TimeTrackerContext(DbContextOptions<TimeTrackerContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Job> Jobs { get; set; }
        public virtual DbSet<TaskTrack> TaskTracks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("name=TimeTracker");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Czech_CI_AS");

            modelBuilder.Entity<Job>(entity =>
            {
                entity.ToTable("Job");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<TaskTrack>(entity =>
            {
                entity.ToTable("TaskTrack");

                entity.Property(e => e.End).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(200);

                entity.Property(e => e.Start).HasColumnType("datetime");

                entity.HasOne(d => d.Job)
                    .WithMany(p => p.TaskTracks)
                    .HasForeignKey(d => d.JobId)
                    .HasConstraintName("FK_TaskTrack_Job");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
