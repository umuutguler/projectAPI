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

        public ProductDto CreateOneProduct(ProductDtoForInsertion productDto) //ProductDtoForInsertion dan Product nesnesine bir tanım gerçekleştirmeliyiz. MappingProfile ekle
        {
            var entity = _mapper.Map<Product>(productDto); // BookDtoForInsertion tan Book a geçiş
            _manager.Product.CreateOneProduct(entity);
            _manager.Save();
            return _mapper.Map<ProductDto>(entity); //Book tan BookDto ya geçiş -  return book;
        }

        public void DeleteOneProduct(int id, bool trackChanges)
        {
            var entity = _manager.Product.GetOneProductById(id, trackChanges);
            if (entity is null)
            {
             //btk ve umutun kodu farklı 
                throw new ProductNotFoundException(id);
            }
            _manager.Product.DeleteOneProduct(entity);
            _manager.Save();
        }

        public IEnumerable<ProductDto> GetAllProducts(bool trackChanges)
        {
            var products = _manager.Product.GetAllProducts(trackChanges);
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

        public ProductDto GetOneProductById(int id, bool trackChanges)
        {
            var product = _manager.Product.GetOneProductById(id,trackChanges);

            // btk ve umutun kodu farklıydı değiştirdim
            if (product is null)
            {
                throw new ProductNotFoundException(id);
            }
            return _mapper.Map<ProductDto>(product); //product tan ProductDto ya geçiş yapıp ProductDto cinsinde veri return ettik return product;
        }

        public (ProductDtoForUpdate productDtoForUpdate, Product product) GetOneProductForPatch(int id, bool trackChanges)
        {
            var product = _manager.Product.GetOneProductById(id, trackChanges);
            if (product is null)
                throw new ProductNotFoundException(id);

            var productDtoForUpdate = _mapper.Map<ProductDtoForUpdate>(product);
            return (productDtoForUpdate, product);
        }

        public void SaveChangesForPatch(ProductDtoForUpdate productDtoForUpdate, Product product)
        {
            _mapper.Map(productDtoForUpdate, product);
            _manager.Save();
        }

        public void UpdateOneProduct(int id, ProductDtoForUpdate productDto, bool trackChanges)
        {
            //check entity
            var entity = _manager.Product.GetOneProductById(id, trackChanges);
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
