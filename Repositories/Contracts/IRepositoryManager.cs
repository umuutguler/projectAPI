namespace Repositories.Contracts
{
    public interface IRepositoryManager
    {
        IProductRepository Product { get; }
        Task SaveAsync(); // KAYIT İŞLEMİ - void Save() -> Task<void> yerine Task
    }
}
