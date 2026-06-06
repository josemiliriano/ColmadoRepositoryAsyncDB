using ApiColmadoAsync.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApiColmadoAsync
{
    public class MyDataContext:DbContext
    {
        public MyDataContext(DbContextOptions<MyDataContext> options) : base(options)
        {

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
