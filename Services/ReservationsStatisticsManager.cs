using Entities.Models;
using Repositories.Contracts;
using Services.Contracts;

namespace Services
{
    public class ReservationsStatisticsManager : IReservationsStatisticsService
    {
        private readonly IRepositoryManager _manager;
        private readonly IReservationInfoService _reservationInfoService;
        public ReservationsStatisticsManager(IRepositoryManager manager, IReservationInfoService reservationInfoService)
        {
            _manager = manager;
            _reservationInfoService = reservationInfoService;

        }

        public async Task<IEnumerable<ReservationInfo>> GetReservationsStatisticsAsync(DateTime startDate, DateTime endDate, bool trackChanges)
        {
            var reservations = await _reservationInfoService.GetAllReservationInfosAsync(trackChanges);
            return reservations;
        }
    }
}
