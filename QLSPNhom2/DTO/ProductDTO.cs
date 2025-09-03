using QLSPNhom2.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLSPNhom2.DTO
{
    public class ProductDTO
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public DateTime ManuDate { get; set; }

        public int Quantity { get; set; }

        public string Avatar { get; set; }
        public IFormFile FormFileAvatar { get; set; }
        public long IDCategory { get; set; }

        public string NameCategory{ get; set; }

}
}
