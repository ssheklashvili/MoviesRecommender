using Microsoft.EntityFrameworkCore;
using MovieRecommender.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieRecommender.Infrastructure.Contexts
{
    public class MoviesRecommenderContext : DbContext
    {
        public MoviesRecommenderContext(DbContextOptions<MoviesRecommenderContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserRate>().HasKey(sc => new { sc.UserId, sc.MovieId });
            modelBuilder.Entity<UserArtist>().HasKey(sc => new { sc.UserId, sc.ArtistId });

            modelBuilder.Entity<UserArtist>()
                .HasOne(sc => sc.User)
                .WithMany(s => s.UserArtists)
                .HasForeignKey(sc => sc.UserId);


            modelBuilder.Entity<UserArtist>()
                .HasOne(sc => sc.Artist)
                .WithMany(s => s.UserArtists)
                .HasForeignKey(sc => sc.ArtistId);

            modelBuilder.Entity<UserGenre>().HasKey(sc => new { sc.UserId, sc.GenreId });

            modelBuilder.Entity<UserGenre>()
                .HasOne(sc => sc.User)
                .WithMany(s => s.UserGenres)
                .HasForeignKey(sc => sc.UserId);


            modelBuilder.Entity<UserGenre>()
                .HasOne(sc => sc.Genre)
                .WithMany(s => s.UserGenres)
                .HasForeignKey(sc => sc.GenreId);

            modelBuilder.Entity<UserDirector>().HasKey(sc => new { sc.UserId, sc.DirectorId });

            modelBuilder.Entity<UserDirector>()
                .HasOne(sc => sc.User)
                .WithMany(s => s.UserDirectors)
                .HasForeignKey(sc => sc.UserId);


            modelBuilder.Entity<UserDirector>()
                .HasOne(sc => sc.Director)
                .WithMany(s => s.UserDirectors)
                .HasForeignKey(sc => sc.DirectorId);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<UserRate> UserRates { get; set; }
        public DbSet<UserArtist> UserArtists { get; set; }
        public DbSet<UserDirector> UserDirectors { get; set; }
        public DbSet<UserGenre> UserGenres { get; set; }
        public DbSet<Dictionary> Dictionaries { get; set; }

    }
}
