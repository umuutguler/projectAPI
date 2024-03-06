using Entities.Models;

namespace Services.Contracts
{
    public interface IProductService
    {
        IEnumerable<Product> GetAllProducts(bool trackChangers);
        Product GetOneProductById(int id, bool trackChangers);
        Product CreateOneProduct(Product product);
        void UpdateOneProduct(int id,Product product, bool trackChangers);
        void DeleteOneProduct(int id, bool trackChangers);
    }
}
