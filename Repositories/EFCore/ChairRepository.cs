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
    public class ChairRepository : RepositoryBase<Chair>, IChairRepository
    {
        public ChairRepository(RepositoryContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Chair>> GetAllChairsAsync(bool trackChanges, bool includeRelated = true)
        {
            IQueryable<Chair> query = FindAll(trackChanges).OrderBy(c => c.Id);

            if (includeRelated)
            {
                query = query.Include(c => c.Table)
                              .ThenInclude(c => c.Department);
            }

            return await query.ToListAsync();
        }

        public async Task<Chair> GetOneChairByIdAsync(int id, bool trackChanges, bool includeRelated = true)
        {
            IQueryable<Chair> query = FindByCondition(c => c.Id.Equals(id), trackChanges);

            if (includeRelated)
            {
                query = query.Include(c => c.Table);
            }

            return await query.SingleOrDefaultAsync();
        }

        public void CreateOneChair(Chair chair) => Create(chair);

        public void DeleteOneChair(Chair chair) => Delete(chair);

        public void UpdateOneChair(Chair chair) => Update(chair);

        // EmptyChairs
        public async Task<IEnumerable<Chair>> GetAllEmptyChairsAsync(bool trackChanges, bool includeRelated)
        {
            IQueryable<Chair> query = FindAll(trackChanges).OrderBy(c => c.Id);

            if (includeRelated)
            {
                query = query.Include(c => c.Table);
            }

            return await query.ToListAsync();
        }
    }
}
