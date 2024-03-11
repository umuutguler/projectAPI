using Entities.Models;
using Entities.RequestFeatures;

namespace Repositories.Contracts
{
    public interface IProductRepository : IRepositoryBase<Product>
    {
        Task<PagedList<Product>> GetAllProductsAsync(ProductParameters productParameters, bool trackChanges);
        Task<Product> GetOneProductByIdAsync(int id, bool trackChanges);
        void CreateOneProduct(Product product);
        void UpdateOneProduct(Product product);
        void DeleteOneProduct(Product product);
    }
}
