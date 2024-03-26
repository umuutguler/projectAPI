using System.Linq.Dynamic.Core.Tokenizer;
using Entities.Models;
using Entities.RequestFeatures;
using Repositories.Contracts;
using Repositories.EFCore;
using Services.Contracts;

namespace Services
{
    public class ReservationInfoManager : IReservationInfoService
    {
        private readonly IRepositoryManager _manager;
        private readonly RepositoryContext _context;
        private readonly ICurrencyService _currencyManager;
        public ReservationInfoManager(IRepositoryManager manager, RepositoryContext context, ICurrencyService currencyManager)
        {
            _manager = manager;
            _context = context;
            _currencyManager = currencyManager;
        }

        public async Task<IEnumerable<ReservationInfo>> GetAllReservationInfosAsync(bool trackChanges) =>
            await _manager.ReservationInfo.GetAllReservationInfosAsync(trackChanges, includeRelated: true);
            


        public async Task<IEnumerable<ReservationInfo>> GetAllReservationInfosByUserId(ReservationParameters reservationParameters, bool trackChanges, string token)
        {
            var reservations = await _manager.ReservationInfo.GetAllReservationInfosByUserIdAsync(reservationParameters, trackChanges, includeRelated: true);
            var reservationsByUserId = reservations.Where(r => r.UserId == token);
            return reservationsByUserId;
        }




        public async Task<IEnumerable<ReservationInfo>> GetAllReservationInfosByChairId(int chairId, bool trackChanges)
        {
            var reservations = await _manager.ReservationInfo.GetAllReservationInfosAsync(trackChanges, includeRelated: true);
            var reservationsByChairId = reservations.Where(r => r.ChairId == chairId);
            return reservationsByChairId;
        }


        public async Task<ReservationInfo> GetOneReservationInfoByIdAsync(int id, bool trackChanges) => 
            await _manager.ReservationInfo.GetOneReservationInfoByIdAsync(id, trackChanges, includeRelated: true);


        public async Task<ReservationInfo> GetOneReservationInfosByChairId(bool trackChanges, int chairId)
        {
            var reservation = await _manager.ReservationInfo.GetAllReservationInfosAsync(trackChanges, includeRelated: true);
            var reservationByChairId = reservation.SingleOrDefault(c => c.ChairId == chairId);
            return reservationByChairId;
        }

        public async Task<ReservationInfo> CreateOneReservationInfoAsync(ReservationInfo reservationInfo, String token)
        {
            if (reservationInfo is null)
                throw new ArgumentException(nameof(reservationInfo));

            var chair = await _manager.Chair.GetOneChairByIdAsync(reservationInfo.ChairId, false, true);
            var user = await _manager.User.GetOneUserByIdAsync(token, false, true);
            if (user is null)
                throw new Exception($"User could not found.");
            if (user.DepartmentId != chair.Table.DepartmentId)
                throw new Exception($"Chair by id: {reservationInfo.ChairId} does not belong to your department. ");
            
            Decimal dollarRate = await _currencyManager.GetUSDRate();
            reservationInfo.ReservationPrice = reservationInfo.Duration * chair.Price *  dollarRate;
            reservationInfo.CreateDate = DateTime.Now;
            reservationInfo.Updatdate ??= new List<DateTime>();
            reservationInfo.Updatdate.Add(DateTime.Now);
            reservationInfo.ReservationEndDate = reservationInfo.ReservationStartDate.AddHours(reservationInfo.Duration);
            reservationInfo.Status = "current";
            reservationInfo.UserId = token;

            if (await IsAvailable(reservationInfo))
                throw new Exception($"Chair by Id: {reservationInfo.ChairId}  {reservationInfo.ReservationStartDate}-{reservationInfo.ReservationEndDate} is already reserved ");

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
            if (user is null)
                throw new Exception($"User could not found.");
            if (user.Id != entity.UserId)
                throw new Exception("This reservation does not belong to you");

            if (entity.ReservationStartDate <= DateTime.Now)
                throw new Exception("Your reservation has started. You cannot make changes");
            if (user.DepartmentId != newchair.Table.DepartmentId)
                throw new Exception($"Chair by id: {reservationInfo.ChairId} does not belong to your department. ");
            if (newchair.Status == true && entity.ChairId!=reservationInfo.ChairId)
                throw new Exception($"Chair by id:{reservationInfo.ChairId} is already reserved ");

            if(entity.Status!="current")
                throw new Exception($"Your Reservations is {entity.Status}");

            Decimal dollarRate = await _currencyManager.GetUSDRate();

            entity.ReservationStartDate = reservationInfo.ReservationStartDate;
            entity.Duration = reservationInfo.Duration;
            entity.ChairId = reservationInfo.ChairId;
            entity.Chair = reservationInfo.Chair;
            entity.ReservationEndDate = reservationInfo.ReservationStartDate.AddHours(entity.Duration);
            entity.Updatdate.Add(DateTime.Now);
            entity.ReservationPrice = reservationInfo.Duration * newchair.Price * dollarRate;

            if (await IsAvailable(entity))
                throw new Exception($"Chair by Id: {entity.ChairId}  {entity.ReservationStartDate}-{entity.ReservationEndDate} is already reserved ");

            _manager.Chair.Update(newchair);
            _manager.ReservationInfo.Update(entity);
            await _manager.SaveAsync();
        }

        public async Task CancelOneReservationInfoAsync(int id, bool trackChanges, String token)
        {
            var entity = await _manager.ReservationInfo.GetOneReservationInfoByIdAsync(id, trackChanges, includeRelated: true);
            if (entity is null)
                throw new Exception($"Reservation with id:{id} could not found.");
            var user = await _manager.User.GetOneUserByIdAsync(token, false, true);
            if (user is null)
                throw new Exception($"User could not found.");
            if (user.Id != entity.UserId)
                throw new Exception("This reservation does not belong to you");
            if (entity.ReservationStartDate <= DateTime.Now)
                throw new Exception("Your reservation has started. You cannot make changes");
            if (entity.Status != "current")
                throw new Exception($"Your Reservations is {entity.Status}");

            entity.Status = "canceled";
            _manager.ReservationInfo.Update(entity);
            await _manager.SaveAsync();
        }

        public async Task DeleteOneReservationInfoAsync(int id, bool trackChanges)
        {
            var entity = await _manager.ReservationInfo.GetOneReservationInfoByIdAsync(id, trackChanges, includeRelated: true);
            if (entity is null)
                throw new ArgumentException(nameof(entity));
            /*if (entity.ReservationStartDate <= DateTime.Now)
                throw new Exception("Your reservation has started. You cannot make changes");*/

            _manager.ReservationInfo.DeleteOneReservationInfo(entity);
            await _manager.SaveAsync();
        }

        public async Task<Boolean> IsAvailable(ReservationInfo reservationInfo)
        {
            var reservations = await GetAllReservationInfosAsync(false);

            return reservations.Any(r =>
            r.Id != reservationInfo.Id &&
            r.ReservationStartDate <= reservationInfo.ReservationStartDate.AddHours(r.Duration) &&
            r.ReservationEndDate >= reservationInfo.ReservationStartDate &&
            reservationInfo.ChairId == r.ChairId&&
            r.Status != "canceled");
        }

        public async Task AreReservationsUpToDate() // IEnumerable<ReservationInfo> reservations
        {
            var reservations = await GetAllReservationInfosAsync(true);

            var endReservations = reservations.Where(r => r.ReservationEndDate < DateTime.Now && r.Status == "current");
            endReservations.ToList().ForEach(r => r.Status = "nonCurrent");

            _context.ReservationInfos.UpdateRange(endReservations);
            _context.SaveChanges();
        }


        //  --- Chair ---
        
        public async Task<IEnumerable<Chair>> GetAllChairsByTableId(int tableId, bool trackChanges)
        {
            var chairs = await _manager
                .Chair
                .GetAllChairsAsync(trackChanges, includeRelated: true);

            return chairs.Where(c => c.TableId == tableId);
        }
    }
}
