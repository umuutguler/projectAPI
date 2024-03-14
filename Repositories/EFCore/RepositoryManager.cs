using System;
using Repositories.Contracts;

namespace Repositories.EFCore
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext _context;
        private readonly Lazy<IProductRepository> _productRepository;
        private readonly Lazy<IDepartmentRepository> _departmentRepository;
        private readonly Lazy<IChairRepository> _chairRepository;
        private readonly Lazy<IReservationInfoRepository> _reservationInfoRepository;
        private readonly Lazy<ITableRepository> _tableRepository;


        public RepositoryManager(RepositoryContext context)
        {
            _context = context;
            _productRepository = new Lazy<IProductRepository> (() => new ProductRepository(_context));
            _departmentRepository = new Lazy<IDepartmentRepository> (() => new DepartmentRepository(_context));
            _chairRepository = new Lazy<IChairRepository> (() => new ChairRepository(_context));
            _reservationInfoRepository = new Lazy<IReservationInfoRepository> (() => new ReservationInfoRepository(_context));
            _tableRepository = new Lazy<ITableRepository> (() => new TableRepository(_context));
        }

        public IProductRepository Product => _productRepository.Value;
        public IDepartmentRepository Department => _departmentRepository.Value;

        public IChairRepository Chair => _chairRepository.Value;

        public IReservationInfoRepository ReservationInfo => _reservationInfoRepository.Value;

        public ITableRepository Table => _tableRepository.Value;

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
