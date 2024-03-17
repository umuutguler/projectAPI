using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IChairService
    {
        Task<IEnumerable<Chair>> GetAllChairsAsync(bool trackChanges);
        Task<Chair> GetOneChairByIdAsync(int id, bool trackChanges);

        Task<IEnumerable<Chair>> GetAllEmptyChairsAsync(bool trackChange);

    }
}
