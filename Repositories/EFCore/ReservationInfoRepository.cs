using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using Repositories.MongoDB;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Repositories.EFCore
{
    public class ReservationInfoRepository : RepositoryBase<ReservationInfo>, IReservationInfoRepository
    {
        private readonly RepositoryContext _context;
        public ReservationInfoRepository(RepositoryContext context) : base(context)
        {
            _context = context;
        }
        public async Task<IEnumerable<ReservationInfo>> GetAllReservationInfosByUserIdAsync(bool trackChanges, bool includeRelated )

        {  
            return await _context.Set<ReservationInfo>().OrderByDescending(r => r.ReservationStartDate).ToListAsync();
            
        }
    }
}
