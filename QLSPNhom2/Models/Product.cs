using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLSPNhom2.Models
{
    [Table("Product")]
    public class Product
    {
        [Key]
        public long ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public DateTime ManuDate { get; set; }

        public int Quantity { get; set; }

        public string Avatar { get; set; }
        public long IDCategory { get; set; }
        [ForeignKey("IDCategory")]
        public Category Category { get; set; }
    }
}
