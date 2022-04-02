using System.ComponentModel.DataAnnotations;

namespace FinalProject.Areas.Admin.Models
{
    public class Category
    {
        [Key]
        public int ID { get; set; }

        [Required, MaxLength(30)]
        public string Name { get; set; }

        ICollection<Item> Items { get; set; }  = new HashSet<Item>();
    }
}
