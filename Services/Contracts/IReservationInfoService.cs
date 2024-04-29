using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.DataTransferObjects;
using Entities.Models;
using Entities.RequestFeatures;
using Iyzipay.Model;
using MongoDB.Bson;

namespace Services.Contracts
{
    public interface IReservationInfoService
    {
        Task<IEnumerable<ReservationInfo>> GetAllReservationInfosAsync(bool trackChanges);
        Task<(IEnumerable<ReservationInfo> reservations, MetaData metaData)> GetAllReservationInfosByUserId(ReservationParameters reservationParameters ,bool trackChanges, String token);
        Task<IEnumerable<ReservationInfo>> GetAllReservationInfosByChairId(int chairId, bool trackChanges);
        Task<ReservationInfo> GetOneReservationInfosByChairId(bool trackChanges, int chairId);
        Task<IEnumerable<Chair>> GetAllChairsByTableId(int tableId, bool trackChanges); 
        Task<ReservationInfo> GetOneReservationInfoByIdAsync(ObjectId id, bool trackChanges);
        Task<ReservationInfo> CreateOneReservationInfoAsync(ReservationInfo reservationInfo, String token);
        Task UpdateOneReservationInfoAsync(ObjectId id, ReservationInfo reservationInfo, bool trackChanges, String token);
        Task CancelOneReservationInfoAsync(ObjectId id, bool trackChanges, String token);

        Task DeleteOneReservationInfoAsync(ObjectId id, bool trackChanges);
        Task<Boolean> IsAvailable(ReservationInfo reservationInfo);

        Task AreReservationsUpToDate();

        Task<ThreedsInitialize> CreateReservationWithPayment(PaymentDto paymentDto, String token);
    }
}
