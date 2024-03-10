using System;
using Repositories.Contracts;

namespace Repositories.EFCore
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext _context;
        private readonly Lazy<IProductRepository> _productRepository;

        public RepositoryManager(RepositoryContext context)
        {
            _context = context;
            _productRepository = new Lazy<IProductRepository> (() => new ProductRepository(_context));
        }

        public IProductRepository Product => _productRepository.Value;

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
