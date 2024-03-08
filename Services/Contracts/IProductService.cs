using Entities.DataTransferObjects;
using Entities.Models;

namespace Services.Contracts
{
    public interface IProductService
    {
        IEnumerable<ProductDto> GetAllProducts(bool trackChangers);
        ProductDto GetOneProductById(int id, bool trackChangers);
        ProductDto CreateOneProduct(ProductDtoForInsertion product);
        void UpdateOneProduct(int id, ProductDtoForUpdate productDto, bool trackChangers);
        void DeleteOneProduct(int id, bool trackChangers);
    }
}
