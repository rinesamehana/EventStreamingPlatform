using System.ComponentModel.DataAnnotations;

namespace EventStreamingPlatform.Models
{
    public class City
    {
        public int CityId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int? CountryId { get; set; }

        public Country Country { get; set; }

        public ICollection<Actor> Actors { get; set; }

        public ICollection<Company> Companies { get; set; }
    }
}
