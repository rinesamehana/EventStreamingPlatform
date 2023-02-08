using System.ComponentModel.DataAnnotations;

namespace EventStreamingPlatform.Models
{
    public class Language
    {
        public int LanguageId { get; set; }
        [Required]
        public string LanguageName { get; set; }

        public string ISO_Code {get;set;}

        public ICollection<Film> Film { get; set; }
    }
}
