using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;

namespace Repositories.EFCore
{
    public class ReservationInfoRepository : RepositoryBase<ReservationInfo>, IReservationInfoRepository
    {
        public ReservationInfoRepository(RepositoryContext context) : base(context)
        {
        }


        public async Task<IEnumerable<ReservationInfo>> GetAllReservationInfosAsync(bool trackChanges, bool includeRelated = true)
        {
            IQueryable<ReservationInfo> query = FindAll(trackChanges).OrderByDescending(r => r.ReservationStartDate);

            if (includeRelated)
            {
                query = query.Include(r => r.User)
                             .Include(r => r.Chair)
                             .ThenInclude(r => r.Table);
            }

            return await query.ToListAsync();
        }

        public async Task<ReservationInfo> GetOneReservationInfoByIdAsync(int id, bool trackChanges, bool includeRelated = true)
        {
            IQueryable<ReservationInfo> query = FindByCondition(r => r.Id.Equals(id), trackChanges);

            if (includeRelated)
            {
                query = query.Include(r => r.User)
                             .Include(r => r.Chair)
                             .ThenInclude(r => r.Table);
            }

            return await query.SingleOrDefaultAsync();
        }

        public void CreateOneReservationInfo(ReservationInfo reservationInfo) => Create(reservationInfo);

        public void DeleteOneReservationInfo(ReservationInfo reservationInfo) => Delete(reservationInfo);

        public void UpdateOneReservationInfo(ReservationInfo reservationInfo) => Update(reservationInfo);

        public async Task<PagedList<ReservationInfo>> GetAllReservationInfosByUserIdAsync(ReservationParameters reservationParameters, bool trackChanges, bool includeRelated )

        {

            IQueryable<ReservationInfo> query = FindAll(trackChanges).OrderByDescending(r => r.ReservationStartDate);
                
         
            // Filtreleme işlemi
            if (!string.IsNullOrEmpty(reservationParameters.Status))
            {
                query = query.Where(r => r.Status == reservationParameters.Status);
            }


            var reservations = await query.ToListAsync();
            return PagedList<ReservationInfo>.ToPagedList(reservations, reservationParameters.PageNumber, reservationParameters.PageSize);
        }
    }
}
