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


        public static IQueryable<Product> Search(this IQueryable<Product> products,
            string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm)) //Boşlukları varsa
                return products;

            var lowerCaseTerm = searchTerm.Trim().ToLower(); // trim, boşlukaları at - ToLower, küçük harflere çevir
            return products
                .Where(b => b.Title
                .ToLower()
                .Contains(searchTerm));
        }
    }
}
