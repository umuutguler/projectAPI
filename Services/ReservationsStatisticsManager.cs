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

        public async Task<IEnumerable<ReservationInfo>> GetReservationsByTimeRange(DateTime startDate, DateTime endDate, bool trackChanges)
        {
            var reservations = await _reservationInfoService.GetAllReservationInfosAsync(trackChanges);
            var reservationsByTimeRange = reservations.Where(r => r.ReservationStartDate >= startDate && r.ReservationEndDate <= endDate);


            return reservationsByTimeRange;
        }

        public async Task<(int TotalReservationCount, IEnumerable<ReservationInfo>)> MostReservedDepartmentAsync(DateTime startDate, DateTime endDate, bool trackChanges)
        {
            var reservations = await GetReservationsByTimeRange(startDate, endDate, trackChanges); 

            // Gruplayıp her departmanın rezervasyon sayısını alıyoruz
            var departmentReservationCounts = reservations
                .GroupBy(r => r.Chair.Table.DepartmentId)
                .Select(g => new { DepartmentId = g.Key, ReservationCount = g.Count() })
                .ToList();
           
            var mostReservedDepartmentCount = departmentReservationCounts
                .OrderByDescending(g => g.ReservationCount)
                .FirstOrDefault();
           
            // En çok rezervasyon yapılan departmanın ID'sini buluyoruz
            var mostReservedDepartmentId = mostReservedDepartmentCount?.DepartmentId;
            // Toplam rezervasyon sayısını al
            var totalReservationCount = mostReservedDepartmentCount?.ReservationCount ?? 0;


            // En çok rezervasyon yapılan departmana ait rezervasyonları döndür
            var mostReservedDepartmentReservations = mostReservedDepartmentCount != null
                ? reservations.Where(r => r.Chair.Table.DepartmentId == mostReservedDepartmentCount.DepartmentId)
                : Enumerable.Empty<ReservationInfo>();

            return (totalReservationCount, mostReservedDepartmentReservations);
        }

        public async Task<(int TotalReservationCount, IEnumerable<ReservationInfo>)> MostReservedUserAsync(DateTime startDate, DateTime endDate, bool trackChanges)
        {
            var reservations = await GetReservationsByTimeRange(startDate, endDate, trackChanges);

            // Kullanıcı bazında rezervasyon sayısını alıyoruz
            var userReservationCounts = reservations
                .GroupBy(r => r.UserId)
                .Select(g => new { UserId = g.Key, ReservationCount = g.Count() })
                .ToList();

            var mostReservedUserCount = userReservationCounts
                .OrderByDescending(g => g.ReservationCount)
                .FirstOrDefault();

            // En çok rezervasyon yapan kullanıcının ID'sini buluyoruz
            var mostReservedUserId = mostReservedUserCount?.UserId;
            // Toplam rezervasyon sayısını al
            var totalReservationCount = mostReservedUserCount?.ReservationCount ?? 0;

            // En çok rezervasyon yapan kullanıcının rezervasyonlarını döndür
            var mostReservedUserReservations = mostReservedUserCount != null
                ? reservations.Where(r => r.UserId == mostReservedUserCount.UserId)
                : Enumerable.Empty<ReservationInfo>();

            return (totalReservationCount, mostReservedUserReservations);
        }
    }
}
