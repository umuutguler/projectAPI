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

        public async Task<string> MostReservedDepartmentAsync(DateTime startDate, DateTime endDate, bool trackChanges)
        {
            var reservations = await GetReservationsByTimeRange(startDate, endDate, trackChanges);

            // Gruplayıp her departmanın rezervasyon sayısını alıyoruz
            var mostReservedDepartmentId = reservations
                .GroupBy(r => r.Chair.Table.DepartmentId)
                .OrderByDescending(g => g.Count())
                .Select(g => g.Key)
                .FirstOrDefault();

            // En çok rezervasyon yapılan departmana ait rezervasyon sayısını buluyoruz
            var mostReservedDepartmentReservationCount = reservations
                .Count(r => r.Chair.Table.DepartmentId == mostReservedDepartmentId);
            var mostReservedDepartment = await _manager.Department.GetOneDepartmentByIdAsync(mostReservedDepartmentId, trackChanges, true);

            // İstenen çıktıyı oluşturuyoruz
            var result = $"Most Reservation Department: {mostReservedDepartment.DepartmentName}, Reservation Count: {mostReservedDepartmentReservationCount}";


            return result;
        }

        public async Task<string> MostReservedUserAsync(DateTime startDate, DateTime endDate, bool trackChanges)
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

            var user = await _manager.User.GetOneUserByIdAsync(mostReservedUser.Key, trackChanges, true);
            return $"Most Reserved User: {user.FirstName} {user.LastName},  Reservation Count: {mostReservedUser.Count()}";
        }

        public async Task<string> MostCancelledUserAsync(DateTime startDate, DateTime endDate, bool trackChanges)
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
            var user = await _manager.User.GetOneUserByIdAsync(mostCancelledUser.Key, trackChanges, true);
            return $"Most Cancelled User: {user.FirstName} {user.LastName}, Cancelled Reservation Count: {mostCancelledUser.Count()}";
        }

        public async Task<string> GetReservedChairCountByTableIdAsync(int id, DateTime reservationStartDate, DateTime reservationEndDate, bool trackChanges)
        {
            var reservations = await GetReservationsByTimeRange(reservationStartDate, reservationEndDate, trackChanges);
            // Belirli bir masa ID'si ile ilgili rezervasyonları al
            var tableReservations = reservations.Where(r => r.Chair.Table.Id == id && r.Status == "current").Count();
            return $"Table ID: {id}, Reservation Count: {tableReservations}";
        }


        public async Task<List<string>> ChairOccupancyRate(DateTime reservationStartDate, DateTime reservationEndDate, bool trackChanges)
        {
            var chairs = await _manager.Chair.GetAllChairsAsync(trackChanges, includeRelated: true);
            List<string> chairOccupancyRates = new List<string>();
            foreach (var chair in chairs)
            {
                var reservations = await _reservationInfoService.GetAllReservationInfosByChairId(chair.Id, trackChanges);
                int totalDuration = reservations
                    .Where(r => r.Status != "canceled" && r.ReservationStartDate >= reservationStartDate && r.ReservationEndDate <= reservationEndDate)
                    .Sum(r => r.Duration);
                TimeSpan difference = reservationEndDate - reservationStartDate;
                double totalHours = difference.TotalHours;
                double occupancyRate = (totalDuration / totalHours) * 100; 
                string chairOccupancyRate = $"Chair Id: {chair.Id} Occupancy Rate By Time Range: %{occupancyRate}";
                chairOccupancyRates.Add(chairOccupancyRate);
            }
            return chairOccupancyRates;
        }

        public async Task<List<string>> GenerateReservationReport(int tableId, DateTime startDate, DateTime endDate, bool trackChanges)
        {
            var mostReservedDepartment = await MostReservedDepartmentAsync(startDate, endDate, trackChanges);
            var mostReservedUser = await MostReservedUserAsync(startDate, endDate, trackChanges);
            var mostCancelledUser = await MostCancelledUserAsync(startDate, endDate, trackChanges);
            var chairOccupancyRates = await ChairOccupancyRate(startDate, endDate, trackChanges);
            List<string> report = new List<string>();
            report.AddRange(new[] { mostReservedUser, mostCancelledUser, mostReservedDepartment });
            report.AddRange(chairOccupancyRates);

            return report;
        }
    }

}