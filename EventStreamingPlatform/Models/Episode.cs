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

        public string Name { get; set; }

        public string Description { get; set; }
        public byte[] RowVersion { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public List<Comment> Comments { get; set; }

        
        public int? SeasonId { get; set; }

        public Season Season { get; set; }

        public int? SerieId { get; set; }

        public Serie Serie { get; set; }
    }
}
