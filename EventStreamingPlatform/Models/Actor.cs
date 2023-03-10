using System.ComponentModel.DataAnnotations;

namespace EventStreamingPlatform.Models
{
    public class Actor
    {
        public int ActorId { get; set; }
        [Required]
        [StringLength(50, MinimumLength =2)]
        public string Name { get; set; }
        [Required]
        public string LastName { get; set; }

        public int Age { get; set; }

        public string Biography { get; set; }

        public string PhotoLink { get; set; }
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
