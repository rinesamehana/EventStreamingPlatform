namespace EventStreamingPlatform.Models
{
    public class Season
    {
        public int SeasonId { get; set; }

        public string Name { get; set; }

        public ICollection<SerieSeason> SerieSeasons { get; set; }
        public ICollection<Episode> Episode { get; set; }
    }
}
