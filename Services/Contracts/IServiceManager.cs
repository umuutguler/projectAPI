using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IServiceManager
    {
        IProductService ProductService { get; }
        IDepartmentService DepartmentService { get; }
        IChairService ChairService { get; }
        ITableService TableService { get; }
        IAuthenticationService AuthenticationManager { get; }
        IReservationInfoService ReservationInfoService { get; }
        IUserService UserService { get; }
        ICurrencyService CurrencyService { get; }
        IReservationsStatisticsService ReservationsStatisticsService { get; }
    }
}
