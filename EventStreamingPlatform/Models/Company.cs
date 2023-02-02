namespace EventStreamingPlatform.Models
{
    public class Company
    {
        public int CompanyId { get; set; }

        public string CompanyName { get; set; }

        public DateTime CreatedDate { get; set; }

        public String CompanyDesc { get; set; }

        public int? CountryId { get; set; }

        public Country Country { get; set; }

        public int? CityId { get; set; }

        public City City { get; set; }

        public ICollection<Film> Films { get; set; }
    }
}
