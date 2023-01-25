namespace EventStreamingPlatform.Models.StreamingViewModel
{
    public class SeasonIndexData
    {
        public IEnumerable<Episode> Episodes { get; set; }
        public IEnumerable<Season> Seasons { get; set; }
    }
}
