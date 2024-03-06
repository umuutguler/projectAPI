﻿using Entities.Models;
using Microsoft.EntityFrameworkCore.Update.Internal;
using Repositories.Contracts;

namespace Repositories.EFCore
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(RepositoryContext context) : base(context)
        {
        }
        public IQueryable<Product> GetAllProducts(bool trackChangers) => FindAll(trackChangers)
            .OrderBy(p => p.Id);

        public Product GetOneProductById(int id, bool trackChangers) => 
            FindByCondition(p => p.Id.Equals(id), trackChangers).SingleOrDefault();


        public void CreateOneProduct(Product product) => Create(product);

        public void UpdateOneProduct(Product product) => Update(product);

        public void DeleteOneProduct(Product product) => Delete(product); // RepositoryBase


    }


}
