﻿namespace EventStreamingPlatform.Models.StreamingViewModel
{
    public class SerieIndexData
    {
        public IEnumerable<Serie> Seriess { get; set; }
        public IEnumerable<Season> Seasons { get; set; }

        public IEnumerable<Episode> Episodes { get; set; }
    }
}