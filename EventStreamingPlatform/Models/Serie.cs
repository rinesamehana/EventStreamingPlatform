namespace EventStreamingPlatform.Models
{
    public class Serie
    {
        public int SerieId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int? CompanyId { get; set; }

        public virtual Company Company { get; set; }
        public int? LanguageId { get; set; }

        public virtual Language Language { get; set; }

        public ICollection<SerieSeason> SerieSeasons { get; set; }
        public ICollection<SerieGenre> SerieGenres { get; set; }
        public ICollection<SerieActor> SerieActors { get; set; }

        public ICollection<SerieMainActor> SerieMainActors { get; set; }
    }
}
