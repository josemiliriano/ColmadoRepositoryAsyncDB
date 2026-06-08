using System.ComponentModel.DataAnnotations.Schema;

namespace ApiColmadoAsync.Dto
{
    public class ProductDto
    {
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public decimal SalePrice { get; set; }
        public int Stock { get; set; }        
        public int CategoryId { get; set; }        
    }
}
