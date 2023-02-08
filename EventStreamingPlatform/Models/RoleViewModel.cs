using System.ComponentModel.DataAnnotations;

namespace EventStreamingPlatform.Models
{
    public class RoleViewModel
    {
        public string RoleId { get; set; }
        [Required]
        public string RoleName { get; set; }    

        public bool IsSelected { get; set; }
    }
}
