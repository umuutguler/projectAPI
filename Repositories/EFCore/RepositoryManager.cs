using System;
using Repositories.Contracts;

namespace Repositories.EFCore
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoriesContext _context;
        private readonly Lazy<IProductRepository> _productRepository;

        public RepositoryManager(RepositoriesContext context)
        {
            _context = context;
            _productRepository = new Lazy<IProductRepository> (() => new ProductRepository(_context));
        }

        public IProductRepository Product => _productRepository.Value;

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
