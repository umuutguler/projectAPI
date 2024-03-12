using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update.Internal;
using Repositories.Contracts;

namespace Repositories.EFCore
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        // base(context) -> RepositoryBase<Book> -> RepositoryContext -> DbContext  Hiyerarşik yapı
        public ProductRepository(RepositoryContext context) : base(context)
        {
        }

        public async Task<PagedList<Product>> GetAllProductsAsync(ProductParameters productParameters, bool trackChanges)
        // async ve await anahtar sözcüğü gerekli
        {
            var products = await  FindAll(trackChanges)
             .FilterProducts(productParameters.MinPrice, productParameters.MaxPrice) // BookRepositoryExtensions Metodu
             .OrderBy(p => p.Id) // kitaplar id ye bağlı olarak sıralanmış olsun
             .ToListAsync(); // ifadenin asenkron dönmesi için

            return PagedList<Product>
                .ToPagedList(products, productParameters.PageNumber, productParameters.PageSize);
        }

        public async Task <Product> GetOneProductByIdAsync(int id, bool trackChanges) => 
            await FindByCondition(p => p.Id.Equals(id), trackChanges).SingleOrDefaultAsync();


        // implament interface
        public void CreateOneProduct(Product product) => Create(product);

        public void UpdateOneProduct(Product product) => Update(product);

        public void DeleteOneProduct(Product product) => Delete(product); // RepositoryBase


    }


}
