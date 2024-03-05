using Microsoft.EntityFrameworkCore;
using WebApi.Models;
using WebApi.Repositories.config;

namespace WebApi.Repositories
{
    public class RepositoriesContext : DbContext
    {
        public RepositoriesContext(DbContextOptions options) :
            base(options)
        { }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductConfig()); // Model Oluşturulurken konfigürasyon ifadesi dikkate alınacak
        }
    }
}