using Entities.Models;

namespace Repositories.Contracts
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        Task<IEnumerable<User>> GetAllUsersAsync(bool trackChanges, bool includeRelated);
        Task<User> GetOneUserByIdAsync(string id, bool trackChanges, bool includeRelated);
        void CreateOneUser(User user);
        void UpdateOneUser(User user);
        void DeleteOneUser(User user);
    }
}
