using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Repositories.Contracts
{
    public interface IRepositoryBase<T>
    {
        Task Create(T entity);
        Task Update(T entity);
        Task Delete(ObjectId id);
        Task<List<T>> GetList();
        Task<T> GetById(ObjectId id);
        Task<int> Count();
        Task<List<T>> GetFilteredList(Expression<Func<T,bool>> predicate);

        
    }
}
