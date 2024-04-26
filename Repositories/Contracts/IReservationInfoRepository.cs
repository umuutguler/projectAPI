using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;
using Entities.RequestFeatures;

namespace Repositories.Contracts
{
    public interface IReservationInfoRepository : IRepositoryBase<ReservationInfo>
    {
      Task<IEnumerable<ReservationInfo>> GetAllReservationInfosByUserIdAsync(bool trackChanges, bool includeRelated);
    }
}
