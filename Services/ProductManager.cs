using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;
using Repositories.Contracts;
using Services.Contracts;
using static Entities.Exceptions.NotFoundException;

namespace Services
{
    public class ProductManager : IProductService
    {
        private readonly IRepositoryManager _manager;
        private readonly ILoggerService _logger;
        private readonly IMapper _mapper;
        public ProductManager(IRepositoryManager manager, ILoggerService logger, IMapper mapper)
        {
            _manager = manager;
            _logger = logger; // constructor'a ekle
            _mapper = mapper;
        }

        public Product CreateOneProduct(Product product)
        {
            
            _manager.Product.CreateOneProduct(product);
            _manager.Save();
            return product;
        }

        public void DeleteOneProduct(int id, bool trackChangers)
        {
            var entity = _manager.Product.GetOneProductById(id, trackChangers);
            if (entity is null)
            {
             //btk ve umutun kodu farklı 
                throw new ProductNotFoundException(id);
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
            var product = _manager.Product.GetOneProductById(id,trackChangers);

            // btk ve umutun kodu farklıydı değiştirdim
            if (product is null)
                throw new ProductNotFoundException(id);
            return product;
        }

        public void UpdateOneProduct(int id, ProductDtoForUpdate productDto, bool trackChangers)
        {
            //check entity
            var entity = _manager.Product.GetOneProductById(id, trackChangers);
            if(entity is null)
                throw new ProductNotFoundException(id);

            /*// check params
            if (productDto is null)
                throw new ArgumentException(nameof(productDto));*/ 
            //check params kodu umutta var btkda yok

            /*Mapping
            Request'ten gelen book nesnesini entity ile eşleştiriyoruz
            entity.Title = book.Title;
            entity.Price = book.Price;
            10 tane alan olsa tek tek eşleştireceğiz
            Burada automapper uygulayacağız
             */
            entity = _mapper.Map<Product>(productDto);

            _manager.Product.UpdateOneProduct(entity);
            _manager.Save();

        }


    }
}
