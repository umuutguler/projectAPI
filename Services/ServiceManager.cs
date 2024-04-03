using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Repositories.Contracts;
using Repositories.EFCore;
using Services.Contracts;

namespace Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IProductService> _productService;
        private readonly Lazy<IDepartmentService> _departmentService;
        private readonly Lazy<IAuthenticationService> _authenticationService;
        private readonly Lazy<IReservationInfoService> _reservationInfoService;
        public readonly Lazy<ITableService> _tableService;
        public readonly Lazy<IChairService> _chairService;
        public readonly Lazy<IUserService> _userService;
        public readonly Lazy<ICurrencyService> _currencyService;
        public readonly Lazy<IReservationsStatisticsService> _reservationsStatisticsService;


        public ServiceManager(IRepositoryManager repositoryManager, 
            RepositoryContext context,
            ILoggerService _logger, 
            IMapper mapper, 
            IDataShaper<ProductDto> shaper,
            UserManager<User> _userManager,// IAuthenticationService ifadesi UserManager ihtiyaç duyuyor
            IConfiguration _configuration,
            IPaymentService _paymentService)
        {
            _currencyService = new Lazy<ICurrencyService>(() => new CurrencyManager());
           _productService = new Lazy<IProductService>(() => new ProductManager(repositoryManager , _logger, mapper, shaper)); // artık logger ifadesi de istiyor
           _departmentService = new Lazy<IDepartmentService>(() => new DepartmentManager(repositoryManager));
           _authenticationService = new Lazy<IAuthenticationService>(() => new AuthenticationManager(_logger, mapper, _userManager, _configuration));
           _reservationInfoService = new Lazy<IReservationInfoService>(() => new  ReservationInfoManager(repositoryManager, context, _currencyService.Value, _paymentService));
           _tableService = new Lazy<ITableService> (() => new TableManager(repositoryManager));
           _chairService = new Lazy<IChairService>(() => new ChairManager(repositoryManager, _reservationInfoService.Value, _currencyService.Value));
           _userService = new Lazy<IUserService>(() => new UserManager(repositoryManager, _userManager));
           _reservationsStatisticsService = new Lazy<IReservationsStatisticsService>(() => new ReservationsStatisticsManager(repositoryManager, _reservationInfoService.Value));
        }

        public IProductService ProductService => _productService.Value;
        public IDepartmentService DepartmentService => _departmentService.Value;
        public IAuthenticationService AuthenticationManager => _authenticationService.Value;
        public IReservationInfoService ReservationInfoService => _reservationInfoService.Value;
        public ITableService TableService => _tableService.Value;
        public IChairService ChairService => _chairService.Value;

        public IUserService UserService => _userService.Value;

        public ICurrencyService CurrencyService => _currencyService.Value;

        public IReservationsStatisticsService ReservationsStatisticsService => _reservationsStatisticsService.Value;
    }
}
