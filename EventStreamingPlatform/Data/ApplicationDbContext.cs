﻿using EventStreamingPlatform.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EventStreamingPlatform.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Film> Films { get; set; }

        public DbSet<Genre> Genres { get; set; }
        public DbSet<Recomandation> Recomandations { get; set; }
        public DbSet<FilmGenre> FilmGenres { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Genre>().ToTable("Genre");
            modelBuilder.Entity<Recomandation>().ToTable("Recomandation");
            modelBuilder.Entity<Film>().ToTable("Film");
            modelBuilder.Entity<FilmGenre>().ToTable("FilmGenres");
            modelBuilder.Entity<FilmGenre>()
                .HasKey(c => new { c.FilmId, c.GenreId });
        }

    }
}