﻿using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Exceptions;
using Entities.Models;
using Entities.RequestFeatures;
using Repositories.Contracts;
using Services.Contracts;
using static Entities.Exceptions.NotFoundException;

namespace Services
{
    public class ProductManager : IProductService
    {
        /* Manager e ihtiyaç duyuyorum. BookRepository e değil.
         * Book Repository e enjekte etmeyeceğiz
         * Manager ı enjekte edeceğiz. Her şey onun üzerinde dönecek*/
        private readonly IRepositoryManager _manager;
        private readonly ILoggerService _logger;
        private readonly IMapper _mapper;
        private readonly IDataShaper<ProductDto> _shaper;

        public ProductManager(IRepositoryManager manager, ILoggerService logger, IMapper mapper, IDataShaper<ProductDto> shaper)
        {
            _manager = manager;
            _logger = logger; // constructor'a ekle
            _mapper = mapper;
            _shaper = shaper;
        }

        public async Task <ProductDto> CreateOneProductAsync(ProductDtoForInsertion productDto) //ProductDtoForInsertion dan Product nesnesine bir tanım gerçekleştirmeliyiz. MappingProfile ekle
        {
            var entity = _mapper.Map<Product>(productDto); // BookDtoForInsertion tan Book a geçiş
            _manager.Product.CreateOneProduct(entity);
            await _manager.SaveAsync();
            return _mapper.Map<ProductDto>(entity); //Book tan BookDto ya geçiş -  return book;
        }

        public async Task DeleteOneProductAsync(int id, bool trackChanges)
        {
            var entity = await GetOneBookByIdAndCheckExists(id, trackChanges);
           
            _manager.Product.DeleteOneProduct(entity);
            await _manager.SaveAsync();
        }

        public async Task<(IEnumerable<ExpandoObject> products, MetaData metaData)> GetAllProductsAsync(ProductParameters productParameters,bool trackChanges)
        //BookParameters'da PageSize, PageNumber vardı ama artık minNumber ve maxNumber değerleri de var.
        {
            if (!productParameters.ValidPriceRange) // valid bir ifade gelmediyse hata kodu
                throw new PriceOutofRangeBadRequestException();


            var productsWithMetaData = await _manager.Product.GetAllProductsAsync(productParameters, trackChanges);
            var productsDto = _mapper.Map<IEnumerable<ProductDto>>(productsWithMetaData);
            var shapedData = _shaper.ShapeData(productsDto, productParameters.Fields);
            // booksDto-> liste | bookParameters.Fields -> ihtiyaç duyulan alanlar
            return (products: shapedData, metaData: productsWithMetaData.MetaData);
        }

        public async Task<ProductDto> GetOneProductByIdAsync(int id, bool trackChanges)
        {
            var product = await GetOneBookByIdAndCheckExists(id, trackChanges);
            return _mapper.Map<ProductDto>(product); //product tan ProductDto ya geçiş yapıp ProductDto cinsinde veri return ettik return product;
        }

        public async Task<(ProductDtoForUpdate productDtoForUpdate, Product product)> GetOneProductForPatchAsync(int id, bool trackChanges)
        {
            var product = await GetOneBookByIdAndCheckExists(id, trackChanges);
           
            var productDtoForUpdate = _mapper.Map<ProductDtoForUpdate>(product);
            return (productDtoForUpdate, product);
        }

        public async Task SaveChangesForPatchAsync(ProductDtoForUpdate productDtoForUpdate, Product product)
        {
            _mapper.Map(productDtoForUpdate, product);
            await _manager.SaveAsync();
        }

        public async Task UpdateOneProductAsync(int id, ProductDtoForUpdate productDto, bool trackChanges)
        {
            //check entity
            var entity = await GetOneBookByIdAndCheckExists(id, trackChanges);
            
            entity = _mapper.Map<Product>(productDto);

            _manager.Product.Update(entity);
            await _manager.SaveAsync();

        }

        //Action Filters la alakası yok.
        private async Task<Product> GetOneBookByIdAndCheckExists(int id, bool trackChanges)
        {
            //check entity
            var entity = await _manager.Product.GetOneProductByIdAsync(id, trackChanges);
            if (entity is null)
            {
                throw new ProductNotFoundException(id);
            }
            return entity;
        }
    }
}
