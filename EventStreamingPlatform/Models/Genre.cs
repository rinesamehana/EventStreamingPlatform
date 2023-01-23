using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventStreamingPlatform.Models
{
    public class Genre
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Number")]
        public int GenreId { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }

  

        public int? RecomandationId { get; set; }

        public Recomandation Recomandation { get; set; }
      
        public ICollection<FilmGenre> FilmGenres { get; set; }
    }
}