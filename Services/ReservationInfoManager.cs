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
            return await _manager.ReservationInfo.GetAllReservationInfosAsync(trackChanges, includeRelated: true);
        }

        public async Task<IEnumerable<ReservationInfo>> GetAllReservationInfosByUserId(bool trackChanges, string token)
        {
            var reservations = await _manager.ReservationInfo.GetAllReservationInfosAsync(trackChanges, includeRelated: true);
            var reservationsByUserId = reservations.Where(r => r.UserId == token).ToList();
            return reservationsByUserId;
        }

        public async Task<ReservationInfo> GetOneReservationInfoByIdAsync(int id, bool trackChanges)
        {
            var info = await _manager.ReservationInfo.GetOneReservationInfoByIdAsync(id, trackChanges, includeRelated: true);

            return info;
        }

        public async Task<ReservationInfo> CreateOneReservationInfoAsync(ReservationInfo reservationInfo, String token)
        {
            if (reservationInfo is null)
                throw new ArgumentException(nameof(reservationInfo));
 
            reservationInfo.CreateDate = DateTime.Now;
            reservationInfo.Updatdate ??= new List<DateTime>();
            reservationInfo.Updatdate.Add(DateTime.Now);
            reservationInfo.ReservationEndDate = reservationInfo.ReservationStartDate.AddDays(1);
            reservationInfo.Status = true;
            reservationInfo.UserId = token;

            var chair = await _manager.Chair.GetOneChairByIdAsync(reservationInfo.ChairId, false, true);
            chair.Status = true;
            _manager.Chair.Update(chair);

            _manager.ReservationInfo.CreateOneReservationInfo(reservationInfo);
            await _manager.SaveAsync();

            return reservationInfo;
        }

        public async Task UpdateOneReservationInfoAsync(int id, ReservationInfo reservationInfo, bool trackChanges, String token)
        {
            var entity = await _manager.ReservationInfo.GetOneReservationInfoByIdAsync(id, trackChanges, includeRelated: true);
            if (entity is null)
                throw new Exception($"Reservation with id:{id} could not found.");

            var chair = await _manager.Chair.GetOneChairByIdAsync(entity.ChairId, false, true);
            chair.Status = false;
            var newchair = await _manager.Chair.GetOneChairByIdAsync(reservationInfo.ChairId, false, true);
            newchair.Status = true;

            entity.ReservationStartDate = reservationInfo.ReservationStartDate;
            
            entity.ChairId = reservationInfo.ChairId;
            entity.UserId = token;
            entity.Chair = reservationInfo.Chair;
            entity.User = reservationInfo.User;

            entity.Updatdate.Add(DateTime.Now);
            entity.ReservationEndDate = reservationInfo.ReservationStartDate.AddDays(1);

            _manager.Chair.Update(chair);
            _manager.Chair.Update(newchair);
            _manager.ReservationInfo.Update(entity);
            await _manager.SaveAsync();
        }

        public async Task DeleteOneReservationInfoAsync(int id, bool trackChanges)
        {
            var entity = await _manager.ReservationInfo.GetOneReservationInfoByIdAsync(id, trackChanges, includeRelated: true);
            if (entity is null)
                throw new ArgumentException(nameof(entity));

            entity.Status = false;

            var chair = await _manager.Chair.GetOneChairByIdAsync(entity.ChairId, false, true);
            chair.Status = false;
            _manager.Chair.Update(chair);

            _manager.ReservationInfo.DeleteOneReservationInfo(entity);
            await _manager.SaveAsync();

        }

        
    }
}
