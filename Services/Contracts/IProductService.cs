using Entities.DataTransferObjects;
using Entities.Models;

namespace Services.Contracts
{
    public interface IProductService
    {
        Task <IEnumerable<ProductDto>> GetAllProductsAsync(bool trackChanges);
        Task <ProductDto> GetOneProductByIdAsync(int id, bool trackChanges);
        Task <ProductDto> CreateOneProductAsync(ProductDtoForInsertion product);
        Task UpdateOneProductAsync(int id, ProductDtoForUpdate productDto, bool trackChanges);
        Task DeleteOneProductAsync(int id, bool trackChanges);
        Task<(ProductDtoForUpdate productDtoForUpdate, Product product)> GetOneProductForPatchAsync(int id, bool trackChanges); // mapperları controller üzeinde kullanamıyoruz. Böyle bir nesneye ihtiya. var

        Task SaveChangesForPatchAsync(ProductDtoForUpdate productDtoForUpdate, Product product);
    }
}
