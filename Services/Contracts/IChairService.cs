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
        Task<Chair> CreateOneReservationInfoAsync(Chair chair);

        Task<IEnumerable<Chair>> GetAllEmptyChairsAsync(bool trackChange);

        Task<Chair> UpdateChairByIdAsync(int id,Chair updatedChair, bool trackChanges);
    }
}
