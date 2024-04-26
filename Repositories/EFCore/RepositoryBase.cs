using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Driver;
using Repositories.Contracts;
using Repositories.EFCore;

namespace Repositories.MongoDB
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        private readonly RepositoryContext _context;

        protected RepositoryBase(RepositoryContext context)
        {
            _context = context;
        }

        public async Task<int> Count()
        {
            return await _context.Set<T>().CountAsync();
        }

        public async Task Create(T entity)
        {
            await _context.AddAsync(entity);
        }

        public async Task Delete(ObjectId id)
        {
            var value = await GetById(id);
            _context.Remove(value);
        }

        public async Task<T> GetById(ObjectId id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<List<T>> GetFilteredList(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().Where(predicate).ToListAsync();
        }

        public async Task<List<T>> GetList()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task Update(T entity)
        {
            _context.Update(entity);
        }
    }
}
