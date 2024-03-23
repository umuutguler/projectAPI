using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;

namespace Services.Contracts
{
    public interface IReservationsStatisticsService
    {
        Task<IEnumerable<ReservationInfo>> GetReservationsByTimeRange(
            DateTime startDate,
            DateTime endDate,
            bool trackChanges);

        Task<(int TotalReservationCount, IEnumerable<ReservationInfo>)> MostReservedDepartmentAsync(
            DateTime startDate,
            DateTime endDate,
            bool trackChanges);

        Task<(int TotalReservationCount, IEnumerable<ReservationInfo>)> MostReservedUserAsync(
            DateTime startDate,
            DateTime endDate,
            bool trackChanges);

         Task<(int TotalCancelledReservationCount, IEnumerable<ReservationInfo>)> MostCancelledUserAsync(
            DateTime startDate,
            DateTime endDate,
            bool trackChanges);
    }
}
