using Document_API.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Document_API.Models
{
    public class User
    {

        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [MinLength(5)]
        [MaxLength(255)]
        public string Username { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [MinLength(8)]
        [MaxLength(255)]
        public string Password { get; set; }

        [MaxLength(255)]
        public string FirstName { get; set; }

        [MaxLength(255)]
        public string LastName { get; set; }

        public EnumRole Role { get; set; }
    }
}