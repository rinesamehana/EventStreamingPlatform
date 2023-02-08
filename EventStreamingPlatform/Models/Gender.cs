using System.ComponentModel.DataAnnotations;

namespace EventStreamingPlatform.Models
{
    public class Gender
    {
        public int GenderId { get; set; }
        [Required]
        public string Name { get; set; }    

        public ICollection<Actor> Actor { get; set; }
    }
}
