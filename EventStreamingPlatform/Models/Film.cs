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
        [ForeignKey("CompanyId")]
        public int? CompanyId { get; set; }
     
        public virtual  Company Company { get; set; }
        public int? LanguageId { get; set; }

        public virtual  Language Language { get; set; }
        public List<FilmGenre> FilmGenres { get; set; }=new List<FilmGenre>();
        public ICollection<FilmActor> FilmActors { get; set; }

        public ICollection<FilmMainActor> FilmMainActors { get; set; }
    }
}