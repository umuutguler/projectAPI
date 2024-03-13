using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Repositories.Contracts;
using Services.Contracts;

namespace Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IProductService> _productService;
        private readonly Lazy<IDepartmentService> _departmentService;
        private readonly Lazy<IAuthenticationService> _authenticationService;

        public ServiceManager(IRepositoryManager repositoryManager, 
            ILoggerService _logger, 
            IMapper mapper, 
            IDataShaper<ProductDto> shaper,
            UserManager<User> _userManager,// IAuthenticationService ifadesi UserManager ihtiyaç duyuyor
            IConfiguration _configuration)
        {
           _productService = new Lazy<IProductService>(() => new ProductManager(repositoryManager , _logger, mapper, shaper)); // artık logger ifadesi de istiyor
           _departmentService = new Lazy<IDepartmentService>(() => new DepartmentManager(repositoryManager));
           _authenticationService = new Lazy<IAuthenticationService>(() => new AuthenticationManager(_logger, mapper, _userManager, _configuration));
        }

        public IProductService ProductService => _productService.Value;
        public IDepartmentService DepartmentService => _departmentService.Value;
        public IAuthenticationService AuthenticationManager => _authenticationService.Value;
    }
}
