using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalProject.Areas.Admin.Models
{
    public class Item
    {
        [Key]
        public int ID { get; set; }
        
        [Required]
        public string Name { get; set; }
        [Required]
        public string Photopath { get; set; }
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }

        [Required]
        public int InStock { get; set; }

        [Required, ForeignKey("Category")]
        public int CategoryID { get; set; }

        public virtual Category Category { get; set; }

    }
}
