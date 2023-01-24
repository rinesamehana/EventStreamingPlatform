namespace EventStreamingPlatform.Models
{
    public class Country
    {
        public int CountryId { get; set; }

        public string Name { get; set; }

        public string Abbreviation { get; set; }

        public string Popullation { get; set; }

        public string ISO_Code { get; set; }

        public ICollection<CountryLanguage> CountryLanguages { get; set; }
        public ICollection<City> Cities { get; set; }

    }
}
