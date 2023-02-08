using System.ComponentModel.DataAnnotations;

namespace EventStreamingPlatform.Models
{
    public class Country
    {
        public int CountryId { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string Name { get; set; }
        [Required]
        public string Abbreviation { get; set; }

        public string Popullation { get; set; }

        public string ISO_Code { get; set; }

        public ICollection<CountryLanguage> CountryLanguages { get; set; }
        public ICollection<City> Cities { get; set; }

        public ICollection<Actor> Actors { get; set; }

        public ICollection<Company> Companies { get; set; }

    }
}
