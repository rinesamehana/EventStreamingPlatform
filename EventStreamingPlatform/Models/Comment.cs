using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EventStreamingPlatform.Models
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }

        [Required]
        [StringLength(150, MinimumLength = 2)]
        public string Description { get; set; }

        [Description("Created Date")]
        public DateTime CreatedDate { get; set; }

        [Description("Last Updated Date")]
        public DateTime LastUpdatedDate { get; set; }

        [ForeignKey("Episode")]
        public int? EpisodeId { get; set; }
        public Episode Episode { get; set; }

        [ForeignKey("Author")]
        public string AuthorId { get; set; }
        public ApplicationUser Author { get; set; }
    }
}
