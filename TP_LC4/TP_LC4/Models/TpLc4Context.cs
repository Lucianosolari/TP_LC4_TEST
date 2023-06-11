using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TP_LC4.Models;

public partial class TpLc4Context : DbContext
{
    public TpLc4Context()
    {
    }

    public TpLc4Context(DbContextOptions<TpLc4Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Actor> Actors { get; set; }

    public virtual DbSet<Movie> Movies { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Database=TP_LC4;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Actor>(entity =>
        {
            entity.HasKey(e => e.ActorId).HasName("PK__Actor__8B2447B43311C919");

            entity.ToTable("Actor");

            entity.Property(e => e.ActorId)
                .ValueGeneratedNever()
                .HasColumnName("actor_id");
            entity.Property(e => e.ActorBirthdate)
                .HasColumnType("date")
                .HasColumnName("actor_birthdate");
            entity.Property(e => e.ActorName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("actor_name");
            entity.Property(e => e.ActorPicture)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("actor_picture");
        });

        modelBuilder.Entity<Movie>(entity =>
        {
            entity.HasKey(e => e.MovieId).HasName("PK__Movie__83CDF74930671A3E");

            entity.ToTable("Movie");

            entity.Property(e => e.MovieId)
                .ValueGeneratedNever()
                .HasColumnName("movie_id");
            entity.Property(e => e.MovieBudget)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("movie_budget");
            entity.Property(e => e.MovieDuration).HasColumnName("movie_duration");
            entity.Property(e => e.MovieGenre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("movie_genre");
            entity.Property(e => e.MovieName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("movie_name");

            entity.HasMany(d => d.Actors).WithMany(p => p.Movies)
                .UsingEntity<Dictionary<string, object>>(
                    "MovieActor",
                    r => r.HasOne<Actor>().WithMany()
                        .HasForeignKey("ActorId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__MovieActo__actor__3C69FB99"),
                    l => l.HasOne<Movie>().WithMany()
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__MovieActo__movie__3B75D760"),
                    j =>
                    {
                        j.HasKey("MovieId", "ActorId").HasName("PK__MovieAct__DB7FB3322C1C809C");
                        j.ToTable("MovieActor");
                        j.IndexerProperty<int>("MovieId").HasColumnName("movie_id");
                        j.IndexerProperty<int>("ActorId").HasColumnName("actor_id");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
