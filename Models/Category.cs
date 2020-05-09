using Document_API.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Document_API.Models
{
    public class Category
    {

        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [MaxLength(255)]
        public string Title { get; set; }

        public virtual ICollection<Document> Documents { get; set; }
    }
}