namespace EventStreamingPlatform.Models
{
    public class Actor
    {
        public int ActorId { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }

        public int? GenderId { get; set; }

        public Gender Gender { get; set; }

        public ICollection<FilmActor> FilmActors { get; set; }
    }
}
