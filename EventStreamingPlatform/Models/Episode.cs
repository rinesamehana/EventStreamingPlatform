using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace EventStreamingPlatform.Models
{
    public class Episode
    {

        public Episode()
        {
            this.Comments = new List<Comment>();
        }
        public int EpisodeId { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string Name { get; set; }

        public string Description { get; set; }
        public string Duration { get; set; }
        [Range(0,10)]
        public string Rating { get; set; }
        public string VideoLink { get; set; }

        public string PhotoLink { get; set; }

        public DateTime RealiseDate { get; set; }
        public byte[] RowVersion { get; set; }
        
        public List<Comment> Comments { get; set; }

        [Required]
        public int? SeasonId { get; set; }

        public Season Season { get; set; }
        [Required]
        public int? SerieId { get; set; }

        public Serie Serie { get; set; }
    }
}
