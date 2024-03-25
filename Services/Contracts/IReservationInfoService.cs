﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;

namespace Services.Contracts
{
    public interface IReservationInfoService
    {
        Task<IEnumerable<ReservationInfo>> GetAllReservationInfosAsync(bool trackChanges);
        Task<IEnumerable<ReservationInfo>> GetAllReservationInfosByUserId(bool trackChanges, String token);
        Task<IEnumerable<ReservationInfo>> GetAllReservationInfosByChairId(int chairId, bool trackChanges);
        Task<ReservationInfo> GetOneReservationInfosByChairId(bool trackChanges, int chairId);
        Task<IEnumerable<Chair>> GetAllChairsByTableId(int tableId, bool trackChanges); 
        Task<ReservationInfo> GetOneReservationInfoByIdAsync(int id, bool trackChanges);
        Task<ReservationInfo> CreateOneReservationInfoAsync(ReservationInfo reservationInfo, String token);
        Task UpdateOneReservationInfoAsync(int id, ReservationInfo reservationInfo, bool trackChanges, String token);
        Task CancelOneReservationInfoAsync(int id, bool trackChanges, String token);

        Task DeleteOneReservationInfoAsync(int id, bool trackChanges);

        Task AreReservationsUpToDate();
    }
}
