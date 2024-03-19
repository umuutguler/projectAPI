using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;

namespace Services.Contracts
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUsersAsync(bool trackChanges);
        Task<User> GetOneUserByIdAsync(string id, bool trackChanges);
        Task UpdateOneUserAsync(string id, User user, bool trackChanges);
        Task DeleteOneUserAsync(string id, bool trackChanges);
    }
}
