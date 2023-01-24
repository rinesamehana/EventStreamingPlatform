using EventStreamingPlatform.Models;
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

        public DbSet<Company> Companies { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<Actor> Actors { get; set; }

        public DbSet<Country> Countries { get; set; }
        public DbSet<CountryLanguage> CountryLanguages { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<FilmGenre> FilmGenres { get; set; }

        public DbSet<FilmActor> FilmActors { get; set; }

        public DbSet<FilmMainActor> FilmMainActors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Genre>().ToTable("Genre");
            modelBuilder.Entity<Company>().ToTable("Company");
            modelBuilder.Entity<Actor>().ToTable("Actor");
            modelBuilder.Entity<Recomandation>().ToTable("Recomandation");
            modelBuilder.Entity<Gender>().ToTable("Gender");
            modelBuilder.Entity<Country>().ToTable("Country");
            modelBuilder.Entity<City>().ToTable("City");
            modelBuilder.Entity<Language>().ToTable("Language");
            modelBuilder.Entity<Film>().ToTable("Film");
            modelBuilder.Entity<FilmGenre>().ToTable("FilmGenres");
            modelBuilder.Entity<FilmGenre>()
                .HasKey(c => new { c.FilmId, c.GenreId });
            modelBuilder.Entity<CountryLanguage>().ToTable("CountryLanguages");
            modelBuilder.Entity<CountryLanguage>()
                .HasKey(c => new { c.CountryId, c.LanguageId });
            modelBuilder.Entity<FilmActor>().ToTable("FilmActors");
            modelBuilder.Entity<FilmActor>()
                .HasKey(c => new { c.FilmId, c.ActorId });
            modelBuilder.Entity<FilmMainActor>().ToTable("FilmMainActors");
            modelBuilder.Entity<FilmMainActor>()
                .HasKey(c => new { c.FilmId, c.ActorId });

            modelBuilder.Entity<Film>()
       .HasOne(i => i.Company)
       .WithMany(c => c.Films)
       .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<City>()
  .HasOne(i => i.Country)
  .WithMany(c => c.Cities)
  .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Actor>()
     .HasOne(i => i.Gender)
     .WithMany(c => c.Actor)
     .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Actor>()
 .HasOne(i => i.Country)
 .WithMany(c => c.Actors)
 .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Actor>()
.HasOne(i => i.City)
.WithMany(c => c.Actors)
.OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Film>()
       .HasOne(i => i.Language)
       .WithMany(c => c.Film)
       .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Genre>()
       .HasOne(i => i.Recomandation)
       .WithMany(c => c.Genres)
       .OnDelete(DeleteBehavior.SetNull);
        }

    



    }
}