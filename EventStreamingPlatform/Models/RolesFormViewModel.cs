﻿using System.ComponentModel.DataAnnotations;

namespace EventStreamingPlatform.Models
{

    public class RoleFormViewModel
    {
        [Required, StringLength(256)]
        public string Name { get; set; }
    }

}
