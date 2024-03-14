using Entities.Models;

namespace Repositories.Contracts
{
    public interface IChairRepository : IRepositoryBase<Chair>
    {
        Task<IEnumerable<Chair>> GetAllChairsAsync(bool trackChanges);
        Task<Chair> GetOneChairByIdAsync(int id, bool trackChanges);
        void CreateOneChair(Chair chair);
        void UpdateOneChair(Chair chair);
        void DeleteOneChair(Chair chair);
    }
}
