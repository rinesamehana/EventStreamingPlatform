namespace EventStreamingPlatform.Models
{
    public class SerieSeason
    {
        public int SerieId { get; set; }

        public int SeasonId { get; set; }
        public Season Season { get; set; }

        public Serie Serie { get; set; }
    }
}
