using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventStreamingPlatform.Models
{
    public class Film
    {
        public int ID { get; set; }
        [Required]
        public string Title { get; set; }
      
        public string Description { get; set; }
        public string Duration { get; set; }
        [Range(0,10)]
        public string Rating { get; set; }
        public string VideoLink { get; set; }

        public string PhotoLink { get; set; }

        public DateTime RealiseDate { get; set; }
        public string Director { get; set; }

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