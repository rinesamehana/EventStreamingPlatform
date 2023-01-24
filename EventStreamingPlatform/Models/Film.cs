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
        public int? LanguageId { get; set; }

        public Language Language { get; set; }
        public ICollection<FilmGenre> FilmGenres { get; set; }
        public ICollection<FilmActor> FilmActors { get; set; }

        public ICollection<FilmMainActor> FilmMainActors { get; set; }
    }
}