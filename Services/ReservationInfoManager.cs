using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;
using Repositories.Contracts;
using Services.Contracts;

namespace Services
{
    public class ReservationInfoManager : IReservationInfoService
    {
        private readonly IRepositoryManager _manager;
        public ReservationInfoManager(IRepositoryManager manager)
        {
            _manager = manager;
        }

        public async Task<IEnumerable<ReservationInfo>> GetAllReservationInfosAsync(bool trackChanges)
        {
            return await _manager.ReservationInfo.GetAllReservationInfosAsync(trackChanges);
        }

        public async Task<ReservationInfo> GetOneReservationInfoByIdAsync(int id, bool trackChanges)
        {
            var info = await _manager.ReservationInfo.GetOneReservationInfoByIdAsync(id, trackChanges);

            return info;
        }

        public async Task<ReservationInfo> CreateOneReservationInfoAsync(ReservationInfo reservationInfo)
        {
            if (reservationInfo is null)
                throw new ArgumentException(nameof(reservationInfo));
 
            reservationInfo.CreateDate = DateTime.Now;
            reservationInfo.Updatdate ??= new List<DateTime>();
            reservationInfo.Updatdate.Add(DateTime.Now);
            reservationInfo.ReservationEndDate = reservationInfo.ReservationStartDate.AddDays(1);
            reservationInfo.Status = true;

            _manager.ReservationInfo.CreateOneReservationInfo(reservationInfo);
            await _manager.SaveAsync();

            return reservationInfo;
        }

        public async Task UpdateOneReservationInfoAsync(int id, ReservationInfo reservationInfo, bool trackChanges)
        {
            var entity = await _manager.ReservationInfo.GetOneReservationInfoByIdAsync(id, trackChanges);
            if (entity is null)
                throw new Exception($"Reservation with id:{id} could not found.");

            entity.ReservationStartDate = reservationInfo.ReservationStartDate;
            entity.User = reservationInfo.User;
            entity.ChairId = reservationInfo.ChairId;

            entity.Updatdate.Add(DateTime.Now);
            entity.ReservationEndDate = reservationInfo.ReservationStartDate.AddDays(1);

            _manager.ReservationInfo.Update(entity);
            await _manager.SaveAsync();
        }

        public async Task DeleteOneReservationInfoAsync(int id, bool trackChanges)
        {
            var entity = await _manager.ReservationInfo.GetOneReservationInfoByIdAsync(id, trackChanges);
            if (entity is null)
                throw new ArgumentException(nameof(entity));

            _manager.ReservationInfo.DeleteOneReservationInfo(entity);
            await _manager.SaveAsync();
        }
    }
}
