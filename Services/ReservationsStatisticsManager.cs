using Entities.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
            var mostReservedDepartmentId = reservations
                .GroupBy(r => r.Chair.Table.DepartmentId)
                .OrderByDescending(g => g.Count())
                .Select(g => g.Key)
                .FirstOrDefault();

            // En çok rezervasyon yapılan departmana ait rezervasyonları döndür
            var mostReservedDepartmentReservations = mostReservedDepartmentId != null
                ? reservations.Where(r => r.Chair.Table.DepartmentId == mostReservedDepartmentId)
                : Enumerable.Empty<ReservationInfo>();

            return (mostReservedDepartmentReservations.Count(), mostReservedDepartmentReservations);
        }

        public async Task<(int TotalReservationCount, IEnumerable<ReservationInfo>)> MostReservedUserAsync(DateTime startDate, DateTime endDate, bool trackChanges)
        {
            var reservations = await GetReservationsByTimeRange(startDate, endDate, trackChanges);

            // Kullanıcı bazında rezervasyon sayısını alıyoruz
            var mostReservedUser = reservations
                .GroupBy(r => r.UserId)
                .OrderByDescending(g => g.Count())
                .FirstOrDefault();

            // En çok rezervasyon yapan kullanıcının rezervasyonlarını döndür
            var mostReservedUserReservations = mostReservedUser != null
                ? reservations.Where(r => r.UserId == mostReservedUser.Key)
                : Enumerable.Empty<ReservationInfo>();

            return (mostReservedUserReservations.Count(), mostReservedUserReservations);
        }

        public async Task<(int TotalCancelledReservationCount, IEnumerable<ReservationInfo>)> MostCancelledUserAsync(DateTime startDate, DateTime endDate, bool trackChanges)
        {
            var reservations = await GetReservationsByTimeRange(startDate, endDate, trackChanges);

            // Kullanıcı bazında iptal edilen rezervasyon sayısını alıyoruz
            var mostCancelledUser = reservations
                .Where(r => r.Status == "canceled")
                .GroupBy(r => r.UserId)
                .OrderByDescending(g => g.Count())
                .FirstOrDefault();

            // En çok rezervasyonu iptal eden kullanıcının iptal edilen rezervasyonlarını döndür
            var mostCancelledUserReservations = mostCancelledUser != null
                ? reservations.Where(r => r.UserId == mostCancelledUser.Key && r.Status == "canceled")
                : Enumerable.Empty<ReservationInfo>();

            return (mostCancelledUserReservations.Count(), mostCancelledUserReservations);
        }

        public async Task<(int TableReservationCount, IEnumerable<ReservationInfo>)> GetReservedChairCountByTableIdAsync(int id, DateTime reservationStartDate, DateTime reservationEndDate, bool trackChanges)
        {
            var reservations = await GetReservationsByTimeRange(reservationStartDate, reservationEndDate, trackChanges);
            // Belirli bir masa ID'si ile ilgili rezervasyonları al
            var tableReservations = reservations.Where(r => r.Chair.Table.Id == id && r.Status == "current");
            return (tableReservations.Count(), tableReservations);
        }
    }
}