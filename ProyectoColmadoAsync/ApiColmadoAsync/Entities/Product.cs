using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiColmadoAsync.Entities
{
    public class Product
    {
        [Key]
        public int IdProduct { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public decimal SalePrice { get; set; }
        public int Stock { get; set; }
        [ForeignKey("IdCategory")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public char IsDelete { get; set; } = '0';  
    }
}
