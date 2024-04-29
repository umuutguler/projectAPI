using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update.Internal;
using Repositories.Contracts;
using Repositories.EFCore.Extensions;
using Repositories.MongoDB;

namespace Repositories.EFCore
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        // base(context) -> RepositoryBase<Book> -> RepositoryContext -> DbContext  Hiyerarşik yapı
        public ProductRepository(RepositoryContext context) : base(context)
        {
        }

    }


}
