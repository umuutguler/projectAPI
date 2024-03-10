using Entities.Models;

namespace Repositories.Contracts
{
    public interface IProductRepository : IRepositoryBase<Product>
    {
        Task<IEnumerable<Product>> GetAllProductsAsync(bool trackChanges);
        Task<Product> GetOneProductByIdAsync(int id, bool trackChanges);
        void CreateOneProduct(Product product);
        void UpdateOneProduct(Product product);
        void DeleteOneProduct(Product product);
    }
}
