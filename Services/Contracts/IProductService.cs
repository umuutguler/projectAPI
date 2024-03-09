using Entities.DataTransferObjects;
using Entities.Models;

namespace Services.Contracts
{
    public interface IProductService
    {
        IEnumerable<ProductDto> GetAllProducts(bool trackChanges);
        ProductDto GetOneProductById(int id, bool trackChanges);
        ProductDto CreateOneProduct(ProductDtoForInsertion product);
        void UpdateOneProduct(int id, ProductDtoForUpdate productDto, bool trackChanges);
        void DeleteOneProduct(int id, bool trackChanges);
        (ProductDtoForUpdate productDtoForUpdate, Product product) GetOneProductForPatch(int id, bool trackChanges); // mapperları controller üzeinde kullanamıyoruz. Böyle bir nesneye ihtiya. var
        
        void SaveChangesForPatch(ProductDtoForUpdate productDtoForUpdate, Product product);
    }
}
