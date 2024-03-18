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


            Console.Write("         UserId:       ");
            Console.Write(token);


            /*Console.Write("         ChairId       ");
            Console.Write(reservationInfo.ChairId);

            var chair = await _manager.Chair.GetOneChairByIdAsync(1, false, true);
            chair.Status = true;

            Console.Write(chair.Status);
            
            Console.Write(chair);*/


            _manager.ReservationInfo.CreateOneReservationInfo(reservationInfo);
            await _manager.SaveAsync();

            return reservationInfo;
        }

        public async Task UpdateOneReservationInfoAsync(int id, ReservationInfo reservationInfo, bool trackChanges, String token)
        {
            var entity = await _manager.ReservationInfo.GetOneReservationInfoByIdAsync(id, trackChanges, includeRelated: true);
            if (entity is null)
                throw new Exception($"Reservation with id:{id} could not found.");

            Console.Write("         UserId:       ");
            Console.Write(token);

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
            var entity = await _manager.ReservationInfo.GetOneReservationInfoByIdAsync(id, trackChanges, includeRelated: true);
            if (entity is null)
                throw new ArgumentException(nameof(entity));

            entity.Status = false;

            _manager.ReservationInfo.DeleteOneReservationInfo(entity);
            await _manager.SaveAsync();
        }
    }
}
