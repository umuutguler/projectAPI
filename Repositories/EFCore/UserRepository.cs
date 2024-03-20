using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;

namespace Repositories.EFCore
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(RepositoryContext context) : base(context)
        {
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync(bool trackChanges, bool includeRelated = true)
        {
            IQueryable<User> query = FindAll(trackChanges).OrderBy(t => t.UserName);
            if (includeRelated)
            {
                query = query.Include(u => u.Department)
                             .Include(u => u.ReservationInfos);
            }

            return await query.ToListAsync();
        }

        public async Task<User> GetOneUserByIdAsync(string id, bool trackChanges, bool includeRelated = true) 
        {
            IQueryable<User> query = FindByCondition(t => t.Id.Equals(id), trackChanges);

            if (includeRelated)
            {
                query = query.Include(u => u.Department)
                             .Include(u => u.ReservationInfos);
            }

            return await query.SingleOrDefaultAsync();
        }

        public void CreateOneUser(User user) => Create(user);

        public void DeleteOneUser(User user) => Delete(user);

        public void UpdateOneUser(User user) => Update(user);
    }
}
