using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventStreamingPlatform.Models
{
    public class Recomandation
    {
        public int RecomandationId { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }
        [Required]
        public string Age { get; set; }

        public string Desc { get; set; }


        public int? FilmId { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

        public Film? Filmm { get; set; }
        public ICollection<Genre> Genres { get; set; }
    }
}