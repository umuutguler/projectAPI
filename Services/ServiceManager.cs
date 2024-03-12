using AutoMapper;
using Entities.DataTransferObjects;
using Repositories.Contracts;
using Services.Contracts;

namespace Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IProductService> _productService;
        private readonly Lazy<IDepartmentService> _departmentService;

        public ServiceManager(IRepositoryManager repositoryManager, ILoggerService _logger, IMapper mapper, IDataShaper<ProductDto> shaper)
        {
           _productService = new Lazy<IProductService>(() => new ProductManager(repositoryManager , _logger, mapper, shaper)); // artık logger ifadesi de istiyor
           _departmentService = new Lazy<IDepartmentService>(() => new DepartmentManager(repositoryManager)); 
        }

        public IProductService ProductService => _productService.Value;
        public IDepartmentService DepartmentService => _departmentService.Value;
    }
}
