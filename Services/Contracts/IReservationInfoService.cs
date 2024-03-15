using System;
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
        Task<ReservationInfo> GetOneReservationInfoByIdAsync(int id, bool trackChanges);
        Task<ReservationInfo> CreateOneReservationInfoAsync(ReservationInfo reservationInfo);
        Task UpdateOneReservationInfoAsync(int id, ReservationInfo reservationInfo, bool trackChanges);
        Task DeleteOneReservationInfoAsync(int id, bool trackChanges);
    }
}
