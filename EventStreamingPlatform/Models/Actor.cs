namespace EventStreamingPlatform.Models
{
    public class Actor
    {
        public int ActorId { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }

        public string Biography { get; set; }

        public int? GenderId { get; set; }

        public Gender Gender { get; set; }

        public int? CountryId { get; set; }

        public Country Country { get; set; }

        public int? CityId { get; set; }

        public City City { get; set; }

        public ICollection<FilmActor> FilmActors { get; set; }

        public ICollection<FilmMainActor> FilmMainActors { get; set; }

        public ICollection<SerieActor> SerieActors { get; set; }

        public ICollection<SerieMainActor> SerieMainActors { get; set; }
    }
}
