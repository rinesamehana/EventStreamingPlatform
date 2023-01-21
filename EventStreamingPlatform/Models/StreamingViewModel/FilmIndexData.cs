namespace EventStreamingPlatform.Models.StreamingViewModel
{
    public class FilmIndexData
    {
        public IEnumerable<Film> Films { get; set; }
        public IEnumerable<Genre> Genres { get; set; }
  
    }
}