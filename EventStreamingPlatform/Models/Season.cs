namespace EventStreamingPlatform.Models
{
    public class Season
    {
        public int SeasonId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int? SerieId { get; set; }

        public Serie Serie { get; set; }

        //public ICollection<SerieSeason> SerieSeasons { get; set; }
        public ICollection<Episode> Episode { get; set; }
    }
}
