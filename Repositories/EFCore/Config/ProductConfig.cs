using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Entities.Models;

namespace Repositories.EFCore.Config
{
    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasData(
                new Product { Id = 1, Price = 100, Title = "Product 1" },
                new Product { Id = 2, Price = 75, Title = "Product 2" },
                new Product { Id = 3, Price = 200, Title = "Product 3" }
                );
        }
    }
}
s