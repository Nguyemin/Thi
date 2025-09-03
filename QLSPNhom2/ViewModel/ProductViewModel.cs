using QLSPNhom2.DTO;

namespace QLSPNhom2.ViewModel
{
    public class ProductViewModel : Paing
    {
        public List<CategoryDTO> Categories { get; set; }

        public List<ProductDTO> Products { get; set; }

        public ProductDTO Request { get; set; }

        public ProductDTO Respone { get; set; }
    }
}
