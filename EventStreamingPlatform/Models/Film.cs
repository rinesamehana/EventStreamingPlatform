using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventStreamingPlatform.Models
{
    public class Film
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public int? CompanyId { get; set; }

        public Company Company { get; set; }
        public ICollection<FilmGenre> FilmGenres { get; set; }
  
    }
}