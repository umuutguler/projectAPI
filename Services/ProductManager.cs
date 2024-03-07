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
        private readonly ILoggerService _logger;
        public ProductManager(IRepositoryManager manager, ILoggerService logger)
        {
            _manager = manager;
            _logger = logger; // constructor'a ekle
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
            {
                string message = $"The Product with id:{id} could not found";
                _logger.LogInf(message); // Varlık yok ise bir log düşsün
                throw new Exception(message);
            }
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
