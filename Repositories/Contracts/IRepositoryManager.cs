namespace Repositories.Contracts
{
    public interface IRepositoryManager
    {
        IProductRepository Product { get; }
        IDepartmentRepository Department { get; }
        Task SaveAsync(); // KAYIT İŞLEMİ - void Save() -> Task<void> yerine Task
    }
}
