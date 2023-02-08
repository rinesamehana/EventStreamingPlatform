using EventStreamingPlatform.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EventStreamingPlatform.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Film> Films { get; set; }

        public DbSet<Genre> Genres { get; set; }
        public DbSet<Recomandation> Recomandations { get; set; }

        public DbSet<Company> Company { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<Actor> Actors { get; set; }

        public DbSet<Episode> Episodes { get; set; }
        public DbSet<Serie> Series { get; set; }
        public DbSet<Season> Seasons { get; set; }
        public DbSet<SerieSeason> SerieSeasons { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<CountryLanguage> CountryLanguages { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<FilmGenre> FilmGenres { get; set; }

        public DbSet<FilmActor> FilmActors { get; set; }

        public DbSet<FilmMainActor> FilmMainActors { get; set; }
        public DbSet<SerieGenre> SerieGenres { get; set; }

        public DbSet<SerieActor> SerieActors { get; set; }

        public DbSet<SerieMainActor> SerieMainActors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ApplicationUser>().ToTable("Users", "security");
            modelBuilder.Entity<IdentityRole>().ToTable("Roles", "security");
            modelBuilder.Entity<IdentityUserRole<string>>().ToTable("UserRoles", "security");
            modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims", "security");
            modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins", "security");
            modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims", "security");
            modelBuilder.Entity<IdentityUserToken<string>>().ToTable("UserTokens", "security");
            modelBuilder.Entity<Genre>().ToTable("Genre");
            modelBuilder.Entity<Company>().ToTable("Company");
            modelBuilder.Entity<Actor>().ToTable("Actor");
            modelBuilder.Entity<Recomandation>().ToTable("Recomandation");
            modelBuilder.Entity<Gender>().ToTable("Gender");
            modelBuilder.Entity<Country>().ToTable("Country");
            modelBuilder.Entity<City>().ToTable("City");
            modelBuilder.Entity<Language>().ToTable("Language");
            modelBuilder.Entity<Film>().ToTable("Film");
            modelBuilder.Entity<Episode>().ToTable("Episode");
            modelBuilder.Entity<FilmGenre>().ToTable("FilmGenres");
            modelBuilder.Entity<FilmGenre>()
                .HasKey(c => new { c.FilmId, c.GenreId });
            modelBuilder.Entity<Season>().ToTable("Season");
            modelBuilder.Entity<Serie>().ToTable("Serie");
            modelBuilder.Entity<SerieSeason>().ToTable("SerieSeasons");
            modelBuilder.Entity<SerieSeason>()
                .HasKey(c => new { c.SeasonId, c.SerieId });
           
            modelBuilder.Entity<CountryLanguage>().ToTable("CountryLanguages");
            modelBuilder.Entity<CountryLanguage>()
                .HasKey(c => new { c.CountryId, c.LanguageId });
            modelBuilder.Entity<FilmActor>().ToTable("FilmActors");
            modelBuilder.Entity<FilmActor>()
                .HasKey(c => new { c.FilmId, c.ActorId });
            modelBuilder.Entity<FilmMainActor>().ToTable("FilmMainActors");
            modelBuilder.Entity<FilmMainActor>()
                .HasKey(c => new { c.FilmId, c.ActorId });
            modelBuilder.Entity<SerieActor>().ToTable("SerieActors");
            modelBuilder.Entity<SerieActor>()
                .HasKey(c => new { c.SerieId, c.ActorId });
            modelBuilder.Entity<SerieMainActor>().ToTable("SerieMainActors");
            modelBuilder.Entity<SerieMainActor>()
                .HasKey(c => new { c.SerieId, c.ActorId });
            modelBuilder.Entity<SerieGenre>().ToTable("SerieGenres");
            modelBuilder.Entity<SerieGenre>()
                .HasKey(c => new { c.SerieId, c.GenreId });
            modelBuilder.Entity<Film>()
       .HasOne(i => i.Company)
       .WithMany(c => c.Films)
       .IsRequired(false)
       .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Episode>()
      .HasOne(i => i.Season)
      .WithMany(c => c.Episode)
      .IsRequired(false)
      .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Season>()
      .HasOne(i => i.Serie)
      .WithMany(c => c.Seasons)
      .IsRequired(false)
      .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Company>()
     .HasMany(i => i.Films)
     .WithOne(c => c.Company)
     .IsRequired(false)
     .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Episode>()
 .HasMany(i => i.Comments)
 .WithOne(c => c.Episode)
 .IsRequired(false)
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
        .IsRequired(false)
       .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Genre>()
       .HasOne(i => i.Recomandation)
       .WithMany(c => c.Genres)
       .OnDelete(DeleteBehavior.SetNull)
        .IsRequired(false);
        }

    



    }
}