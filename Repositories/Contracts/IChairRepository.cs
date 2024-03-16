using Entities.Models;

namespace Repositories.Contracts
{
    public interface IChairRepository : IRepositoryBase<Chair>
    {
        Task<IEnumerable<Chair>> GetAllChairsAsync(bool trackChanges, bool includeRelated);
        Task<Chair> GetOneChairByIdAsync(int id, bool trackChanges, bool includeRelated);
        void CreateOneChair(Chair chair);
        void UpdateOneChair(Chair chair);
        void DeleteOneChair(Chair chair);
    }
}
