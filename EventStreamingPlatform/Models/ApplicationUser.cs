using Microsoft.AspNetCore.Identity;
using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;

namespace EventStreamingPlatform.Models
{
    public class ApplicationUser : IdentityUser
    {
        [System.ComponentModel.DataAnnotations.Required, MaxLength(100)]
        public string FirstName { get; set; }
        [System.ComponentModel.DataAnnotations.Required, MaxLength(100)]
        public string LastName { get; set; }

        public byte[] ProfilePicture { get; set; }
    }
}
