using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;
using Repositories.Contracts;
using Services.Contracts;

namespace Services
{
    public class ProductManager : IProductService
    {
        private readonly IRepositoryManager _manager;

        public ProductManager(IRepositoryManager manager)
        {
            _manager = manager;
        }

        public Product CreateOneProduct(Product product)
        {
            if (product is null)
                throw new ArgumentNullException(nameof(product));
            _manager.Product.CreateOneProduct(product);
            _manager.Save();
            return product;
        }

        public void DeleteOneProduct(int id, bool trackChangers)
        {
            var entity = _manager.Product.GetOneProductById(id, trackChangers);
            if (entity is null)
                throw new Exception($"Product with id:{id} could not found");
            _manager.Product.DeleteOneProduct(entity);
            _manager.Save();
        }

        public IEnumerable<Product> GetAllProducts(bool trackChangers)
        {
            return _manager.Product.GetAllProducts(trackChangers);
        }

        public Product GetOneProductById(int id, bool trackChangers)
        {
            return _manager.Product.GetOneProductById(id,trackChangers);
        }

        public void UpdateOneProduct(int id, Product product, bool trackChangers)
        {
            var entity = _manager.Product.GetOneProductById(id, trackChangers);

            if(entity is null)
                throw new Exception($"Product with id:{id} could not found");

            if (product is null)
                throw new ArgumentNullException(nameof(product));

            entity.Title = product.Title;
            entity.Price = product.Price;

            _manager.Product.UpdateOneProduct(entity);
            _manager.Save();

        }
    }
}
