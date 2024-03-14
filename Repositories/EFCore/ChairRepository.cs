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

        public async Task<IEnumerable<Chair>> GetAllChairsAsync(bool trackChanges) =>
             await FindAll(trackChanges)
                .OrderBy(c => c.Id)
                .ToListAsync();

        public async Task<Chair> GetOneChairByIdAsync(int id, bool trackChanges) =>
            await FindByCondition(c => c.Id.Equals(id), trackChanges)
                .SingleOrDefaultAsync();

        public void CreateOneChair(Chair chair) => Create(chair);

        public void DeleteOneChair(Chair chair) => Delete(chair);

        public void UpdateOneChair(Chair chair) => Update(chair);
    }
}
