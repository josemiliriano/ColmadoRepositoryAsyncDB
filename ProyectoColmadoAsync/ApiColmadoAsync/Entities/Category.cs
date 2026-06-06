using System.ComponentModel.DataAnnotations;

namespace ApiColmadoAsync.Entities
{
    public class Category
    {
        [Key]
        public int IdCategory { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public char Isdelete { get; set; } = '0';
    }
}
