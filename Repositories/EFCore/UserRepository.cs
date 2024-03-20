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

        public async Task<User> GetOneUserByIdAsync(string id, bool trackChanges = false, bool includeRelated = false)
        {
            // Tercih ettiğiniz FindByCondition metodunu kullanın
            var query = FindByCondition(t => t.Id.Equals(id), trackChanges);

            // İlgili varlıkları dahil etmek istiyorsanız, Include'u kullanın
            if (includeRelated)
            {
                // Departman ve ReservationInfos varlıklarını dahil edin
                query = query.Include(u => u.Department).Include(u => u.ReservationInfos);
            }

            return await query.SingleOrDefaultAsync();
        }


        public void CreateOneUser(User user) => Create(user);

        public void DeleteOneUser(User user) => Delete(user);

        public void UpdateOneUser(User user) => Update(user);
    }
}
