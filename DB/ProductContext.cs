using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("myConnection");

            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}