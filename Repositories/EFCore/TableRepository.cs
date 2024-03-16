using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;

namespace Repositories.EFCore
{
    public class TableRepository : RepositoryBase<Table>, ITableRepository
    {
        public TableRepository(RepositoryContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Table>> GetAllTablesAsync(bool trackChanges, bool includeRelated = true) //=>
          /*  await FindAll(trackChanges)
            .OrderBy(t => t.Id)
            .ToListAsync();*/
        {
            IQueryable<Table> query = FindAll(trackChanges).OrderBy(t => t.Id);

            if (includeRelated)
            {
                query = query.Include(t => t.Department)
                             .Include(t => t.Chairs);
            }

            return await query.ToListAsync();
        }

        public async Task<Table> GetOneTableByIdAsync(int id, bool trackChanges, bool includeRelated = true) //=>
        /* await FindByCondition(t => t.Id.Equals(id), trackChanges)
         .SingleOrDefaultAsync();*/
        {
            IQueryable<Table> query = FindByCondition(t => t.Id.Equals(id), trackChanges);

            if (includeRelated)
            {
                query = query.Include(t => t.Department)
                             .Include(t => t.Chairs);
            }

            return await query.SingleOrDefaultAsync();
        }

        public void CreateOneTable(Table table) => Create(table);

        public void DeleteOneTable(Table table) => Delete(table);

        public void UpdateOneTable(Table table) => Update(table);
    }
}
