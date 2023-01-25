namespace EventStreamingPlatform.Models
{
    public class Serie
    {
        public int SerieId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public ICollection<SerieSeason> SerieSeasons { get; set; }
    }
}
