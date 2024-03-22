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
        Task<IEnumerable<ReservationInfo>> GetReservationsStatisticsAsync(
            DateTime reservationStartDate,
            DateTime reservationEndDate,
            bool trackChanges);
    }
}
