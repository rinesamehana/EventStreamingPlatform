namespace EventStreamingPlatform.Models.StreamingViewModel
{
    public class SeasonIndexData
    {
        public IEnumerable<Serie> Series { get; set; }
        public IEnumerable<Episode> Episodes { get; set; }
        public IEnumerable<Season> Seasons { get; set; }
    }
}
