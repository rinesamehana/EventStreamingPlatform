namespace EventStreamingPlatform.Models
{
    public class Episode
    {
        public int EpisodeId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public byte[] RowVersion { get; set; }
        public int? SeasonId { get; set; }

        public Season Season { get; set; }

        public int? SerieId { get; set; }

        public Serie Serie { get; set; }
    }
}
