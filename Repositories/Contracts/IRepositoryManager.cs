namespace Repositories.Contracts
{
    public interface IRepositoryManager
    {
        IProductRepository Product { get; }
        IDepartmentRepository Department { get; }
        IChairRepository Chair { get; }
        IReservationInfoRepository ReservationInfo { get; }
        ITableRepository Table { get; }

        Task SaveAsync(); // KAYIT İŞLEMİ - void Save() -> Task<void> yerine Task
    }
}
