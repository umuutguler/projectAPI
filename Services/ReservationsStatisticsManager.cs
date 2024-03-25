﻿using Entities.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Repositories.Contracts;
using Services.Contracts;
using System.Text;
using System.Text.Json;

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

            // İstenen çıktıyı oluşturuyoruz
            var result = $"Most Reservation Department: {mostReservedDepartmentId}, Reservation Count: {mostReservedDepartmentReservationCount}";


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

            return $"Most Reserved User: {mostReservedUser.Key},  Reservation Count: {mostReservedUser.Count()}";
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

            return $"Most Cancelled User: {mostCancelledUser.Key}, Cancelled Reservation Count: {mostCancelledUser.Count()}";
        }

        public async Task<string> GetReservedChairCountByTableIdAsync(int id, DateTime reservationStartDate, DateTime reservationEndDate, bool trackChanges)
        {
            var reservations = await GetReservationsByTimeRange(reservationStartDate, reservationEndDate, trackChanges);
            // Belirli bir masa ID'si ile ilgili rezervasyonları al
            var tableReservations = reservations.Where(r => r.Chair.Table.Id == id && r.Status == "current").Count();
            return $"Table ID: {id}, Reservation Count: {tableReservations}";
        }


        public async Task<string> GenerateReservationReport(int tableId, DateTime startDate, DateTime endDate, bool trackChanges)
        {
            var mostReservedDepartment = await MostReservedDepartmentAsync(startDate, endDate, trackChanges);
            var mostReservedUser = await MostReservedUserAsync(startDate, endDate, trackChanges);
            var mostCancelledUser = await MostCancelledUserAsync(startDate, endDate, trackChanges);
            var tableReservation = await GetReservedChairCountByTableIdAsync(tableId, startDate, endDate, trackChanges);

            var report = new
            {
                mostReservedDepartment,
                mostReservedUser,
                mostCancelledUser,
                tableReservation
            };

            return JsonSerializer.Serialize(report, new JsonSerializerOptions { WriteIndented = true });
        }
    }

}