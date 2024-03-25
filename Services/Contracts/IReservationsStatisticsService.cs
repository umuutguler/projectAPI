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

        Task<string> MostReservedDepartmentAsync(
            DateTime startDate,
            DateTime endDate,
            bool trackChanges);

        Task<string> MostReservedUserAsync(
            DateTime startDate,
            DateTime endDate,
            bool trackChanges);

         Task<string> MostCancelledUserAsync(
            DateTime startDate,
            DateTime endDate,
            bool trackChanges);
        Task<string> GetReservedChairCountByTableIdAsync(
            int id,
            DateTime startDate,
            DateTime endDate,
            bool trackChanges);

        Task<string> GenerateReservationReport(
            int tableId,
            DateTime startDate, 
            DateTime endDate, 
            bool trackChanges);

    }
}
