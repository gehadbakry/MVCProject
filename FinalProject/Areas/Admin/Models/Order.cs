using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalProject.Areas.Admin.Models
{
    public class Order
    {
        [Key]
        public int ID { get; set; }

        [Required, DataType(DataType.DateTime)]
        public DateTime Date { get; set; }

        [Required]
        public decimal TotalCost { get; set; }

        [Required, ForeignKey("Customer")]
        public int CustomerID { get; set; }

        public virtual Customer Customer { get; set; }

        ICollection<Item> Items { get; set; } = new HashSet<Item>();
    }
}
