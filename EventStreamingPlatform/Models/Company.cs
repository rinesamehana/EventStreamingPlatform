namespace EventStreamingPlatform.Models
{
    public class Company
    {
        public int CompanyId { get; set; }

        public string CompanyName { get; set; }

        public DateTime CreatedDate { get; set; }

        public String CompanyDesc { get; set; }

        public ICollection<Film> Films { get; set; }
    }
}
