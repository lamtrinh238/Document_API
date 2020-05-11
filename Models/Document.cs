using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace Document_API.Models
{
    public class Document
    {
        [Key]
        public int ID { get; set; }

        [ForeignKey("Category")]
        [Required(ErrorMessage = "This field is required")]
        public int CategoryID { get; set; }
        public virtual Category Category { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string Title { get; set; }

        [ForeignKey("Owner")]
        public int OwnerID { get; set; }
        public virtual User Owner { get; set; }

        public byte[] Cover { get; set; }

        [MaxLength(255)]
        public string Description { get; set; }

        [MaxLength(255)]
        public string CoverFileExtension { get; set; }


        [MaxLength(255)]
        public string ContentFileExtension { get; set; }
    }
}