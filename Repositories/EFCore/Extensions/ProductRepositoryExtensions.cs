using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;

namespace Repositories.EFCore.Extensions
{
    public static class ProductRepositoryExtensions
    {
        public static IQueryable<Product> FilterProducts(this IQueryable<Product> products,
            uint minPrice, uint maxPrice) =>
            products.Where(product =>
            product.Price >= minPrice &&
            product.Price <= maxPrice);


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

        public static IQueryable<Product> Sort(this IQueryable<Product> products,
            string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return products.OrderBy(p => p.Id);

            
            var orderQuery = OrderQueryBuilder.CreateOrderQuery<Product>(orderByQueryString); // OrderyQueryBuilder sorting metodları

            if (orderQuery is null)
                return products.OrderBy(p => p.Id); // orderQuery null ise books u id ye göre sıralayıp gönder

            return products.OrderBy(orderQuery);
        }

    }
}
