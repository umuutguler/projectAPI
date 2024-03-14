using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;

namespace Repositories.Contracts
{
    public interface IReservationInfoRepository : IRepositoryBase<ReservationInfo>
    {
        Task<IEnumerable<ReservationInfo>> GetAllReservationInfosAsync(bool trackChanges);
        Task<ReservationInfo> GetOneReservationInfoByIdAsync(int id, bool trackChanges);
        void CreateOneReservationInfo(ReservationInfo reservationInfo);
        void UpdateOneReservationInfo(ReservationInfo reservationInfo);
        void DeleteOneReservationInfo(ReservationInfo reservationInfo);
    }
}
