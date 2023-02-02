namespace EventStreamingPlatform.Models
{
    public class SerieMainActor
    {
        public int SerieId { get; set; }

        public int ActorId { get; set; }
        public Serie Serie { get; set; }

        public Actor Actor { get; set; }
    }
}
