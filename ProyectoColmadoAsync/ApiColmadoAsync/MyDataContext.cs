using Microsoft.EntityFrameworkCore;

namespace ApiColmadoAsync
{
    public class MyDataContext:DbContext
    {
        public MyDataContext(DbContextOptions<MyDataContext> options) : base(options)
        {

        }
    }
}
