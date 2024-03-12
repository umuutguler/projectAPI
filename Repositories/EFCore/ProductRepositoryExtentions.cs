using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EFCore
{
    public static class ProductRepositoryExtentions
    {
        public static IQueryable<Product> FilterProducts(this IQueryable<Product> products,
            uint minPrice, uint maxPrice) =>
            products.Where(product =>
            (product.Price >= minPrice) &&
            (product.Price <= maxPrice));
    }
}
