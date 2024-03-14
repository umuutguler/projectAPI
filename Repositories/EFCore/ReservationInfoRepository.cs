using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;

namespace Repositories.EFCore
{
    public class ReservationInfoRepository : RepositoryBase<ReservationInfo>, IReservationInfoRepository
    {
        public ReservationInfoRepository(RepositoryContext context) : base(context)
        {
        }


        public async Task<IEnumerable<ReservationInfo>> GetAllReservationInfosAsync(bool trackChanges) =>
            await FindAll(trackChanges)
            .OrderBy(r => r.Id)
            .ToListAsync();

        public async Task<ReservationInfo> GetOneReservationInfoByIdAsync(int id, bool trackChanges) =>
            await FindByCondition(r => r.Id.Equals(id), trackChanges)
            .SingleOrDefaultAsync();

        public void CreateOneReservationInfo(ReservationInfo reservationInfo) => Create(reservationInfo);

        public void DeleteOneReservationInfo(ReservationInfo reservationInfo) => Delete(reservationInfo);

        public void UpdateOneReservationInfo(ReservationInfo reservationInfo) => Update(reservationInfo);
    }
}
