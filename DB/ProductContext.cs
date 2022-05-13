using Microsoft.EntityFrameworkCore;
using ProductHandlerKafka.Models;


namespace ProductHandlerKafka.DB
{
    public class ProductContext : DbContext
    {
        // public ProductContext(DbContextOptions<ProductContext> options) : base(options)
        // {
        // }

        public DbSet<Product> Products { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=127.0.0.1,1433;Database=productApi;User Id=sa;Password=Mau123&&&");
        }
    }
}