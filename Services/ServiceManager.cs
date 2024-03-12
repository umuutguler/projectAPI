﻿using AutoMapper;
using Entities.DataTransferObjects;
using Repositories.Contracts;
using Services.Contracts;

namespace Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IProductService> _productService;

        public ServiceManager(IRepositoryManager repositoryManager, ILoggerService _logger, IMapper mapper, IDataShaper<ProductDto> shaper)
        {
           _productService = new Lazy<IProductService>(() => new ProductManager(repositoryManager , _logger, mapper, shaper)); // artık logger ifadesi de istiyor
        }

        public IProductService ProductService => _productService.Value;
    }
}
