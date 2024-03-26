using Entities.Exceptions;
using Entities.Models;
using Repositories.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ChairManager : IChairService
    {
        private readonly IRepositoryManager _manager;
        private readonly IReservationInfoService _reservationInfoService;
        public ChairManager(IRepositoryManager manager, IReservationInfoService reservationInfoService)
        {
            _manager = manager;
            _reservationInfoService = reservationInfoService;
        }

        public async Task<Chair> CreateOneReservationInfoAsync(Chair chair)
        {
            if (chair is null)
                throw new ArgumentException(nameof(chair));


            _manager.Chair.CreateOneChair(chair);
            await _manager.SaveAsync();

            return chair;
        }

        public async Task<IEnumerable<Chair>> GetAllChairsAsync(bool trackChanges)
        {
            return await _manager
                .Chair
                .GetAllChairsAsync(trackChanges , includeRelated: true);
        }

        // Empty Chairs
        public async Task<IEnumerable<Chair>> GetAllEmptyChairsAsync(
            ReservationInfo reservationInfo ,bool trackChanges, string token)
        {
            var chairs = await _manager
                .Chair
                .GetAllChairsAsync(trackChanges, includeRelated: true);
            var user = await _manager.User.GetOneUserByIdAsync(token, false, true);
            if (user is null)
                throw new Exception("Please Log in");
            var filteredChairs = chairs.Where(chair =>
                chair.Table.DepartmentId == user.DepartmentId).ToList();
            foreach (var chair in filteredChairs)
            {
                var reservationsbyChair = await _reservationInfoService.GetAllReservationInfosByChairId(chair.Id,trackChanges);
                if (reservationsbyChair.Any(r =>
                    r.ReservationStartDate <= reservationInfo.ReservationStartDate.AddHours(r.Duration) &&
                    r.ReservationEndDate >= reservationInfo.ReservationStartDate &&
                    r.Status == "current"))
                {
                    chair.Status = true;
                }
            }

            if (filteredChairs.Count == 0)
                throw new Exception("All the chairs are full");
            return filteredChairs;
        }

        public async Task<Chair> GetOneChairByIdAsync(int id, bool trackChanges)
        {
            var chair = await _manager
                .Chair
                .GetOneChairByIdAsync(id, trackChanges, includeRelated: true);
            if (chair is null)
                throw new ChairNotFoundException(id);


            return chair;
        }

        public async Task<Chair> UpdateChairByIdAsync(int id, Chair updatedChair, bool trackChanges)
        {
            var chair = await _manager
                .Chair
                .GetOneChairByIdAsync(id, trackChanges, includeRelated: false); // Güncellenmiş sandalyeyi almak için includeRelated: false kullanıyoruz
            if (chair == null)
                throw new ChairNotFoundException(id);

            chair.Price = updatedChair.Price;
            chair.Status = updatedChair.Status; // Güncelleme işlemleri, updatedChair içindeki özelliklere göre yapılmalıdır
            chair.TableId = updatedChair.TableId; 

            _manager.Chair.UpdateOneChair(chair); // Güncelleme işlemi
            await _manager.SaveAsync(); // Değişiklikleri kaydet

            return chair;
        }

        public async Task DeleteChairByIdAsync(int id, bool trackChanges)
        {

            var chairReservations = await _reservationInfoService.GetOneReservationInfosByChairId(false, id);
            if (chairReservations is null)
                throw new ArgumentException(nameof(chairReservations));

            _manager.Chair.DeleteOneChair(chairReservations.Chair);
            _manager.ReservationInfo.DeleteOneReservationInfo(chairReservations);
            await _manager.SaveAsync();

        }

     /*   public async Task DeleteTableByIdAsync(int tableId, bool trackChanges)
        {
            var table = await _manager.Table.GetOneTableByIdAsync(tableId, trackChanges, includeRelated: false);

            if (table == null)
            {
                throw new Exception($"Table with ID {tableId} not found.");
            }

            var chairs = await _reservationInfoService.GetAllChairsByTableId(tableId, trackChanges);

            foreach (var chair in chairs)
            {
                await DeleteChairByIdAsync(chair.Id, trackChanges);
            }

            _manager.Table.DeleteOneTable(table);
            await _manager.SaveAsync();
        }*/


    }
}
