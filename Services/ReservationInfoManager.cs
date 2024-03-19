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


            var chair = await _manager.Chair.GetOneChairByIdAsync(reservationInfo.ChairId, false, true);
            var user = await _manager.User.GetOneUserByIdAsync(token, false, true);

            if (chair.Status == true)
                throw new Exception($"Chair by id: {reservationInfo.ChairId} is already reserved ");

            if (user.DepartmentId != chair.Table.DepartmentId)
                throw new Exception($"Chair by id: {reservationInfo.ChairId} does not belong to your department. ");

            reservationInfo.CreateDate = DateTime.Now;
            reservationInfo.Updatdate ??= new List<DateTime>();
            reservationInfo.Updatdate.Add(DateTime.Now);
            reservationInfo.ReservationEndDate = reservationInfo.ReservationStartDate.AddDays(1);
            reservationInfo.Status = true;
            reservationInfo.UserId = token;

           
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


            var newchair = await _manager.Chair.GetOneChairByIdAsync(reservationInfo.ChairId, false, true);
            var user = await _manager.User.GetOneUserByIdAsync(token, false, true);

            if (newchair.Status == true && entity.ChairId!=reservationInfo.ChairId)
                throw new Exception($"Chair by id:{reservationInfo.ChairId} is already reserved ");
            if (user.DepartmentId != newchair.Table.DepartmentId)
                throw new Exception($"Chair by id: {reservationInfo.ChairId} does not belong to your department. ");

            entity.Chair.Status = false;
            newchair.Status = true;
            
            

            entity.ReservationStartDate = reservationInfo.ReservationStartDate;
 
            entity.ChairId = reservationInfo.ChairId;
            entity.UserId = token;
            entity.Chair = reservationInfo.Chair;
            entity.User = reservationInfo.User;

            entity.Updatdate.Add(DateTime.Now);
            entity.ReservationEndDate = reservationInfo.ReservationStartDate.AddDays(1);

            
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
            entity.Chair.Status = false;
            _manager.ReservationInfo.Update(entity);


            _manager.ReservationInfo.DeleteOneReservationInfo(entity);
            await _manager.SaveAsync();

        }

        
    }
}
