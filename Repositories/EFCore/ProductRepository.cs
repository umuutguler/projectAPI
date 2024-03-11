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
        public async Task<IEnumerable<Product>> GetAllProductsAsync(ProductParameters productParameters, bool trackChanges) => await FindAll(trackChanges)    // async ve await anahtar sözcüğü gerekli
            .OrderBy(b => b.Id) // kitaplar id ye bağlı olarak sıralanmış olsun
            .Skip((productParameters.PageNumber - 1) * productParameters.PageSize)   // kaç tane Kayıt atlamak gerekir
            .Take(productParameters.PageSize)  // kaç tane kayıt almam gerek
            .ToListAsync(); // ifadenin asenkron dönmesi için

        public async Task <Product> GetOneProductByIdAsync(int id, bool trackChanges) => 
            await FindByCondition(p => p.Id.Equals(id), trackChanges).SingleOrDefaultAsync();

        // implament interface
        public void CreateOneProduct(Product product) => Create(product);

        public void UpdateOneProduct(Product product) => Update(product);

        public void DeleteOneProduct(Product product) => Delete(product); // RepositoryBase


    }


}
